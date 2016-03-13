var viewModel;

function PetWalker(id, firstName, lastName, emailAddress, phoneNumber) {
    var self = this;

    self.Id = ko.observable(id);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.EmailAddress = ko.observable(emailAddress);
    self.PhoneNumber = ko.observable(phoneNumber);
   
    self.addPetWalker = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: petWalkersControllerUrl,
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                
                viewModel.petWalkerListViewModel.petWalkers.push(new PetWalker(data.Id, data.FirstName, data.LastName, data.EmailAddress, data.PhoneNumber))

                self.Id(undefined);
                self.FirstName('');
                self.LastName('');
                self.EmailAddress('');
                self.PhoneNumber('');
            },

            error: function (request, textStatus, errorThrown)  {
                alert(request.responseText);
            }
        });
    };
}

function petWalkerList() {
    var self = this;
    self.petWalkers = ko.observableArray([]);

    self.getPetWalkers = function () {
        $.getJSON(petWalkersControllerUrl, function (data) {
            self.petWalkers.removeAll();

            $.each(data, function (key, value) {
                self.petWalkers.push(new PetWalker(value.Id, value.FirstName, value.LastName, value.EmailAddress, value.PhoneNumber));
            });
        });
    };

    self.removePetWalker = function (petWalker) {
        $.ajax({
            url: petWalkersControllerUrl + '/' + petWalker.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.petWalkers.remove(petWalker);
            }
        });
    };
}


viewModel = { addPetWalkerViewModel : new PetWalker(), petWalkerListViewModel: new petWalkerList() };

$(document).ready(function () {

    ko.applyBindings(viewModel);

    viewModel.petWalkerListViewModel.getPetWalkers();
});
