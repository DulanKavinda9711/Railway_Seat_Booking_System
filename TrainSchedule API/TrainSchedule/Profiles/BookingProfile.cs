using AutoMapper;
using TrainSchedule.Model;
using TrainSchedule.DTO;
namespace TrainSchedule.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile() 
        {
            CreateMap<Booking, BookingReadDTO>();
            CreateMap<BookingCreateDTO, Booking>();
        }

    }
}
