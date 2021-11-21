using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.DTOs
{
    public class CommentCreateDto
    {
        public string Commenter { get; set; }
        public string Comment1 { get; set; }
        public int PetId { get; set; }
    }
}
