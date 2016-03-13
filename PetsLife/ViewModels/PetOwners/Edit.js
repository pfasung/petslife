var viewModel;


function Pet(id, name, ownerId, birthDate) {
    var self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.OwnerId = ko.observable(ownerId);
    self.BirthDate = ko.observable(birthDate);  
}

function Approval(id, petId, petName, walkerId, occurence, dateApproved) {
    var self = this;

    self.Id = ko.observable(id);
    self.PetId = ko.observable(petId);
    self.PetName = ko.observable(petName);
    self.WalkerId = ko.observable(walkerId);

    self.Occurence = ko.observable(occurence);
    self.DateApproved = ko.observable(dateApproved);

    var walker = viewModel.petWalkerList.petWalkersAssoc[walkerId];
    self.WalkerName = walker.Name;
}

function PetOwner(id, firstName, lastName, emailAddress) {
    var self = this;
    
    self.Id = ko.observable(id);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.EmailAddress = ko.observable(emailAddress);
    self.Pets = ko.observableArray([]);
    self.Approvals = ko.observableArray([]);

    self.DeletedPets = ko.observableArray([]);
    self.DeletedApprovals = ko.observableArray([]);

    self.load = function (id) {

        $.getJSON(petOwnersControllerUrl + '/' + id, function (data) {
            
            self.Id(data.Id)
            .FirstName(data.FirstName)
            .LastName(data.LastName)
            .EmailAddress(data.EmailAddress);

            $.each(data.Pets, function (petKey, pet) {
                self.Pets.push(new Pet(pet.Id, pet.Name, self.Id(), pet.BirthDate));

                $.each(pet.Approvals, function (approvalKey, approval) {
                    self.Approvals.push(new Approval(approval.Id, pet.Id, pet.Name, approval.WalkerId, approval.Occurence, approval.DateApproved));
                });
            });
        });
    }

    self.save = function () {
        var dataObject = ko.toJSON(this);

        $.each(self.DeletedPets(), function (key, value) {
            self.deletePet(value);
        });
        self.DeletedPets.removeAll();

        $.each(self.DeletedApprovals(), function (key, value) {
            self.deleteApproval(value);
        });
        self.DeletedApprovals.removeAll();

        $.ajax({
            url: petOwnersControllerUrl + '/' + this.Id(),
            type: 'put',
            data: dataObject,
            async:false,
            contentType: 'application/json',
            success: function (data) {

                history.go(-1);
            },

            error: function (request, textStatus, errorThrown) {
                alert(request.responseText);
            }
        });
    }
    self.removePet = function (pet) {
        self.Pets.remove(pet);
        self.DeletedPets.push(pet);
    }
    self.removeApproval = function (approval) {
        self.Approvals.remove(approval);
        self.DeletedApprovals.push(approval);
    }
    self.deletePet = function (pet) {
        $.ajax({
            url: petsControllerUrl + '/' + pet.Id(),
            type: 'delete',
            contentType: 'application/json',
        });
    }
    self.deleteApproval = function (approval) {
        $.ajax({
            url: petApprovalsControllerUrl + '/' + approval.Id(),
            type: 'delete',
            contentType: 'application/json',
        });

    }
}


function PetWalker(id, firstName, lastName, emailAddress, phoneNumber) {
    var self = this;

    self.Id = id;
    self.Name = firstName + " " + lastName;
    self.EmailAddress = emailAddress;
    self.PhoneNumber = phoneNumber;
}

function petWalkerList() {

    var self = this;
    self.petWalkers = ko.observableArray([]);
    self.petWalkersAssoc = {};

    self.getPetWalkers = function () {
        self.petWalkers.removeAll();
        self.petWalkersAssoc = {};

        $.getJSON(petWalkersControllerUrl, function (data) {
            $.each(data, function (key, value) {
                var petOwner = new PetWalker(value.Id, value.FirstName, value.LastName, value.EmailAddress, value.PhoneNumber);
                self.petWalkersAssoc[value.Id] = petOwner;
                self.petWalkers.push(petOwner);
            });
        });
    };
}


viewModel = { petOwner : new PetOwner(), petWalkerList : new petWalkerList() };


$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(viewModel);

    // load data
    viewModel.petWalkerList.getPetWalkers();
    viewModel.petOwner.load(OwnerId);
});

