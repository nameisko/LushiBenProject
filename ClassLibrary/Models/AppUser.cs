using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            Comments = new HashSet<Comment>();
            Pets = new HashSet<Pet>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
