using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.DTOs
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Commenter { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment1 { get; set; }
        public int PetId { get; set; }
    }
}
