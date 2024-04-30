using Microsoft.EntityFrameworkCore;
using TrainSchedule.Model;
namespace TrainSchedule.Data
{
    public class TrainRepository
    {
        private AppDBContext dBContext;
        public TrainRepository(AppDBContext context) 
        {
            dBContext = context;
        }

        public bool CreateTrain(Train train) 
        {
            if (train != null) 
            {
                dBContext.trains.Add(train);
                return Save();
            }
            else
                return false;
        }

        public bool Save()
        {
            int count = dBContext.SaveChanges();
            if (count > 0)
                return true;
            else
                return false;
        }

        public bool UpdateTrain(Train train) 
        {
            dBContext.trains.Update(train);
            return Save();
        
        }

        public bool RemoveTrain(Train train) 
        {
            dBContext.trains.Remove(train);
            return Save();
        }

        public Train GetTrain(int id) 
        {
            return dBContext.trains.FirstOrDefault(train => train.Id == id);
        }

        public IEnumerable<Train> GetTrains() 
        {
            return dBContext.trains.ToList();
        }

        public IEnumerable<Train> GetTrainsByParameters(string startLocation, string endLocation, string date)
        {
            // Filter trains based on start location, end location, and date
            return dBContext.trains.Where(train =>
                train.StartLocation == startLocation &&
                train.EndLocation == endLocation &&
                train.Date == date
            ).ToList();
        }

    }
}
