using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public class PetRepository : IPetRepository
    {
        private ReunitePetsDbContext _context;
        public PetRepository(ReunitePetsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> PetExists(int petId)
        {
            return await _context.Pets.AnyAsync<Pet>(p => p.PetId == petId);
        }

        public async Task<IEnumerable<Pet>> GetPets()
        {
            var result = _context.Pets.OrderBy(p => p.PetId);
            return await result.ToListAsync();
        }

        public async Task<Pet> GetPetById(int petId, bool includeComments)
        {
            IQueryable<Pet> result;

            if (includeComments)
            {
                result = _context.Pets.Include(c => c.Comments)
                .Where(c => c.PetId == petId);
            }
            else result = _context.Pets.Where(c => c.PetId == petId);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Pet> AddPet(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            _context.Entry(pet).GetDatabaseValues();

            return pet;
        }
        public async Task DeletePet(int petId)
        {
            Pet petToRemove = await _context.Pets.SingleOrDefaultAsync(p => p.PetId == petId);

            if(petToRemove != null)
            {
                _context.Pets.Remove(petToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpadatePetStatusByPetId(int petId, string status)
        {
            Pet petToUpdate = await _context.Pets.SingleOrDefaultAsync(p => p.PetId == petId);

            if(petToUpdate != null)
            {
                petToUpdate.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPetId(int petId)
        {
            IQueryable<Comment> result = _context.Comments.Where(p => p.PetId == petId);
            return await result.ToListAsync();
        }


        public async Task<Comment> AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            _context.Entry(comment).GetDatabaseValues();

            return comment;
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _context.Comments.Where(c => c.CommentId == commentId).FirstOrDefaultAsync();
        }

        public async Task DeleteComment(int commentId)
        {
            Comment commentToRemove = await _context.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);

            if (commentToRemove != null)
            {
                _context.Comments.Remove(commentToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
