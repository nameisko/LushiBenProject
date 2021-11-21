using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public interface IPetRepository
    {
        // Pet
        Task<bool> PetExists(int petId);
        Task<IEnumerable<Pet>> GetPets();
        Task<Pet> GetPetById(int petId, bool includeComments);
        Task<Pet> AddPet(Pet pet);
        Task UpadatePetStatusByPetId(int petId, string status);
        Task DeletePet(int petId);

        // Comment
        Task<IEnumerable<Comment>> GetCommentsByPetId(int petId);
        Task<Comment> GetCommentById(int commentId);
        Task<Comment> AddComment(Comment comment);
        Task DeleteComment(int commentId);
        Task<bool> Save();
    }
}
