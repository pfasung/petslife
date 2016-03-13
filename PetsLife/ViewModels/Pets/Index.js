var viewModel;


function Pet(id, name, ownerId, birthDate, age) {
    var self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.OwnerId = ko.observable(ownerId)
    self.BirthDate = ko.observable(birthDate);
    self.Age = ko.observable(age);
       
    self.OwnerName = ko.computed(function () { return viewModel === undefined ? '' : viewModel.filter.Owners.petOwnersAssoc[self.OwnerId()].Name; });

    self.addPet = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: petsControllerUrl,
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                
                viewModel.petListViewModel.pets.push(new Pet(data.Id, data.Name, data.OwnerId, data.BirthDate))

                self.Id(undefined);
                self.Name('');
                self.OwnerId(undefined);
                self.BirthDate('');
            },

            error: function (request, textStatus, errorThrown)  {
                alert(request.responseText);
            }
        });
    };
}


function petList() {
    var self = this;
    self.pets = ko.observableArray([]);

    self.getPets = function () {
        var petId = viewModel.filter.PetId();
        var petAge = viewModel.filter.PetAge();
        var ownerId = viewModel.filter.OwnerId();

        var query = petsControllerUrl;
        if (!(!petId && !petAge && !ownerId))
            query += '/GetPetsByFilter/' + [petId, petAge, ownerId].map(function (a) { return !a ? '-1' : a }).join();

        $.getJSON(query, function (data) {

            self.pets.removeAll();
            $.each(data, function (key, value) {
                if (undefined != value.Id)
                    self.pets.push(new Pet(value.Id, value.Name, value.OwnerId, value.BirthDate, value.Age));
            });
        });
    };

    self.removePet = function (pet) {
        $.ajax({
            url: petsControllerUrl + '/' + pet.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.pets.remove(pet);
            }
        });
    };
}

function PetOwner(id, firstName, lastName, emailAddress) {
    var self = this;

    self.Id = id;
    self.Name = firstName + " " + lastName;
}

function petOwnerList() {

    var self = this;
    self.petOwners = ko.observableArray([]);
    self.petOwnersAssoc = {};

    self.getPetOwners = function () {
        $.ajax({
            url: petOwnersControllerUrl,
            type: 'get',
            async: false,
            contentType: 'application/json',
            success: function (data) {

                self.petOwners.removeAll();
                self.petOwnersAssoc = {};

                $.each(data, function (key, value) {
                    var petOwner = new PetOwner(value.Id, value.FirstName, value.LastName, value.EmailAddress);
                    self.petOwnersAssoc[value.Id] = petOwner;
                    self.petOwners.push(petOwner);
                });
            }
        });
    };
}

function Filter() {
    var self = this;

    self.PetId = ko.observable();
    self.PetAge = ko.observable();

    self.OwnerId = ko.observable();
    self.Owners = new petOwnerList();
}

viewModel = { filter : new Filter(), petListViewModel: new petList()  };



$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(viewModel);

    viewModel.filter.Owners.getPetOwners();
    viewModel.petListViewModel.getPets();
});