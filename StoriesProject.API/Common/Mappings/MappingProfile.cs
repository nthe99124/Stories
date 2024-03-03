using AutoMapper;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel.Accountant;

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
