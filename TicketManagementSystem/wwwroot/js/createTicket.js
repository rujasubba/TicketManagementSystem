$(document).on("click", "#addCommentBtn", function () {
    const index = $("#commentsContainer .comment-item").length;

    const commentHtml = `
        <div class="comment-item mb-2">
            <textarea 
                name="CommentContents[${index}]"
                class="form-control"
                rows="3"
                placeholder="Add comment">
            </textarea>
        </div>
    `;

    $("#commentsContainer").append(commentHtml);
});



$(document).on("click", ".remove-attachment-btn", function () {
    debugger
    var $btn = $(this);
    var $listItem = $btn.closest("li");

    // Enable the hidden input so the ID is submitted with the form
    $listItem.find(".remove-attachment-input").prop("disabled", false);

    // Visual feedback
    $listItem.find("a").css("text-decoration", "line-through");
    $listItem.addClass("text-muted");
    $btn.prop("disabled", true).text("Removed");
});
