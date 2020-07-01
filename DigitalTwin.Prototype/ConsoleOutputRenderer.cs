using System;
using System.Linq;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype
{
    public static class ConsoleOutputRenderer
    {
        private static ConsoleSpinner Spinner = new ConsoleSpinner();

        public static void RenderCurrentState(SimulationSystem simulationSystem)
        {
            Console.CursorVisible = false;
            Spinner.Turn();
            RenderWarehouse(simulationSystem);
        }

        private static void RenderWarehouse(SimulationSystem simulationSystem)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            foreach (var warehouseCompartment in warehouse.WarehouseCompartments)
            {
                if (warehouseCompartment.Location.Z == 0)
                {
                    Console.SetCursorPosition(
                        Convert.ToInt32(warehouseCompartment.Location.X) + 2,
                        Convert.ToInt32(warehouseCompartment.Location.Y) + 2);
                    Console.Write("□");
                }
            }

            foreach (var employee in warehouse.Employees)
            {
                Console.SetCursorPosition(
                    Convert.ToInt32(employee.CurrentLocation.X) + 2,
                    Convert.ToInt32(employee.CurrentLocation.Y) + 2);
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("o");
                Console.ForegroundColor = color;
            }
        }
    }
}