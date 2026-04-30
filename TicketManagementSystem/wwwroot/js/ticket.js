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
        dubugger
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
        var formData = new FormData();
        formData.append("Content", content);
        formData.append("TicketId", ticketId);

        
        var files = $('#commentAttachments')[0].files;
        for (var i = 0; i < files.length; i++) {
            formData.append("Attachments", files[i]);
        }

        $.ajax({
            url: '/Comment/Create',
            type: 'POST',
            data: formData,
            contentType: false, 
            processData: false,   
            
            success: function (result) {
      
                var attachmentsHtml = '';
                if (result.attachments && result.attachments.length > 0) {
                    attachmentsHtml = '<ul class="list-group mt-2">';
                    $.each(result.attachments, function (i, a) {
                        var filePath = a.filePath.startsWith('/') ? a.filePath : '/' + a.filePath;
                        var imageExtensions = /\.(jpg|jpeg|png|gif|bmp|webp|svg)$/i;

                        var viewBtn = '';
                        if (imageExtensions.test(filePath)) {
                           
                            viewBtn = `
                    <a class="btn btn-sm btn-outline-primary img-view-btn"
                       data-img-path="${filePath}"
                       data-img-name="${a.fileName}">
                        <i class="bi bi-eye"></i> View
                    </a>`;
                        } else {
                            
                            viewBtn = `
                    <a href="${filePath}" target="_blank"
                       class="btn btn-sm btn-outline-primary">
                        <i class="bi bi-eye"></i> View
                    </a>`;
                        }

                        attachmentsHtml += `
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    <span class="text-truncate me-2">
                        <i class="bi bi-paperclip"></i> ${a.fileName}
                    </span>
                    <div class="d-flex gap-1">
                        <a href="${filePath}" target="_blank"
                           class="btn btn-sm btn-outline-secondary">
                            <i class="bi bi-download"></i> Download
                        </a>
                        ${viewBtn}
                    </div>
                </li>`;
                    });
                    attachmentsHtml += '</ul>';
                }

                var commentHtml = `
        <div class="mb-3 border-bottom pb-2">
            <div>
                <small><strong>${result.createdByFullName}</strong></small><br />
                <small class="text-muted">${result.createdDate}</small>
            </div>
            <div>${result.content}</div>
            ${attachmentsHtml}
        </div>`;

                $('#commentsSection p.text-muted').remove();

                $('#commentsSection').prepend(commentHtml);

                $('#newCommentContent').val('');
                $('#commentAttachments').val('');
                $('#attachmentPreview').html('');
            },
            error: function (xhr) { 
                alert("Failed to add comment. Status: " + xhr.status);
            }
            
        });
    });

    
    $(document).on('change', '#commentAttachments', function () {
        var preview = $('#attachmentPreview');
        preview.html('');
        $.each(this.files, function (i, file) {
            preview.append(`
            <span class="badge bg-light text-dark border">
                <i class="bi bi-paperclip"></i> ${file.name}
            </span>`);
        });
    });

    $(document).on('click', '.img-view-btn', function () {
        const imgPath = $(this).data('img-path');
        const imgName = $(this).data('img-name');

        console.log('imgPath:', imgPath); 
        console.log('imgName:', imgName);

        const imageExtensions = /\.(jpg|jpeg|png|gif|bmp|webp|svg)$/i;

        if (imageExtensions.test(imgPath)) {
            $('#previewImage').attr('src', imgPath);
            $('#imagePreviewModal .modal-title').text(imgName);
            const modal = new bootstrap.Modal(document.getElementById('imagePreviewModal'));
            modal.show();
        } else {
            window.open(imgPath, '_blank');
        }
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

