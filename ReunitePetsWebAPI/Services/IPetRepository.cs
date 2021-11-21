using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public interface IPetRepository
    {
        Task<bool> PetExists(int petId);
        Task<IEnumerable<Pet>> GetPets();
        Task<Pet> GetPetById(int petId, bool includeComments);
        Task<Pet> AddPet(Pet pet);
        Task UpadatePetByPetId(int petId, Pet pet);
        Task DeletePet(int petId);

        Task<bool> Save();
    }
}
