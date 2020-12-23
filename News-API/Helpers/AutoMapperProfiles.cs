using System.Linq;
using AutoMapper;
using News_API.Dtos.User;
using News_API.Dtos.Photos;
using News_API.Models;
using News_API.Dtos.News;

namespace News_API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            // User
            CreateMap<UserRegisterDto,User>();
            CreateMap<User,UserForDetailsDto>();

            //Photos
            CreateMap<Photo,PhotoForDetailsDto>();
            CreateMap<PhotoForCreateDto,Photo>();
            CreateMap<Photo,PhotoForReturnDto>();
            //News
            CreateMap<New,NewsForDetailsDto>()
            .ForMember(dest=>dest.PhotoURL,opt=>{opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);});
            CreateMap<New,NewsForListDto>()
            .ForMember(dest=>dest.PhotoURL,opt=>{opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);});
            CreateMap<AddNewsDto,New>();
            CreateMap<UpdateNewsDto,New>();

           
        }
    }
}