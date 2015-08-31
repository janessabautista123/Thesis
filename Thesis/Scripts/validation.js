$(document).ready(function () {
        $("#confirmPassword, #password").change(function () {
            if ($('#confirmPassword').val() != "") {
                if ($('#confirmPassword').val() != $('#password').val()) {
                    $('.password').removeClass("has-error has-feedback");
                    $('#errorPass1').remove();
                    $('#errorPass2').remove();
                    $('.password').addClass("has-error has-feedback");
                    $('#password').after("<div id=\"errorPass1\"><span class=\"glyphicon glyphicon-remove form-control-feedback\"></span></div>");
                    $('#confirmPassword').after("<div id=\"errorPass2\"><span class=\"glyphicon glyphicon-remove form-control-feedback\"></span><br>Passwords do not match.</div>");
                }
                else {
                    $('.password').removeClass("has-error has-feedback");
                    $('#errorPass1').remove();
                    $('#errorPass2').remove();
                }
            }
        });
    //$('.identicalForm').formValidation({
    //    framework: 'bootstrap',
    //    icon: {
    //        valid: 'glyphicon glyphicon-ok',
    //        invalid: 'glyphicon glyphicon-remove',
    //        validating: 'glyphicon glyphicon-refresh'
    //    },
    //    fields: {
    //        confirmPassword: {
    //            validators: {
    //                identical: {
    //                    enabled: true,
    //                    field: 'password',
    //                    message: 'The password and its confirm are not the same'
    //                }
    //            }
    //        }
    //    }
    //});

    $('#datePicker')
        .datepicker({
            format: 'mm/dd/yyyy',
            endDate: '+0d',
            autoclose: true
        })
        .on('changeDate', function (e) {
            // Revalidate the date field
            $('#eventForm').formValidation('revalidateField', 'date');
        });

    //$('#eventForm').formValidation({
    //    framework: 'bootstrap',
    //    icon: {
    //        valid: 'glyphicon glyphicon-ok',
    //        invalid: 'glyphicon glyphicon-remove',
    //        validating: 'glyphicon glyphicon-refresh'
    //    },
    //    fields: {
    //        name: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'The name is required'
    //                }
    //            }
    //        },
    //        date: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'The date is required'
    //                },
    //                date: {
    //                    format: 'MM/DD/YYYY',
    //                    message: 'The date is not a valid'
    //                }
    //            }
    //        }
    //    }
    //});

    
});