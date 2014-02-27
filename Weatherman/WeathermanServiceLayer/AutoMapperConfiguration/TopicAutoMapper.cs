using System;
using AutoMapper;
using WeathermanServiceLayer.ViewModels;

//namespace WeathermanServiceLayer.AutoMapperConfiguration
//{



//    public class TopicAutoMapperConfiguration : Profile
//    {
//        protected override void Configure()
//        {
//            Mapper.CreateMap<WeathermanDataLayer.Topic, TopicDisplayViewModel>()
//                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.TopicNumber.ToString()))
//                .ForMember(dest => dest.GuidId, opt => opt.MapFrom(src => src.GuidId));


//            Mapper.CreateMap<WeathermanDataLayer.Topic, EditTopicViewModel>()
//    .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.TopicNumber))
//    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
//        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
//    .ForMember(dest => dest.GuidId, opt => opt.MapFrom(src => src.GuidId)).ReverseMap();


//            Mapper.CreateMap<TopicCreateViewModel, WeathermanDataLayer.Topic>()
//                .ForMember(dest => dest.TopicNumber, opt => opt.MapFrom(src => src.Number))
//                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
//                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
//                .ForMember(dest => dest.GuidId, opt => opt.UseValue(Guid.NewGuid()));





//        }
//    }
//}