using System;

namespace DigitalTwin.Prototype
{
    public class ConsoleSpinner
    {
        int counter;

        public void Turn()
        {
            counter++;
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