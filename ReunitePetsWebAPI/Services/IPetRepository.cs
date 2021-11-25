using ClassLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public interface IPetRepository
    {
        Task<bool> PetExists(int petId);
        Task<IEnumerable<Pet>> GetPets();
        Task<Pet> GetPetById(int petId, bool includeComments);

        Task<IEnumerable<Pet>> GetPetsByTypeAndStatus(string type, string status);
        Task<Pet> AddPet(Pet pet);
        Task UpadatePetByPetId(int petId, Pet pet);
        Task DeletePet(int petId);

        Task<bool> Save();
    }
}
