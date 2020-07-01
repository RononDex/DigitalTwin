using System;
using Simulation;

namespace DigitalTwin.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulationSystem = SetupSimulation.SetupMockedWarehouseForPresentation();

            while (true)
            {
                var timeStep = new TimeSpan(hours: 0, minutes: 0, seconds: 30);
                simulationSystem.Update(timeStep);
            }
        }
    }
}
