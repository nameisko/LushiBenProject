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
    public class PetsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IPetRepository _petRepository;

        public PetsController(IMapper mapper, IPetRepository petRepository)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult> GetPets()
        {
            var pets = await _petRepository.GetPets();

            var results = _mapper.Map<IEnumerable<PetWithoutCommentsDto>>(pets);

            return Ok(results);
        }

        // GET: api/Pets/5
        [HttpGet("{petId}")]
        public async Task<ActionResult> GetPetById(int petId)
        {
            var pet = await _petRepository.GetPetById(petId, true);

            if (pet == null)
            {
                return NotFound();
            }

            var petWithoutCommentsResult = _mapper.Map<PetWithoutCommentsDto>(pet);

            return Ok(petWithoutCommentsResult);
        }

        // GET api/Pets/filter?type=Dog&status=Lost
        [HttpGet("filter")]
        public async Task<ActionResult> GetPetsByType(string type, string status)
        {
            var pets = await _petRepository.GetPetsByTypeAndStatus(type, status);

            var results = _mapper.Map<IEnumerable<PetWithoutCommentsDto>>(pets);

            return Ok(results);
        }

        // POST api/Pets
        [HttpPost]
        public async Task<ActionResult> AddPet([FromBody] PetWithoutIdDto pet)
        {
            if (pet == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            Pet petToInsert = _mapper.Map<Pet>(pet);

            petToInsert.PostDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

            var petInserted = await _petRepository.AddPet(petToInsert);

            if (!await _petRepository.Save())
            {
                return StatusCode(500, "A problem occured while processing the request");
            }

            return CreatedAtAction(nameof(GetPetById), new { petId = petInserted.PetId, includeComments = false }, petInserted);
        }

        // DELETE api/Pets/5
        [HttpDelete("{petId}")]
        public async Task<ActionResult> DeletePet(int petId)
        {
            Pet pet = await _petRepository.GetPetById(petId, false);

            if (pet == null)
            {
                return NotFound();
            }

            await _petRepository.DeletePet(petId);

            return Ok(pet);
        }

        // PUT api/Pets/5
        [HttpPut("{petId}")]
        public async Task<ActionResult> UpadatePetStatusByPetId(int petId, [FromBody] PetWithoutIdDto pet)
        {
            if (pet == null) return BadRequest();

            var petToUpdate = _mapper.Map<Pet>(pet);
            petToUpdate.PostDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

            await _petRepository.UpadatePetByPetId(petId, petToUpdate);


            return Ok(petToUpdate);
        }

        // POST api/Pets/UploadImage
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            string result = await S3BucketOperations.UploadImage(image);
            return Ok(result);
        }
    }
}
