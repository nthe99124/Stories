using AutoMapper;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel.Accountant;
using StoriesProject.Model.ViewModel.Story;

namespace StoriesProject.API.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountantRegister, Accountant>();
            CreateMap<AuthorRegisterModel, AuthorRegister>();
            CreateMap<Story, StoryAccountGeneric>();
            CreateMap<StoryRegisterVM, Story>();
            CreateMap<Story, StoryGeneric>();
        }
    }
}
