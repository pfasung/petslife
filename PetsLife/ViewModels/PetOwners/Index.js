var viewModel;

function PetOwner(id, firstName, lastName, emailAddress) {
    var self = this;

    self.Id = ko.observable(id);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.EmailAddress = ko.observable(emailAddress);
}

function petOwnerList() {
    var self = this;

    self.petOwners = ko.observableArray([]);

    self.load = function () {
        $.getJSON(petOwnersControllerUrl, function (data) {

            self.petOwners.removeAll();

            $.each(data, function (key, value) {
                self.petOwners.push(new PetOwner(value.Id, value.FirstName, value.LastName, value.EmailAddress));
            });
        });
    };

    self.removePetOwner = function (petOwner) {
        $.ajax({
            url: petOwnersControllerUrl + '/' + petOwner.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.petOwners.remove(petOwner);
            }
        });
    };
}



viewModel = new petOwnerList();

$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(viewModel);

    // load pet owners
    viewModel.load();
});