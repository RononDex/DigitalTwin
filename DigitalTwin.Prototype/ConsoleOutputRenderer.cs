using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype
{
    public static class ConsoleOutputRenderer
    {
        private static ConsoleSpinner Spinner = new ConsoleSpinner();

        private static List<List<char>> oldRender;

        public static void RenderCurrentState(SimulationSystem simulationSystem)
        {
            var currentRender = new List<List<char>>();

            for (var i = 0; i < 50; i++)
            {
                currentRender.Add(new List<char>());
                for (var j = 0; j < 50; j++)
                {
                    currentRender[i].Add(' ');
                }
            }
            if (oldRender == null) oldRender = CopyList(currentRender);

            Console.CursorVisible = false;
            Spinner.Turn();
            RenderWarehouse(simulationSystem, currentRender);
            RenderEmployees(simulationSystem, currentRender);
            RenderOpenPickingTours(simulationSystem);
            RenderWarehouseDimensions(simulationSystem);
            RenderWarehouseDelta(currentRender);
            oldRender = CopyList(currentRender);
        }

        private static List<List<char>> CopyList(List<List<char>> currentRender)
        {
            var copiedList = new List<List<char>>();
            foreach (var sublist in currentRender)
            {
                copiedList.Add(new List<char>(sublist));
            }
            return copiedList;
        }

        private static void RenderWarehouseDelta(List<List<char>> currentRender)
        {
            for (var x = currentRender.Count - 1; x >= 0; x--)
            {
                for (var y = currentRender.Count - 1; y >= 0; y--)
                {
                    if (oldRender[x][y] != currentRender[x][y])
                    {
                        if (currentRender[x][y] == 'o')
                        {
                            var color = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(x, y);
                            Console.Write(currentRender[x][y]);
                            Console.ForegroundColor = color;
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write(currentRender[x][y]);
                        }
                    }
                }
            }
        }

        private static void RenderWarehouseDimensions(SimulationSystem simulationSystem)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            var warehouseDimensions = new Vector3(
                warehouse.WarehouseCompartments.Max(wc => wc.Location.X),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Y),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Z));

            Console.SetCursorPosition(0, Convert.ToInt32(warehouseDimensions.Y) + 9);
            Console.Write($"Warehouse Dimensions:    X: {warehouseDimensions.X}    Y: {warehouseDimensions.Y}    Z: {warehouseDimensions.Z}");
        }

        private static void RenderOpenPickingTours(SimulationSystem simulationSystem)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            var warehouseDimensions = new Vector2(
                warehouse.WarehouseCompartments.Max(wc => wc.Location.X),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Y));

            var numberOfOpenPickingTours = warehouse.PickingTours;
            Console.SetCursorPosition(0, Convert.ToInt32(warehouseDimensions.Y) + 10);
            Console.WriteLine($"Picking Tours(new): {numberOfOpenPickingTours.Count(p => p.State == PickingTour.PickingTourState.New)}");
            Console.WriteLine($"Picking Tours(in progress): {numberOfOpenPickingTours.Count(p => p.State == PickingTour.PickingTourState.InProgress)}");
            Console.WriteLine($"Picking Tours(finished): {numberOfOpenPickingTours.Count(p => p.State == PickingTour.PickingTourState.Finished)}");
        }

        private static void RenderEmployees(SimulationSystem simulationSystem, List<List<char>> currentRender)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();
            foreach (var employee in warehouse.Employees)
            {
                currentRender[Convert.ToInt32(employee.CurrentLocation.X) + 4][Convert.ToInt32(employee.CurrentLocation.Y) + 4] = 'o';
            }

            var warehouseDimensions = new Vector2(
                warehouse.WarehouseCompartments.Max(wc => wc.Location.X),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Y));
            Console.SetCursorPosition(0, Convert.ToInt32(warehouseDimensions.Y) + 13);
            Console.WriteLine($"Employees(busy): {warehouse.Employees.Count(e => e.PickingTour != null)}");
            Console.WriteLine($"Employees(soon to be unemployed): {warehouse.Employees.Count(e => e.PickingTour == null)}");
        }

        private static void RenderWarehouse(SimulationSystem simulationSystem, List<List<char>> currentRender)
        {
            var warehouse = (Warehouse)simulationSystem.World.Objects.First();

            foreach (var warehouseCompartment in warehouse.WarehouseCompartments)
            {
                if (warehouseCompartment.Location.Z == 0)
                {
                    currentRender[Convert.ToInt32(warehouseCompartment.Location.X) + 4][Convert.ToInt32(warehouseCompartment.Location.Y) + 4] = '□';
                }
            }

            var warehouseDimensions = new Vector2(
                warehouse.WarehouseCompartments.Max(wc => wc.Location.X),
                warehouse.WarehouseCompartments.Max(wc => wc.Location.Y));
            for (var x = 3; x <= warehouseDimensions.X + 7; x++)
            {
                if (x == 3 || x == warehouseDimensions.X + 7)
                {
                    currentRender[x][3] = '+';
                }
                else
                {
                    currentRender[x][3] = '-';
                }

                if (x == 3 || x == warehouseDimensions.X + 7)
                {
                    currentRender[x][Convert.ToInt32(warehouseDimensions.Y) + 6] = '+';
                }
                else
                {
                    currentRender[x][Convert.ToInt32(warehouseDimensions.Y) + 6] = '-';
                }
            }
            for (var y = 4; y < warehouseDimensions.Y + 6; y++)
            {
                currentRender[3][y] = '|';

                currentRender[7 + Convert.ToInt32(warehouseDimensions.X)][y] = '|';
            }
        }
    }
}