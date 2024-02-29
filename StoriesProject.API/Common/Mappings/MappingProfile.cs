using AutoMapper;
using StoriesProject.API.Model.BaseEntity;
using StoriesProject.API.Models.ViewModel.Accountant;

namespace StoriesProject.API.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountantRegister, Accountant>();
        }
    }
}
