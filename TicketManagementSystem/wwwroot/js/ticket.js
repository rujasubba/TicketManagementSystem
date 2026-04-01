$(document).ready(function () {

    window.openDeleteModal = function (id) {
        $.ajax({
            url: '/Ticket/Delete/' + id,
            type: 'GET',
            success: function (response) {
                $('#deleteModalContent').html(response);

                var modal = new bootstrap.Modal(document.getElementById('deleteModal'));
                modal.show();
            },
            error: function () {
                alert('Unable to load delete confirmation.');
            }
        });
    };

});