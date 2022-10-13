using AutoMapper;
using DevFest22Asyut.Dtos;
using DevFest22Asyut.Models;

namespace DevFest22Asyut.Helpers
{
    public class DbContextProfile : Profile 
    {
        public DbContextProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(des => des.Image,
                opts => opts.MapFrom(src => Constants.BaseUrl + src.Image));

            CreateMap<ContactInfo, ContactInfoDto>();

        }
    }
}
