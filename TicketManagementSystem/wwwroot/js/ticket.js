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

    $(document).on('click', '.delete-btn', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        openDeleteModal(id);
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

 
    $('[id^=ticketRow-]').on('click', function () {
        const id = this.id.split('-')[1];
        window.location.href = '/Ticket/Details/' + id;
    });

    $(document).on('click', '.edit-link, .delete-link', function (e) {
        e.stopPropagation();
    });

    
    $('#addCommentBtn').on('click', function () {

        var content = $('#newCommentContent').val().trim();
        if (!content) {
            alert("Please enter a comment.");
            return;
        }

        var ticketId = $("#commentsSection").attr("ticket-id");

        $.post('/Comment/Create', {
            TicketId: ticketId,
            Content: content
        })
            .done(function (html) {
                $('#commentsSection').append(html);
                $('#newCommentContent').val('');
            })
            .fail(function () {
                alert("Failed to add comment.");
            });
    });

    loadStatusChart();

});

function loadStatusChart() {

    $.ajax({
        url: '/Home/GetTicketStatusChart',
        type: 'GET',
        success: function (data) {

            var canvas = $('#statusChart');

            if (canvas.length === 0) return;

            var ctx = canvas[0].getContext('2d');

            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Open', 'In Progress', 'Closed'],
                    datasets: [{
                        data: [data.open, data.inProgress, data.closed],
                        backgroundColor: ['#0d6efd', '#198754','#ffc107' ]
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'bottom'
                        }
                    }
                }
            });
        },
        error: function (err) {
            console.log("Error loading chart:", err);
        }
    });
}

function removeAttachment(fileId, btn) {
    // Enable the hidden input so the ID gets submitted
    const hiddenInput = document.getElementById('remove_' + fileId);
    hiddenInput.disabled = false;

    // Strike through the item visually
    btn.closest('li').classList.add('text-muted');
    btn.closest('li').querySelector('a').style.textDecoration = 'line-through';
    btn.disabled = true;
}









//$(document).ready(function () {

//    $.ajaxSetup({
//        headers: { 'X-Requested-With': 'XMLHttpRequest' }
//    });

//    function handleAjaxError(xhr) {
//        if (xhr.status === 401) {
//            window.location.href = '/Identity/Account/Login';
//        } else {
//            alert("Something went wrong. Status: " + xhr.status);
//        }
//    }

//    window.openDeleteModal = function (id) {
//        $.get('/Ticket/Delete/' + id)
//            .done(function (html) {
//                $('#deleteModalContent').html(html);
//                const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
//                modal.show();
//            })
//            .fail(handleAjaxError);
//    };
//    $(document).on('click', '.btn btn-danger delete-btn', function (e) {

//        e.preventDefault();
//        openDeleteModal();
//    });

//    window.openCreateModal = function () {
//        $.get('/Ticket/Create')
//            .done(function (html) {
//                $('#createTicketModalContent').html(html);
//                const modal = new bootstrap.Modal(document.getElementById('createTicketModal'));
//                modal.show();
//            })
//            .fail(handleAjaxError);
//    };

//    $(document).on('click', '#createTicketBtn', function (e) {
//        e.preventDefault();
//        openCreateModal();
//    });

//    window.openEditModal = function () {
//        $.get('/Ticket/Edit' + id)
//            .done(function (html) {
//                $('#editTicketModalContent').html(html);
//                const modal = new bootstrap.Modal(document.getElementById('editTicketModal'));
//                modal.show();
//            })
//            .fail(handleAjaxError);
//    };
//    $(document).on('click', '.btn btn-primary edit-btn', function (e) {

//        e.preventDefault();
//        openDeleteModal();
//    });


//    $(document).ready(function () {

//        $('[id^=ticketRow-]').on('click', function () {
//            const id = this.id.split('-')[1];
//            window.location.href = '/Ticket/Details/' + id;
//        });
//        $(document).on('click', '.edit-link', function (e) {
//            e.stopPropagation();
//        });
//        $(document).on('click', '.delete-link', function (e) {
//            e.stopPropagation();
//            const id = $(this).data('id');
//            openDeleteModal(id);
//        });

//    });


//    $(document).ready(function () {
//        $('#addCommentBtn').on('click', function () {
//            debugger

//            var content = $('#newCommentContent').val().trim();
//            if (!content) {
//                alert("Please enter a comment.");
//                return;
//            }
//            var ticketId = $("#commentsSection").attr("ticket-id");
//            console.log(ticketId);

//            var data = {
//                TicketId: ticketId, Content: content
//            };

//            $.post('/Comment/Create', data)
//                .done(function (html) {
//                    $('#commentsSection').append(html);
//                    $('#newCommentContent').val('');
//                })
//                .fail(function () {
//                    alert("Failed to add comment.");
//                });
//        });

//    });

   

//});

