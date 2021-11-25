using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
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
            var result = _context.Pets.OrderBy(p => p.PostDate);
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

        public async Task<IEnumerable<Pet>> GetPetsByTypeAndStatus(string type, string status)
        {
            IQueryable<Pet> result;

            if (type == "All" && status == "All")
            {
                result = _context.Pets.OrderBy(p => p.PostDate);
            }
            else if(type == "All")
            {
                 result = _context.Pets.Where(p => p.Status == status);
            }
            else if(status == "All")
            {
                result = _context.Pets.Where(p => p.Type == type);
            }
            else
            {
                result = _context.Pets.Where(p => p.Type == type && p.Status == status);
            }

            return await result.ToListAsync();
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

        public async Task UpadatePetByPetId(int petId, Pet pet)
        {
            Pet petToUpdate = await _context.Pets.SingleOrDefaultAsync(p => p.PetId == petId);
            Pet newPetInfo = new Pet();
            if(petToUpdate != null)
            {
                newPetInfo.PetId = petToUpdate.PetId;
                newPetInfo.Username = petToUpdate.Username;
                newPetInfo.Name = pet.Name;
                newPetInfo.Status = pet.Status;
                newPetInfo.Type = pet.Type;
                newPetInfo.Image = pet.Image;
                newPetInfo.Breed = pet.Breed;
                newPetInfo.LastSeen = pet.LastSeen;
                newPetInfo.Description = pet.Description;
                newPetInfo.Contact = pet.Contact;
                _context.Entry(petToUpdate).CurrentValues.SetValues(newPetInfo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
