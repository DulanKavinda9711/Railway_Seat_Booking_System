using TrainSchedule.Model;
using Microsoft.EntityFrameworkCore;
namespace TrainSchedule.Data
{
    public class BookingRepository
    {
        private AppDBContext dBContext;

        public BookingRepository(AppDBContext context) 
        {
            dBContext = context;
        }

        public bool CreateBooking(Booking booking) 
        {
            if (booking != null) 
            {
                dBContext.bookings.Add(booking);
                return Save();
            }
            else
                return false;
        }

        public bool Save()
        {
            int count = dBContext.SaveChanges();
            if(count > 0)
                return true;
            else
                return false;
        }

        public bool UpdateBooking(Booking booking) 
        {
            dBContext.bookings.Update(booking);
            return Save();
        }

        public bool RemoveBooking(Booking booking)
        {
            dBContext.bookings.Remove(booking);
            return Save();
        }

        public Booking GetBooking(int id) 
        {
            return dBContext.bookings.FirstOrDefault(booking => booking.Id == id);
        }

        public IEnumerable<Booking> GetBookings() 
        {
            return dBContext.bookings.ToList();
        }


    }
}
