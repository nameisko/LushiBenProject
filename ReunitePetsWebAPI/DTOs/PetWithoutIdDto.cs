using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.DTOs
{
    public class PetWithoutIdDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Breed { get; set; }
        public string LastSeen { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
    }
}
