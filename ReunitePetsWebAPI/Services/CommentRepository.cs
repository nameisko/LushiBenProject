using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public class CommentRepository: ICommentRepository
    {
        private ReunitePetsDbContext _context;
        public CommentRepository(ReunitePetsDbContext context)
        {
            _context = context;
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

        public async Task UpdateComment(int commentId, Comment comment)
        {
            Comment commentToUpdate = await _context.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);

            if (commentToUpdate != null)
            {
                commentToUpdate.Content = comment.Content;
                commentToUpdate.CommentDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
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
