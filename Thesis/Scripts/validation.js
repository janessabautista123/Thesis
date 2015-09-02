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

                    //$(".glyphicon glyphicon-remove").click(function () {
                    //    alert("NACLICK NA!");
                    //});
                }
                else {
                    $('.password').removeClass("has-error has-feedback");
                    $('#errorPass1').remove();
                    $('#errorPass2').remove();
                }
            }
        });

        $('.next').click(function () {
            var nextId = $(this).parents('.tab-pane').next().attr("id");
            //alert(nextId);
            $('[href=#' + nextId + ']').tab('show');
            return false;

        });

        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {

            //update progress
            var step = $(e.target).data('step');
            var percent = (parseInt(step) / 4) * 100;

            $('.progress-bar').css({ width: percent + '%' });
            $('.progress-bar').text("Step " + step + " of 4");

            //e.relatedTarget // previous tab

        });

        $('.first').click(function () {

            $('#myWizard a:first').tab('show')

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