using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary.Models;
using AutoMapper;
using ReunitePetsWebAPI.Services;
using ReunitePetsWebAPI.DTOs;

namespace ReunitePetsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        // GET: /Comments?petId=1
        [HttpGet]
        public async Task<ActionResult> GetCommentsByPetId(int petId)
        {
            var comments = await _commentRepository.GetCommentsByPetId(petId);

            var results = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Ok(results);
        }

        // POST: /Comments
        [HttpPost]
        public async Task<ActionResult> AddComment([FromBody] CommentCreateDto comment)
        {
            if (comment == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var commentToInsert = _mapper.Map<Comment>(comment);
            commentToInsert.CommentDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

            await _commentRepository.AddComment(commentToInsert);

            if (!await _commentRepository.Save())
            {
                return StatusCode(500, "A problem occured while processing the request");
            }

            return StatusCode(200, "Comment is submitted successfully.");
        }

        // GET: /Comments/1
        [HttpGet("{commentId}")]
        public async Task<ActionResult> GetCommentsById(int commentId)
        {
            var comments = await _commentRepository.GetCommentById(commentId);

            if (comments == null)
            {
                return NotFound();
            }

            var results = _mapper.Map<CommentDto>(comments);

            return Ok(results);
        }

        // PUT api/Comments/5
        [HttpPut("{commentId}")]
        public async Task<ActionResult> UpdateCommentByCommentId(int commentId, [FromBody] CommentUpdateDto comment)
        {
            if (comment == null) return BadRequest();

            var commentToUpdate = _mapper.Map<Comment>(comment);
            commentToUpdate.CommentDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

            await _commentRepository.UpdateComment(commentId, commentToUpdate);

            return StatusCode(200, "Comment is updated successfully.");
        }

        // DELETE api/Comments/5
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            Comment comment = await _commentRepository.GetCommentById(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            await _commentRepository.DeleteComment(commentId);

            return Ok(comment);
        }
    }
}
