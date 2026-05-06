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


    // Multi-file attachment accumulator
 
    let selectedFiles = [];

    $(document).on('change', '#commentAttachments', function () {
        $.each(this.files, function (i, file) {
            const isDuplicate = selectedFiles.some(f => f.name === file.name && f.size === file.size);
            if (!isDuplicate) {
                selectedFiles.push(file);
            }
        });

        $('#commentAttachments').val('');

        renderAttachmentPreview();
    });

    function renderAttachmentPreview() {
        var preview = $('#attachmentPreview');
        preview.html('');
        $.each(selectedFiles, function (i, file) {
            preview.append(`
                <span class="badge bg-light text-dark border me-1 mb-1">
                    <i class="bi bi-paperclip"></i> ${file.name}
                    <button type="button"
                            class="btn-close remove-attachment-btn ms-1"
                            data-index="${i}"
                            style="font-size: 0.6rem;"
                            aria-label="Remove">
                    </button>
                </span>`);
        });
    }

   
    $(document).on('click', '.remove-attachment-btn', function () {
        var index = parseInt($(this).data('index'));
        selectedFiles.splice(index, 1);
        renderAttachmentPreview();
    });

    $(document).on('click', '#addAttachmentBtn', function () {
        $('#commentAttachments').click();
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

        
        for (var i = 0; i < selectedFiles.length; i++) {
            formData.append("Attachments", selectedFiles[i]);
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
                selectedFiles = [];
            },

            error: function (xhr) {
                alert("Failed to add comment. Status: " + xhr.status);
            }
        });
    });


    $(document).on('click', '.img-view-btn', function () {
        const imgPath = $(this).data('img-path');
        const imgName = $(this).data('img-name');

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
                        backgroundColor: ['#0d6efd', '#198754', '#ffc107']
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { position: 'bottom' }
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
    const hiddenInput = document.getElementById('remove_' + fileId);
    hiddenInput.disabled = false;

    btn.closest('li').classList.add('text-muted');
    btn.closest('li').querySelector('a').style.textDecoration = 'line-through';
    btn.disabled = true;
}













