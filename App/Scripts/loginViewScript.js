$('#login').click(function () {
    var val = $('form').validate();
    val.checkForm();

    if (!val.valid()) {
        val.showErrors();
    }
    else {
        $('form').submit();
        $('#login').attr('disabled', true);

    }
   
});