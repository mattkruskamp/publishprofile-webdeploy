publishProfile = window.publishProfile || {};

(function(self, $) {

    self.vars = {
        $form: $('#entry'),
        $result: $('#result'),
        $yml: $('#yml'),
        $error: $('#error'),
        $done: $('#done'),
        $fieldset: $('#fieldset'),
        url: '/api/webdeploy'
    };

    self.submitForm = function(e) {
        e.preventDefault();
        var vars = self.vars;

        vars.$error.hide();
       

        //document.getElementById("fieldset").disabled = true;
        $.ajax({
            url: vars.url,
            type: 'POST',
            dataType: 'json',
            data: vars.$form.serialize(),
            success: success,
            error: error
        });
        vars.$fieldset.prop("disabled", true);

        function success(res) {
            vars.$yml.text(res.yml);
            vars.$fieldset.prop("disabled", false);
            vars.$form.fadeOut(function() {
                vars.$result.fadeIn();
            });
        }

        function error(err) {
            vars.$fieldset.prop("disabled", false);
            vars.$error.show();
            self.showForm();
        }
    };

    self.donePressed = function(e) {
        e.preventDefault();
        var vars = self.vars;

        vars.$result.fadeOut(function() {
            vars.$form.fadeIn();
        });
    };

    self.load = function() {
        var vars = self.vars;

        vars.$form.submit(self.submitForm);
        vars.$done.click(self.donePressed);
    };

    $(document).ready(function() {
        self.load();
    });

})(publishProfile, jQuery);

