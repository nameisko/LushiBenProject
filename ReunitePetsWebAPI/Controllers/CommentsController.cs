using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private IPetRepository _petRepository;

        public CommentsController(IPetRepository petRepository, IMapper mapper)
        {
            _mapper = mapper;
            _petRepository = petRepository;
        }

        // GET: /Comments?petId=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPetId(int petId)
        {
            var comments = await _petRepository.GetCommentsByPetId(petId);

            var results = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] CommentCreateDto comment)
        {
            if (comment == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var commentToInsert = _mapper.Map<Comment>(comment);
            commentToInsert.CommentDate = DateTime.Now;

            await _petRepository.AddComment(commentToInsert);

            if (!await _petRepository.Save())
            {
                return StatusCode(500, "A problem occured while processing the request");
            }

            return StatusCode(200, "Comment is submitted successfully.");
        }

        // GET: /Comments/1
        [HttpGet("{commentId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsById(int commentId)
        {
            var comments = await _petRepository.GetCommentById(commentId);

            var results = _mapper.Map<CommentDto>(comments);

            return Ok(results);
        }

        // DELETE api/Comments/5
        [HttpDelete("{commentId}")]
        public async Task<ActionResult<Pet>> DeleteComment(int commentId)
        {
            Comment comment = await _petRepository.GetCommentById(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            await _petRepository.DeleteComment(commentId);

            return Ok(comment);
        }
    }
}
