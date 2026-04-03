$(document).ready(function () {

    $.ajaxSetup({
        headers: { 'X-Requested-With': 'XMLHttpRequest' }
    });

    function handleAjaxError(xhr) {
        if (xhr.status === 401) {
            window.location.href = '/Identity/Account/Login';
        } else {
            alert("Something went wrong. Status: " + xhr.status);
        }
    }

    window.openCreateModal = function () {
        $.get('/Department/Create')
            .done(function (html) {
                $('#createDepartmentModalContent').html(html);
                const modal = new bootstrap.Modal(document.getElementById('createDepartmentModal'));
                modal.show();
            })
            .fail(handleAjaxError);
    };

    $(document).on('click', '#createDepartmentBtn', function (e) {
        e.preventDefault();
        openCreateModal();
    });


    window.openDeleteModal = function (id) {
        $.get('/Department/Delete/' + id)
            .done(function (html) {
                $('#deleteModalContent').html(html);
                const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
                modal.show();
            })
            .fail(handleAjaxError);
    };

});
