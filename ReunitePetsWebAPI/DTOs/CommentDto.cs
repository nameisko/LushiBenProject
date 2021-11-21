using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.DTOs
{
    public class CommentDto
    {

        public int CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public int PetId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
