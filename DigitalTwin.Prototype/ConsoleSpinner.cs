using System;

namespace DigitalTwin.Prototype
{
    public class ConsoleSpinner
    {
        private DateTime LastUpdate = DateTime.Now;
        int counter;

        public void Turn()
        {
            if (DateTime.Now - LastUpdate > new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 0, milliseconds: 100))
            {
                counter++;
                LastUpdate = DateTime.Now;
            }
            Console.SetCursorPosition(0, 0);
            switch (counter % 4)
            {
                case 0: Console.Write("/"); counter = 0; break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
        }
    }
}