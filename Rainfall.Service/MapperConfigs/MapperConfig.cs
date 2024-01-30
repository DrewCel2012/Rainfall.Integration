using AutoMapper;
using Rainfall.Model;
using Rainfall.Service.DomainModels;

namespace Rainfall.Service.MapperConfigs
{
    public class MapperConfig
    {
        public static IMapper InitializeRainfallReadingMapper
        {
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FloodMonitoringReadingsDomain, RainfallReadingResponse>()
                    .ForMember(dest => dest.Readings, opt => opt.MapFrom(src => src.items));

                    cfg.CreateMap<Item, RainfallReading>()
                    .ForMember(dest => dest.DateMeasured, opt => opt.MapFrom(src => src.dateTime))
                    .ForMember(dest => dest.AmountMeasured, opt => opt.MapFrom(src => src.value));
                }).CreateMapper();
            }
        }
    }
}
