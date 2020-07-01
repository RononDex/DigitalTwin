using System;
using System.Linq;
using System.Numerics;
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
            RenderEmployees(simulationSystem);
            RenderOpenPickingTours(simulationSystem);
        }

        private static void RenderOpenPickingTours(SimulationSystem simulationSystem)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            var warehouseDimensions = new Vector2(
                warehouse.WarehouseCompartments.Max(wc => wc.Location.X),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Y));

            var numberOfOpenPickingTours = warehouse.PickingTours.Count;
            Console.SetCursorPosition(0, Convert.ToInt32(warehouseDimensions.Y) + 8);
            Console.Write($"Open PickingTours: {numberOfOpenPickingTours}                   ");
        }

        private static void RenderEmployees(SimulationSystem simulationSystem)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            foreach (var employee in warehouse.Employees)
            {
                Console.SetCursorPosition(
                    Convert.ToInt32(employee.CurrentLocation.X) + 4,
                    Convert.ToInt32(employee.CurrentLocation.Y) + 4);
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("o");
                Console.ForegroundColor = color;
            }
        }

        private static void RenderWarehouse(SimulationSystem simulationSystem)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            foreach (var warehouseCompartment in warehouse.WarehouseCompartments)
            {
                if (warehouseCompartment.Location.Z == 0)
                {
                    Console.SetCursorPosition(
                        Convert.ToInt32(warehouseCompartment.Location.X) + 4,
                        Convert.ToInt32(warehouseCompartment.Location.Y) + 4);
                    Console.Write("□");
                }
            }

            var warehouseDimensions = new Vector2(
                warehouse.WarehouseCompartments.Max(wc => wc.Location.X),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Y));
            for (var x = 3; x <= warehouseDimensions.X + 7; x++)
            {
                Console.SetCursorPosition(x, 3);
                if (x == 3 || x == warehouseDimensions.X + 7)
                {
                    Console.Write("+");
                }
                else
                {
                    Console.Write("-");
                }

                Console.SetCursorPosition(x, Convert.ToInt32(warehouseDimensions.Y) + 6);
                if (x == 3 || x == warehouseDimensions.X + 7)
                {
                    Console.Write("+");
                }
                else
                {
                    Console.Write("-");
                }
            }
            for (var y = 4; y < warehouseDimensions.Y + 6; y++)
            {
                Console.SetCursorPosition(3, y);
                Console.Write("|");

                Console.SetCursorPosition(7 + Convert.ToInt32(warehouseDimensions.X), y);
                Console.Write("|");
            }
        }
    }
}