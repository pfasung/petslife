var viewModel;

function PetOwner(id, firstName, lastName, emailAddress) {
    var self = this;

    self.Id = id;
    self.Name = firstName + " " + lastName;
}

function Pet() {
    var self = this;

    self.Id = ko.observable();
    self.Name = ko.observable();
    self.OwnerId = ko.observable();
    self.BirthDate = ko.observable();

    self.Owners = ko.observableArray([]);

    self.save = function () {
        var dataObject = ko.toJSON(this);
        var result = false;

        $.ajax({
            url: petsControllerUrl,
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            async: false,
            success: function (data) {
                history.go(-1);
                result = true;
            },

            error: function (request, textStatus, errorThrown) {
                alert(request.responseText);
            }
        });

        return result;
    };

    self.getOwners = function () {
        self.Owners.removeAll();

        $.ajax({
            url: petOwnersControllerUrl,
            type: 'get',
            contentType: 'application/json',
            async: false,
            success: function (data) {
                $.each(data, function (key, value) {
                    var petOwner = new PetOwner(value.Id, value.FirstName, value.LastName, value.EmailAddress);
                    self.Owners.push(petOwner);
                });
            }
        });
    };
}


viewModel = new Pet();

$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(viewModel);

    viewModel.getOwners();
    viewModel.OwnerId(OwnerId);
   
});