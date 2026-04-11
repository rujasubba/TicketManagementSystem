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
            var content = $('#newCommentContent').val().trim();
            if (!content) {
                alert("Please enter a comment.");
                return;
            }
            var ticketId = $('#commentsSection').data('ticket-id');

            $.post('/Comment/Create', { TicketId: ticketId, Content: content })
                .done(function (html) {
                    $('#commentsSection').append(html);
                    $('#newCommentContent').val('');
                })
                .fail(function () {
                    alert("Failed to add comment.");
                });
        });

    });

});
//$(document).on("click", "#addCommentBtn", function () {
//    debugger

//    const dto = {
//        ticketId: $("#ticketId").val(),
//        content: $("#newCommentContent").val().trim()
//    };

//    if (dto.content === "") {
//        alert("Please enter a comment.");
//        return;
//    }

//    $.ajax({
//        url: "/Comment/AddComment",
//        type: "POST",
//        data: dto,
//        success: function (response) {

//            if (response.success) {

//                $("#newCommentContent").val("");

//                // optionally add comment immediately to UI
//                $("#commentsSection").prepend(`
//                    <div class="mb-3 border-bottom pb-2">
//                        <div>
//                            <small class="text-muted">Just now</small>
//                        </div>
//                        <div>${dto.content}</div>
//                    </div>
//                `);
//            }
//            else {
//                alert(response.message);
//            }
//        },
//        error: function () {
//            alert("Unable to add comment.");
//        }
//    });
//});

//$(document).ready(function () {

//    $("#addCommentBtn").click(function () {

//        const content = $("#newCommentContent").val().trim();
//        const ticketId = $("#ticketId").val();

//        if (content === "") {
//            alert("Please enter a comment");
//            return;
//        }

//        $.ajax({
//            url: "/Ticket/AddComment",
//            type: "POST",
//            data: {
//                ticketId: ticketId,
//                content: content
//            },
//            success: function (response) {

//                if (response.success) {

//                    // remove "No comments yet."
//                    $("#commentsSection p.text-muted").remove();

//                    const newCommentHtml = `
//                        <div class="mb-3 border-bottom pb-2" data-id="${response.id}">
//                            <div class="d-flex justify-content-between">
//                                <div>
//                                    <strong>${response.createdBy}</strong><br />
//                                    <small class="text-muted">${response.createdDate}</small>
//                                </div>
//                            </div>

//                            <div class="mt-2">${response.content}</div>

//                            <div class="mt-1">
//                                <a href="javascript:void(0);" class="text-primary small edit-comment">Edit</a> |
//                                <a href="javascript:void(0);" class="text-danger small delete-comment">Delete</a>
//                            </div>
//                        </div>
//                    `;

//                    // prepend = show newest comment at top
//                    $("#commentsSection").prepend(newCommentHtml);

//                    // clear textbox
//                    $("#newCommentContent").val("");
//                }
//            },
//            error: function () {
//                alert("Failed to add comment");
//            }
//        });
//    });

//});