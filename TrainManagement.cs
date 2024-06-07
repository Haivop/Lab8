namespace KP8.Interaction
{
    internal static class TrainManagement
    {
        
        private static readonly List<Train> Railroad = new List<Train>();
        private static void TrainsDisplay()
        {
            Console.WriteLine("-----Trains----- \n");
            foreach (var train in Railroad)
            {
                train.Display();
            }
        }
        private static bool TrainNameIsUnique(string name)
        {
            foreach (var train in Railroad)
            {
                if (name == train.Name) return false;
            }
            return true;
        }
        public static void AddTrain()
        {
            ConsoleKey exit;
            do
            {
                TrainsDisplay();
                Console.WriteLine("\nEnter name of new train: ");
                var name = Console.ReadLine();
                if (name != null)
                {
                    if (TrainNameIsUnique(name))
                    {
                        Railroad.Add(new Train(name));
                    }
                    else Console.WriteLine("\nWrite unique names for each train!");
                }
                else Console.WriteLine("\nWrite name or leave!");
                
                Console.Clear();
                Console.Write("\nNew List OF");
                TrainsDisplay();
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        public static void RemoveTrain()
        {
            ConsoleKey exit;
            do
            {
                TrainsDisplay();
                Console.WriteLine("\nEnter name of train you want remove: ");
                var name = Console.ReadLine();
                
                var prevCount = Railroad.Count;
                if (name != null)
                {
                    Railroad.RemoveAll(train => train.Name == name && train.Carriages.Count == 0);
                }
                else Console.WriteLine("\nWrite name or leave!");
                
                if (Railroad.Count == prevCount) Console.WriteLine("There is no train with that name or train " +
                                                                   "you have mentioned has carriages!");
                else
                {
                    Console.Clear();
                    Console.Write("\nNew List Of");
                    TrainsDisplay();
                }
                
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        public static void TrainSearch()
        {
            ConsoleKey exit;
            do
            {
                TrainsDisplay();
                Console.WriteLine("\nEnter name of new train: ");
                var name = Console.ReadLine();
                if (name != null)
                {
                    Console.Clear();
                    var train = Railroad.FirstOrDefault(train => train.Name == name);
                    Console.WriteLine(Railroad.Any(train => train.Name == name) && train != null ? 
                        $"Train '{name}' has {train.Carriages.Count} carriages and " +
                        $"{train.Carriages.Sum(carriage => carriage.Reservations.Count)} bookings." 
                        : 
                        $"No train found with name '{name}'.");
                }
                else Console.WriteLine("\nWrite name or leave!");
                
                do
                {
                    Console.WriteLine("\nPress \"ENTER\" to Continue OR \"ESC\" to Exit");
                    exit = Console.ReadKey().Key;
                } while (exit != ConsoleKey.Escape && exit != ConsoleKey.Enter);
                Console.Clear();
            } while (exit != ConsoleKey.Escape);
        }
        public static void TrainInteraction()
        {
            Console.WriteLine("----Train Interactions----\n" +
                              "==========================");
            bool valid; bool exit;
            string? name;
            do
            {
                valid = true;
                TrainsDisplay();
                Console.WriteLine("\nEnter name of train :");

                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("\nWrite name or leave!");
                    valid = false;
                }
                if (!valid) Console.Clear();
            } while (!valid);

            var thisTrain = Railroad.FirstOrDefault(train => train.Name == name);
            if (thisTrain == null) return;

            var menuActions = new Dictionary<char, Action>
            {
                { '1', () => CarriageManagement.AddCarriage(thisTrain) },
                { '2', () => CarriageManagement.RemoveCarriage(thisTrain) },
                { '3', () => { thisTrain.DisplayCarriages(); Console.ReadKey(); } },
                { '4', () => BookingManagement.CarriageInteraction(thisTrain) },
                { '5', () => exit = true }
            };

            do
            {
                exit = false; valid = true;

                Console.WriteLine("\n1/ To Add Carriage\n" +
                                  "2/ To Remove Carriage\n" +
                                  "3/ To Show Carriages with Booking percent\n" +
                                  "4/ To Interact with Bookings\n" +
                                  "5/ To Exit\n");

                var choice = Console.ReadKey().KeyChar;
                Console.Clear();

                if (menuActions.ContainsKey(choice)) menuActions[choice]();
                else valid = false;

                Console.Clear();
            } while (!valid || !exit);
        }
    }
}