using AutoMapper;
using CyberSecurityProject.Data.Migrations;
using CyberSecurityProject.Models;

namespace CyberSecurityProject.Mappers
{
    public class MealProfile:Profile
    {
        public MealProfile()
        {
            CreateMap<MealViewModel, Meal>().ReverseMap();
        }
    }
}
