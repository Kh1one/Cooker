﻿using cooker_api.Features.Comment.UpdateComment;
using cooker_api.Features.Post.UpdatePost;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/update-comment/{CommentId}")]
    [ApiController]
    public class UpdateCommentController : ControllerBase
    {
        private readonly ISender _sender;
        public UpdateCommentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(int CommentId, [FromBody]string Content)
        {
            var command = new UpdateCommentCommand(CommentId, Content);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
