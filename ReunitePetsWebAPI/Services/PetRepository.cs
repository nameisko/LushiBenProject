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
                    .Where(p => p.PetId == petId);
            }
            else
            {
                result = _context.Pets.Where(p => p.PetId == petId);
            }
            result = _context.Pets.Where(p => p.PetId == petId);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Pet> AddPet(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            _context.Entry(pet).GetDatabaseValues();

            return pet;
        }
        public async void DeletePet(int petId)
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


        public Task AddCommentForPet(int petId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }


        public Task<Comment> GetCommentForPet(int petId, int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetCommentsByPetId(int petId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
