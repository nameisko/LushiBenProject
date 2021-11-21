using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Commenter { get; set; }
        public DateTime CommentDate { get; set; }
        public string Comment1 { get; set; }
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
