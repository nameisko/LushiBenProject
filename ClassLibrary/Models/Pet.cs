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
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string PetImage { get; set; }
        public string Breed { get; set; }
        public string LastSeen { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
