using Simulation;

namespace DigitalTwin.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulationSystem = new SimulationSystem();

            // Add a thread and a simulation engine to the simulation framework
            // system.AddSimulationThread(new SimulationThread(new SimulationEngine[] { new GravityEngine() }.ToList()));
        }
    }
}
