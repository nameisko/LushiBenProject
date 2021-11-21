using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Services
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPetId(int petId);
        Task<Comment> GetCommentById(int commentId);
        Task<Comment> AddComment(Comment comment);
        Task UpdateComment(int commentId, Comment comment);
        Task DeleteComment(int commentId);
        Task<bool> Save();
    }
}
