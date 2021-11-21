using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Comments = new HashSet<Comment>();
        }

        public int PetId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Breed { get; set; }
        public string LastSeen { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public string Username { get; set; }

        public virtual AppUser UsernameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
