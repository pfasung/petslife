var viewModel;

function Pet(id, name, birthDate) {
    var self = this;

    self.Id = id;
    self.Name = name;
    self.BirthDate = birthDate;
}

function PetOwner(id, firstName, lastName, emailAddress) {
    var self = this;

    self.Id = id;
    self.Name = firstName + " " + lastName;
    self.EmailAddress = emailAddress;
}
function PetWalker(id, firstName, lastName) {
    var self = this;

    self.Id = id;
    self.Name = firstName + " " + lastName;
}

function Approval() {
    var self = this;

    self.Id = ko.observable();
    self.OwnerId = ko.observable();
    self.PetId = ko.observable();
    self.WalkerId = ko.observable();
    self.Occurence = ko.observable();
    self.DateApproved = ko.observable();

    self.Pets = ko.observableArray([]);
    self.Owners = ko.observableArray([]);
    self.Walkers = ko.observableArray([]);

    self.OwnerId.subscribe(function (newValue) { self.getPets();})

    self.save = function () {

        self.DateApproved(new Date().toISOString());
        var dataObject = ko.toJSON(this);
        var result = false;


        $.ajax({
            url: petApprovalsControllerUrl,
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
                    self.Owners.push(new PetOwner(value.Id, value.FirstName, value.LastName, value.EmailAddress));
                });
            }
        });
    };

    self.getPets = function () {
        self.Pets.removeAll();
        $.ajax({
            url: petOwnersControllerUrl + '/' + self.OwnerId() + '/pets',
            type: 'get',
            contentType: 'application/json',
            async: false,
            success: function (data) {
                $.each(data, function (key, value) {
                    if (undefined != value.Id)
                        self.Pets.push(new Pet(value.Id, value.Name, value.BirthDate));
                });
            }
        });
    };

    self.getWalkers = function () {
        self.Walkers.removeAll();
        $.ajax({
            url: petWalkersControllerUrl,
            type: 'get',
            contentType: 'application/json',
            async: false,
            success: function (data) {
                $.each(data, function (key, value) {
                    self.Walkers.push(new PetWalker(value.Id, value.FirstName, value.LastName));
                });
            }
        });
    };
}


viewModel = new Approval();

$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(viewModel);

    viewModel.getWalkers();
    viewModel.getOwners();
    viewModel.OwnerId(OwnerId);
   
});