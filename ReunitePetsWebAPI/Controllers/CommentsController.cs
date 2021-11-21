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
    }
}
