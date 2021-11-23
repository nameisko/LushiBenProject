using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.DTOs
{
    public class CommentUpdateDto
    {
        public int PetId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
