using AutoMapper;
using ClassLibrary.Models;
using ReunitePetsWebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReunitePetsWebAPI.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Pet, PetWithoutCommentsDto>();
            CreateMap<PetWithoutCommentsDto, Pet>();
            CreateMap<PetWithoutIdDto, Pet>();
            CreateMap<Pet, PetDto>();
            CreateMap<PetWithoutIdDto, Pet>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<UserCreateDto, AppUser>();
            CreateMap<UserLoginDto, AppUser>();
            CreateMap<AppUser, UserWithoutPasswordDto>();
            CreateMap<CommentUpdateDto, Comment>();
        }
    }
}
