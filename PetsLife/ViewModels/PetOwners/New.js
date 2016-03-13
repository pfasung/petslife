var viewModel;

function PetOwner() {
    var self = this;
    
    self.Id = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.EmailAddress = ko.observable();

    self.save = function () {
        var dataObject = ko.toJSON(this);

        var redirect = false;

        $.ajax({
            url: petOwnersControllerUrl,
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            async: false,
            success: function (data) {

                self.Id(data.Id);
                redirect = true;
            },

            error: function (request, textStatus, errorThrown)  {
                alert(request.responseText);
            }
        });

        return redirect;
    };
}

viewModel = new PetOwner();

$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(viewModel);
});




