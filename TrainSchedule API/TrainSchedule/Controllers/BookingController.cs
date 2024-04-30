using Microsoft.AspNetCore.Mvc;
using TrainSchedule.Model;
using TrainSchedule.Data;
using TrainSchedule.DTO;
using AutoMapper;
namespace TrainSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IMapper mapper;
        private readonly BookingRepository repository;

        public BookingController(BookingRepository bookingRepository, IMapper _mapper)
        {
            repository = bookingRepository;
            mapper = _mapper;
        }

        [HttpPost]
        public ActionResult CreateBooking(BookingCreateDTO bookingCreate) 
        {
            var booking = mapper.Map<Booking>(bookingCreate);
            if (repository.CreateBooking(booking))
                return Ok();
            else 
                return BadRequest();
        }

        [HttpGet("{id}", Name = "GetBookingById")]
        public ActionResult<BookingRepository> GetBooking(int id)
        {
            var booking = repository.GetBooking(id);
            if (booking != null)
                return Ok(mapper.Map<BookingReadDTO>(booking));
            else
                return NotFound();
        }


        [HttpGet]
        public ActionResult<IEnumerable<BookingReadDTO>> GetBookins()
        {
            var bookings = repository.GetBookings();
            return Ok(mapper.Map<IEnumerable<BookingReadDTO>>(bookings));
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteBooking(int id) 
        {
            var booking = repository.GetBooking(id);
            if(booking != null)
            {
                repository.RemoveBooking(booking);
                return Ok();
            }
            else
                return NotFound();  
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBooking(int id, BookingCreateDTO CreateDTO)
        {
            var booking = mapper.Map<Booking>(CreateDTO);
            booking.Id = id;
            if (repository.UpdateBooking(booking))
                return Ok();
            else
                return NotFound();
        }
    }

}
