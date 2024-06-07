namespace KP8.Interaction
{
    internal class Train
    {
        public List<Carriage> Carriages;
        private int _index = 1; 
        
        public string Name { get; set; }

        public Train(string name)
        {
            Name = name;
            Carriages = new List<Carriage>();
        }

        public void Display()
        {
            Console.WriteLine($"Name : {this.Name} | Carriages : {this.Carriages.Count}");
        }

        public void AddCarriage(Carriage car)
        {
            Carriages.Add(car);
            car.SettledIndex = _index;
            _index++;
        }

        public void RemoveCarriage(Carriage car)
        {
            Carriages.Remove(car);
            foreach (var carriage in Carriages)
            {
                if (car.SettledIndex < carriage.SettledIndex) carriage.SettledIndex--;
            }
            _index--;
        }
        
        public void DisplayCarriages()
        {
            if (Carriages.Count != 0)
            {
                foreach (var carriage in Carriages)
                {
                    carriage.Display();
                }
            }
        }
    }
}
