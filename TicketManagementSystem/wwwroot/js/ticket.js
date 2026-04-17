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

    window.openDeleteModal = function (id) {
        $.get('/Ticket/Delete/' + id)
            .done(function (html) {
                $('#deleteModalContent').html(html);
                const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
                modal.show();
            })
            .fail(handleAjaxError);
    };
    $(document).on('click', '.btn btn-danger delete-btn', function (e) {

        e.preventDefault();
        openDeleteModal();
    });

    window.openCreateModal = function () {
        $.get('/Ticket/Create')
            .done(function (html) {
                $('#createTicketModalContent').html(html);
                const modal = new bootstrap.Modal(document.getElementById('createTicketModal'));
                modal.show();
            })
            .fail(handleAjaxError);
    };

    $(document).on('click', '#createTicketBtn', function (e) {
        e.preventDefault();
        openCreateModal();
    });

    window.openEditModal = function () {
        $.get('/Ticket/Edit' + id)
            .done(function (html) {
                $('#editTicketModalContent').html(html);
                const modal = new bootstrap.Modal(document.getElementById('editTicketModal'));
                modal.show();
            })
            .fail(handleAjaxError);
    };
    $(document).on('click', '.btn btn-primary edit-btn', function (e) {

        e.preventDefault();
        openDeleteModal();
    });


    $(document).ready(function () {

        $('[id^=ticketRow-]').on('click', function () {
            const id = this.id.split('-')[1];
            window.location.href = '/Ticket/Details/' + id;
        });
        $(document).on('click', '.edit-link', function (e) {
            e.stopPropagation();
        });
        $(document).on('click', '.delete-link', function (e) {
            e.stopPropagation();
            const id = $(this).data('id');
            openDeleteModal(id);
        });

    });


    $(document).ready(function () {
        $('#addCommentBtn').on('click', function () {
            debugger

            var content = $('#newCommentContent').val().trim();
            if (!content) {
                alert("Please enter a comment.");
                return;
            }
            var ticketId = $("#commentsSection").attr("ticket-id");
            console.log(ticketId);

            var data = {
                TicketId: ticketId, Content: content
            };

            $.post('/Comment/Create', data)
                .done(function (html) {
                    $('#commentsSection').append(html);
                    $('#newCommentContent').val('');
                })
                .fail(function () {
                    alert("Failed to add comment.");
                });
        });

    });

    //$(document).on('click', '.ticket-status', function (e) {
    //    e.preventDefault();
    //    $.get('/Home/GetTicketByStatusId' + id)
    //}

});

