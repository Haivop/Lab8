namespace KP8.Interaction
{
    internal static class CarriageManagement
    {
        private static readonly List<Carriage> UnsettledCarriages = new List<Carriage>();
        private static readonly List<string> SettledID = new List<string>();
        public static void UnsettledCarriageInteraction()
        {
            bool valid; bool exit;
            do
            {
                exit = false; valid = true;

                var menuActions = new Dictionary<char, Action>()
                {
                    {'1', () => CreateCarriage()} ,
                    {'2', () => DemolishCarriage()} ,
                    {'3', () => exit = true}
                };
                
                
                Console.WriteLine("1/ To Create Carriage\n" +
                                  "2/ To Demolish Carriage\n" +
                                  "3/ To Exit");
                
                var choice = Console.ReadKey().KeyChar;
                Console.Clear();
                
                if (menuActions.ContainsKey(choice)) menuActions[choice]();
                else valid = false;
                
                Console.Clear();
            } while (!valid || !exit);
        }
        public static void AddCarriage(Train train)
        {
            ConsoleKey exit;
            bool valid;
            string? id;
            do{
                DisplayUnsettledCarriages();
                do
                {
                    valid = true;
                    Console.WriteLine("\nIs there carriage you want to add ? (ENTER | ESC)");
                    exit = Console.ReadKey().Key;
                    switch (exit)
                    {
                        case ConsoleKey.Enter:
                            break;
                        case ConsoleKey.Escape:
                            return;
                        default:
                            valid = false;
                            break;
                    }
                } while (!valid);

                do
                {
                    valid = true;
                    Console.WriteLine("\nChoose carriage by ID :");
                    id = Console.ReadLine();
                    if (id == null) valid = false;
                } while (!valid);
                
                var carriageToAdd = UnsettledCarriages.FirstOrDefault(carriage => carriage.ID == id && carriage.Reservations.Count == 0);

                if (carriageToAdd != null)
                {
                    train.AddCarriage(carriageToAdd);
                    UnsettledCarriages.Remove(carriageToAdd);
                }
                else Console.WriteLine("\nThere is no such a carriage!");
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        public static void RemoveCarriage(Train train)
        {
            ConsoleKey exit;
            bool valid;
            string? id;
            do
            {
                train.DisplayCarriages();
                do
                {
                    valid = true;
                    Console.WriteLine("Is there carriage you want to remove ? (ENTER | ESC)");
                    exit = Console.ReadKey().Key;
                    switch (exit)
                    {
                        case ConsoleKey.Enter:
                            break;
                        case ConsoleKey.Escape:
                            return;
                        default:
                            valid = false;
                            break;
                    }
                } while (!valid);
                
                do
                {
                    valid = true;
                    Console.WriteLine("Choose carriage by number :");
                    id = Console.ReadLine();
                    if (id == null) valid = false;
                } while (!valid);
                var carriageToRemove = train.Carriages.FirstOrDefault(carriage => carriage.ID == id && carriage.Reservations.Count == 0);

                if (carriageToRemove != null)
                {
                    train.RemoveCarriage(carriageToRemove);
                    UnsettledCarriages.Add(carriageToRemove);
                }
                Console.WriteLine("There is no such a carriage!");
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        private static void CreateCarriage()
        {
            ConsoleKey exit;
            bool valid;
            string? id;
            do
            {
                DisplayUnsettledCarriages();
                do
                {
                    valid = true;
                    Console.WriteLine("\nEnter unique ID: ");
                    id = Console.ReadLine();
                    if (id == null || SettledID.Contains(id))
                    {
                        Console.WriteLine("Not a unique ID!");
                        valid = false;
                    }
                }while (!valid);

                do
                {
                    valid = true;
                    Console.WriteLine("Enter capacity: ");
                    var valueCapacity = Console.ReadLine();
                    if (!int.TryParse(valueCapacity, out var capacity) || valueCapacity == null)
                    {
                        Console.WriteLine("Not a number!");
                        valid = false;
                    }
                    else
                    {
                        Console.Clear();
                        SettledID.Add(id);
                        UnsettledCarriages.Add(new Carriage(id, capacity));
                        Console.WriteLine("New List OF Unsettled Carriages");
                        DisplayUnsettledCarriages();
                    }
                }while (!valid);
                
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        private static void DemolishCarriage()
        {
            ConsoleKey exit;
            do
            {
                DisplayUnsettledCarriages();
                Console.WriteLine("\nEnter ID of carriage you want demolish: ");
                var id = Console.ReadLine();
                
                var prevCount = UnsettledCarriages.Count;
                if (id != null)
                {
                    UnsettledCarriages.RemoveAll(carriage => carriage.ID == id);
                    SettledID.RemoveAll(ID => ID == id);
                }
                else Console.WriteLine("\nWrite name or leave!");
                
                if (UnsettledCarriages.Count == prevCount) Console.WriteLine("There is no train with that name!");
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nNew List Of Unsettled Carriages");
                    DisplayUnsettledCarriages();
                }
                
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        private static void DisplayUnsettledCarriages()
        {
            Console.WriteLine();
            foreach(var carriage in UnsettledCarriages) carriage.Display();
        }
    }
}