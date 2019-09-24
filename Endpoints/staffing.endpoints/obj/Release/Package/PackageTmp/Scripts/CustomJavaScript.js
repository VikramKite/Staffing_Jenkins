//this code for avaid double click but its not working due to Jquery refernces start
function CheckValidInput(submitElement) {
    var IsValidData = true;
    $(submitElement).prop("disabled", true);
    $(submitElement).css("cursor", "default");
    var FormInputFields = $('input[type=text]').each(function () {
        if (IsValidData)
            IsValidData = CheckLegalCharactersOnSubmit(this);
    });
    if (IsValidData) {
        if (!$('form').valid()) {
            $('input[type=submit]').prop("disabled", false);
            $(submitElement).prop("disabled", false);
            $('input[type=submit]').css("cursor", "pointer");
            IsValidData = false;
        } else {
            $(submitElement).prop("disabled", true);
            $(submitElement).css("cursor", "default");
            $(submitElement).prop("value", "Please wait...");
            IsValidData = true;
            $('form').submit();
        }
    }
    return IsValidData;

}

function CheckLegalCharactersOnSubmit(e, t) {
    try {
        if (($(e).val().indexOf('<') != -1) || ($(e).val().indexOf('>') != -1)) {
            alert("Please enter valid inputs.", "", "error");
            return false;
        } else
            return true;
    } catch (err) {
        alert(err.Description);
        return false;
    }
}
// end

//below coade for input text or numeric validation start
function IsNumeric(event) {
    var keyCode = event.which ? event.which : event.keyCode;

    if ((keyCode >= 48 && keyCode <= 57) || //lets allow only numerics
            ((keyCode == 46)) //allow period conditionally
        //based on the control's choice
    ) {
        return true;
    }

    return false;
};

function alphaOnly(event) {
    var keyCode = event.which ? event.which : event.keyCode;

    if ((keyCode >= 97 && keyCode <= 122) || //lets allow for the small alphabets
            (keyCode >= 65 && keyCode <= 90) || //Let us allow the capital letters too
            ((keyCode == 32)) //allow space conditionally
        //based on the control's choice
    ) {
        return true;
    }

    return false;
};

function AcceptAlphaNumericOnly(event) {
    if ((alphaOnly(event) == true) || //Create alphabetic text box
            (IsNumeric(event) == true) //Create numeric text box
    ) {
        return true;
    }

    return false;
};
// end

//avoid double click start
function DisableButton(b) {
    b.disabled = true;
    b.value = 'Please wait...';
    b.form.submit();
}
//end

