publishProfile = window.publishProfile || {};

(function(self, $) {

    self.submitForm = function(e) {
        e.preventDefault();

        var $form = $(this),
            $result = $('#result'),
            $yml = $('#yml'),
            $error = $('#error');
        
        $error.hide();

        $.ajax({
            url: '/api/webdeploy',
            type: 'POST',
            dataType: 'json',
            data: $form.serialize(),
            success: success,
            error: error
        });

        function success(res) {
            $yml.text(res.yml);

            $form.fadeOut(function() {
                $result.fadeIn();
            });
        }

        function error(err) {
            $form.show();
            $result.hide();
            $error.show();
        }
    };

    self.load = function() {
        var $form = $('#entry');

        $form.submit(self.submitForm);
    };

    $(document).ready(function() {
        self.load();
    });

})(publishProfile, jQuery);

