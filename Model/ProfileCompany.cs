using AutoMapper;

namespace Fora.Model
{
    public class ProfileCompany : Profile
    {
        public ProfileCompany()
        {
            CreateMap<EdgarCompanyData, CompanyOutput>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.EntityName))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Cik))
            ;
        }
    }
}
