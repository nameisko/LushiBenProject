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
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            var pets = await _petRepository.GetPets();

            var results = _mapper.Map<IEnumerable<PetWithoutCommentsDto>>(pets);

            return Ok(results);
        }

        // GET: api/Pets/5
        [HttpGet("{petId}")]
        public async Task<ActionResult<Pet>> GetPetById(int petId)
        {
            var pet = await _petRepository.GetPetById(petId, true);

            if(pet == null)
            {
                return NotFound();
            }

            var petWithoutCommentsResult = _mapper.Map<PetWithoutCommentsDto>(pet);

            return Ok(petWithoutCommentsResult);
        }

        // POST api/Pets
        [HttpPost]
        public async Task<ActionResult<Pet>> AddPet([FromBody] PetWithoutIdDto pet)
        {
            if (pet == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var petToInsert = _mapper.Map<Pet>(pet);

            var petInserted = await _petRepository.AddPet(petToInsert);

            if (!await _petRepository.Save())
            {
                return StatusCode(500, "A problem occured while processing the request");
            }
            return CreatedAtAction(nameof(GetPetById), new { petId = petInserted.PetId, includeComments = false}, petInserted);
        }

        // DELETE api/Pets/5
        [HttpDelete("{petId}")]
        public async Task<ActionResult<Pet>> DeletePet(int petId)
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
        public async Task<ActionResult<Pet>> UpadatePetStatusByPetId(int petId, [FromBody] PetWithoutIdDto pet)
        {
            var petUpdateInfo = _mapper.Map<Pet>(pet);

            await _petRepository.UpadatePetByPetId(petId, petUpdateInfo);

            return StatusCode(200, "Pet's status is updated successfully.");
        }
    }
}
