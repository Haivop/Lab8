namespace KP8.Interaction
{
    internal static class BookingManagement
    {
        public static void CarriageInteraction(Train train)
        {
            Console.WriteLine("\n----Booking Interactions----\n" +
                              "==========================");
            bool valid; bool exit;
            string? id;
            do
            {
                valid = true;
                train.DisplayCarriages();
                Console.WriteLine("\nEnter ID of carriage :");
                
                id = Console.ReadLine();
                if (id == null)
                {
                    Console.WriteLine("\nWrite name or leave!");
                    valid = false;
                }
                if(!valid) Console.Clear();
            } while (!valid);
            
            var thisCarriage = train.Carriages.FirstOrDefault(carriage => carriage.ID == id);
            if(thisCarriage == null) return;
            
            var menuActions = new Dictionary<char, Action>
            {
                { '1', () => AddBooking(thisCarriage) },
                { '2', () => RemoveBookingByPlace(thisCarriage) },
                { '3', () => { thisCarriage.DisplayAllBookings(); Console.ReadKey(); } },
                { '4', () => thisCarriage.DisplayBookingsByDay() },
                { '5', () => ChangeBooking(thisCarriage) },
                { '6', () => thisCarriage.DisplayPlaces() },
                { '7', () => exit = true }
            };
            
            
            do
            {
                exit = false; valid = true;
                
                Console.WriteLine("\n1/ To Add Booking\n" +
                                  "2/ To Remove Booking\n" +
                                  "3/ To Display All Bookings\n" +
                                  "4/ To Display Bookings By Day\n" +
                                  "5/ To Change Booking\n" +
                                  "6/ To Display Free and Set Places\n" +
                                  "7/ To Exit\n");
                
                var choice = Console.ReadKey().KeyChar;
                Console.Clear();
                
                if (menuActions.ContainsKey(choice)) menuActions[choice]();
                else valid = false;
                
                Console.Clear();
            } while (!valid || !exit);
        }
        private static void AddBooking(Carriage carriage)
        {
            ConsoleKey exit;
            do{
                bool valid;
                int place;
                int price;
                string? person;
                string? day;
                
                do
                {
                    valid = true;
                    Console.WriteLine("\nEnter name of Person who make reservation :");
                    person = Console.ReadLine();
                    if (person == null)
                    {
                        valid = false;
                    }
                } while (!valid);
                
                do
                {
                    valid = true;
                    Console.WriteLine("\nEnter booked place :");
                    var splace = Console.ReadLine();
                    if (splace == null)
                    {
                        valid = false;
                    }
                    if (!int.TryParse(splace, out place))
                    {
                        Console.WriteLine("\nEnter integer for place!");
                        valid = false;
                    }
                    if (carriage.SetPlaces.Contains(place))
                    {
                        Console.WriteLine("\nPlace is booked already!");
                        valid = false;
                    }
                    if (place > carriage.Reservations.Capacity)
                    {
                        Console.WriteLine("\nThere is no such a place!");
                        valid = false;
                    }
                } while (!valid);
                
                do
                {
                    valid = true;
                    Console.WriteLine("\nEnter price of booking :");
                    var sprice = Console.ReadLine();
                    if (!int.TryParse(sprice, out price))
                    {
                        Console.WriteLine("\nEnter floating for price!");
                        valid = false;
                    }
                } while (!valid);
                
                do
                {
                    valid = true;
                    Console.WriteLine("\nEnter the day of travel like <<dd.mm.yy>> :");
                    day = Console.ReadLine();
                    if (day == null)
                    {
                        valid = false;
                    }
                } while (!valid);
                
                carriage.AddBooking(new Booking(place, person, price, day));
                
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        private static void RemoveBookingByPlace(Carriage carriage)
        {
            ConsoleKey exit;
            do{
                bool valid;
                int place;
                
                Console.Clear();
                carriage.DisplayAllBookings();
                do
                {
                    valid = true;
                    Console.WriteLine("\nChoose booking by place :");
                    var splace = Console.ReadLine();
                    if (splace == null) valid = false;
                    if (!int.TryParse(splace, out place))
                    {
                        Console.WriteLine("\nIsn't an integer!");
                        valid = false;
                    }
                } while (!valid);
                
                var bookingToRemove = carriage.Reservations.FirstOrDefault(booking => booking.Place == place);
                if (bookingToRemove != null) carriage.RemoveBooking(bookingToRemove);
                else Console.WriteLine("\nThere is no such a booking!");
                
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        private static void ChangeBooking(Carriage carriage)
        {
            ConsoleKey exit;
            do{
                bool valid;
                int place;
                
                Console.Clear();
                carriage.DisplayAllBookings();
                do
                {
                    valid = true;
                    Console.WriteLine("\nChoose booking by place :");
                    var splace = Console.ReadLine();
                    if (splace == null) valid = false;
                    if (!int.TryParse(splace, out place))
                    {
                        Console.WriteLine("\nIsn't an integer!");
                        valid = false;
                    }
                } while (!valid);
                
                var bookingToChange = carriage.Reservations.FirstOrDefault(booking => booking.Place == place);
                if (bookingToChange != null) bookingToChange.ChangeBooking();
                else Console.WriteLine("\nThere is no such a booking!");
                    
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
    }
}