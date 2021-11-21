using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public int PetId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }

        public virtual Pet Pet { get; set; }
        public virtual AppUser UsernameNavigation { get; set; }
    }
}
