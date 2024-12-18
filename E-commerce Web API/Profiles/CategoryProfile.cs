using AutoMapper;
using E_commerce_Web_API.DTOs;
using E_commerce_Web_API.Models;

namespace E_commerce_Web_API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
