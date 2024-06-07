namespace KP8.Interaction
{
    internal class Carriage
    {
        public string ID { get; set; }
        public int SettledIndex { get; set; }
        public bool Settled { get; set; }
        public List<Booking> Reservations { get; }
        public List<int> FreePlaces { get; }
        public List<int> SetPlaces { get; }

        public Carriage(string id, int capacity)
        {
            ID = id;
            Reservations = new List<Booking>(capacity);
            FreePlaces = new List<int>(capacity);
            for (var i = 0; i < capacity; i++) FreePlaces.Add(i+1);
            SetPlaces = new List<int>(capacity);
            Settled = false;
        }
        public void Display()
        {
            var percent = Reservations.Count/Reservations.Capacity * 100;
            Console.WriteLine($"Index: {SettledIndex} ID : {ID}" + $" Free Places : {percent}%");
        }
        public void DisplayPlaces()
        {
            Console.WriteLine("Free Places : \n");
            foreach (var place in FreePlaces)
            {
                Console.Write($"{place} ");
            }
            
            Console.WriteLine("\nSet Places : \n");
            foreach (var place in SetPlaces)
            {
                Console.Write($"{place} ");
            }

            Console.ReadKey();
        }
        public void AddBooking(Booking reserv)
        {
            Reservations.Add(reserv);
            FreePlaces.Remove(reserv.Place);
            SetPlaces.Add(reserv.Place);
        }
        public void RemoveBooking(Booking reserv)
        { 
            Reservations.Remove(reserv);
            FreePlaces.Add(reserv.Place);
            SetPlaces.Remove(reserv.Place);
        }
        public void DisplayBookingsByDay()
        {
            Console.WriteLine("Enter the day in form of <<dd.mm.yy>>:");
            var day = Console.ReadLine();
            var exist = false;
            var bookingsOnDay = Reservations.Where(booking => booking.Day == day);

            foreach (var booking in bookingsOnDay)
            {
                booking.Display();
                exist = true;
            }
            if (exist) return;
            
            Console.WriteLine("No bookings on that day");
            Console.ReadKey();
        }
        public void DisplayAllBookings()
        {
            if (Reservations.Count != 0)
            {
                foreach (var booking in Reservations)
                {
                    booking.Display();
                }
            }
            else Console.WriteLine("No bookings in this carriage");
        }
    }
}
