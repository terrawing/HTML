$( document ).ready(function() {
    console.log( "ready!" );
    $("input[name='inputFirstName']").val("");
    $("input[name='inputLastName']").val("");
    $("input[name='inputEmail']").val("");
    $("#myModalSuccess").modal("hide");
    $("#myModalFail").modal("hide");
    
    $("#rsvpNo").click(function(){
        $("#RSVP").val('No');
    });

    $("#rsvpYes").click(function(){
        $("#RSVP").val('Yes');
    });
    
    $('input[type="text"]').keyup(function(){
        if ($("input[name='inputFirstName']").val() != "" && $("input[name='inputLastName']").val() != "" && $("input[name='inputEmail']").val() != "") {
            $("input[name='inputSubmitYes']").removeClass('disabled').addClass('active');
            $("input[name='inputSubmitNo']").removeClass('disabled').addClass('active');
        }
        else {
            $("input[name='inputSubmitYes']").addClass('disabled');
            $("input[name='inputSubmitNo']").addClass('disabled');
        }
    });
    
    function getParameterByName(name, url) {
        if (!url) {
          url = window.location.href;
        }
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
    
    var success = null;
    var error1 = null;
    var error2 = null;
    var error3 = null;
    var error4 = null;
    var error5 = null;
    
    success = getParameterByName('success');
    error1 = getParameterByName('error1');
    error2 = getParameterByName('error2');
    error3 = getParameterByName('error3');
    error4 = getParameterByName('error4');
    error5 = getParameterByName('error5');
    
    if (error1 != null) {
        $("label[name='error1").text('First name cannot be empty.');
        $("#myModalFail").modal("show");
    }
    else if (error2 != null) {
        $("label[name='error1").text('First name can only contain alphabetical letters, numbers, dash and space.');
        $("#myModalFail").modal("show");
    }
    else if (error3 != null) {
        $("label[name='error2").text('Last name cannot be empty.');
        $("#myModalFail").modal("show");
    }
    else if (error4 != null) {
        $("label[name='error2").text('Last name can only contain alphabetical letters, numbers, dash and space.');
        $("#myModalFail").modal("show");
    }
    else if (error5 != null) {
        $("label[name='error3").text('Email cannot be empty.');
        $("#myModalFail").modal("show");
    }
    else if (success != null) {
        $("#myModalSuccess").modal("show");
    }
    
});