using AutoMapper;
using TrainSchedule.Model;
using TrainSchedule.DTO;
namespace TrainSchedule.Profiles
{
    public class TrainProfile : Profile
    {
        public TrainProfile() 
        {
            CreateMap<Train, TrainReadDTO>();
            CreateMap<TrainCreateDTO, Train>();
        }
    }
}
