using AutoMapper;
using TodoApp.Domain.Entities;
using TodoApp.Domain.InputModels;
using TodoApp.Domain.ViewModel;

namespace TodoApp.API.Registers.MapProfiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, TodoViewModel>().ReverseMap();

            CreateMap<Todo, TodoInputModel>().ReverseMap();
        }
    }
}
