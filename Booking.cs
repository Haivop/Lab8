namespace KP8.Interaction
{
    internal class Booking
    {
        private string Person { get; set; }
        public string Day { get; private set; }
        public int Place { get; private set; }
        private int Price { get; set; }
        
        public Booking(int place, string fullname, int price, string day)
        {
            Place = place;
            Person = fullname;
            Price = price;
            Day = day;
        }
        public void Display()
        {
            Console.WriteLine($"Day: {Day} Person : {Person} " +
                              $"Place : {Place} Price : {Price}");
        }
        public void ChangeBooking()
        {
            ConsoleKey exit;
            bool valid;
            do
            {
                valid = true;
                
                Console.WriteLine("1/ To Change Person\n" +
                                  "2/ To Change Day\n" +
                                  "3/ To Change Place\n" +
                                  "4/ To Change Price");
                Console.WriteLine("Enter new:");
                string? newValue = Console.ReadLine();
                if (newValue == null)
                {
                    Console.WriteLine("ERROR : Null reference");
                    return;
                }

                var menuActions = new Dictionary<char, Action>()
                {
                    { '1', () => Person = newValue },
                    { '2', () => Day = newValue },
                    { '3', () => Place = int.Parse(newValue) },
                    { '4', () => Price = int.Parse(newValue) },
                };
                
                do
                {
                    var choice = Console.ReadKey().KeyChar;
                    if (menuActions.ContainsKey(choice)) menuActions[choice]();
                    else
                    {
                        Console.WriteLine("Wrong choice!");
                        valid = false;
                    }
                } while (!valid);
                
                do
                {
                    Console.WriteLine("Press \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Spacebar);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
    }
}