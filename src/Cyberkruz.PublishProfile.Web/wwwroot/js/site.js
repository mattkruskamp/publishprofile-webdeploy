publishProfile = window.publishProfile || {};

(function(self, $) {

    self.vars = {
        $form: $('#entry'),
        $result: $('#result'),
        $yml: $('#yml'),
        $error: $('#error'),
        $done: $('#done'),
        url: '/api/webdeploy'
    };

    self.submitForm = function(e) {
        e.preventDefault();
        var vars = self.vars;

        vars.$error.hide();

        $.ajax({
            url: vars.url,
            type: 'POST',
            dataType: 'json',
            data: vars.$form.serialize(),
            success: success,
            error: error
        });

        function success(res) {
            vars.$yml.text(res.yml);

            vars.$form.fadeOut(function() {
                vars.$result.fadeIn();
            });
        }

        function error(err) {
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

