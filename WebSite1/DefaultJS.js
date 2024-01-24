

$(document).ready(function () {

    // country
    $('#ddlCountry').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddlCountry').on('select2:select', function (e) {
        __doPostBack('<%= ddlCountry.ClientID %>', '');
    });

    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {

        }, 0);

        // category
        $('#ddlCountry').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        
    });
});
