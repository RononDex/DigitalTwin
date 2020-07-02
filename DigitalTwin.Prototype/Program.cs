using System;
using System.Threading;

namespace DigitalTwin.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting simulation...");
            var simulationSystem = SetupSimulation.SetupMockedWarehouseForPresentation();

            Console.Clear();
            var dateTimeFromLastUpdate = DateTime.Now;
            while (true)
            {
                var timeStep = (DateTime.Now - dateTimeFromLastUpdate);
                simulationSystem.Update(timeStep * 4);
                ConsoleOutputRenderer.RenderCurrentState(simulationSystem);
                dateTimeFromLastUpdate = DateTime.Now;
                Thread.Sleep(10);
            }
        }
    }
}
