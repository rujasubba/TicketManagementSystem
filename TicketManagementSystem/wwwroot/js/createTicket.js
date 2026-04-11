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


//$(document).on("click", "#addCommentBtn", function () {
//    debugger
//    const newCommentContent = $(`
//        <div class=" comment-box mb-3 border-bottom pb-2">
//           <textarea class="form-control mb-2" id="newCommentContent" rows="3" placeholder="Enter your comment here..."></textarea>
//        </div>
//    `);
   

//    const dto = {
//        ticketId: $("#ticketId").val(),
//        content: $("#newCommentContent").val()
//    };

//    if (dto.content === "") {
//        alert("Please enter a comment.");
//        return;
//    }

//    $.ajax({
//        url: "/Comment/CreateAsync",
//        type: "POST",
//        data: dto,
//        success: function (response) {

//            if (response.success) {
//                $("#newCommentContent").val("");
//                $("#commentsContainer").Append(`
//                    <div class="mb-3 border-bottom pb-2">
//                       <div><strong>${response.createdBy}</strong> <small class="text-muted>${response.createdDate}</small></div>
//                          <div>${response.content}</div>
//                   </div>

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

