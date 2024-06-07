using KP8.Interaction;

namespace KP8
{
    class Program
    {
        private static void Main(string[] args)
        {
            var exit = false;
            Console.WriteLine("This is a program for railroad checkout\n");
            
            var menuActions = new Dictionary<char, Action>
            {
                { '1', TrainManagement.AddTrain },
                { '2', TrainManagement.RemoveTrain },
                { '3', TrainManagement.TrainSearch },
                { '4', TrainManagement.TrainInteraction },
                { '5', CarriageManagement.UnsettledCarriageInteraction },
                { '6', () => exit = true }
            };

            do
            {
                Console.WriteLine("1/ To Add Train\n" +
                                  "2/ To Remove Train\n" +
                                  "3/ To Search Train\n" +
                                  "4/ To Interact with Train\n" +
                                  "5/ To Interact with Unsettled Carriages\n" +
                                  "6/ To Exit");

                var choice = Console.ReadKey().KeyChar;
                Console.Clear();
                
                if (menuActions.ContainsKey(choice)) menuActions[choice]();
                else Console.WriteLine("Invalid option, please try again.");

                Console.Clear();
            } while (!exit);
        }
    }
}