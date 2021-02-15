using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DigitalTwin.Prototype.Engines;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype
{
    public static class SetupSimulation
    {
        public static SimulationSystem SetupMockedWarehouseForPresentation()
        {
            var simulationSystem = new SimulationSystem();

            // Add a thread and a simulation engine to the simulation framework
            simulationSystem.AddSimulationThread(new SimulationThread(new SimulationEngine[] { new EmployeeMovementEngine() }.ToList()));
            simulationSystem.AddSimulationThread(new SimulationThread(new SimulationEngine[] { new RandomPickingTourGenerator() }.ToList()));
            simulationSystem.AddSimulationThread(new SimulationThread(new SimulationEngine[] { new EmployeeAdjustmentEngine() }.ToList()));
            simulationSystem.AddSimulationThread(new SimulationThread(new SimulationEngine[] { new PickingEngine() }.ToList()));

            var warehouse = new Warehouse();
            var employee1 = new Employee
            {
                Speed = 5F, // change this?
                CurrentLocation = new Vector3(0, 0, 0),
                Status = Employee.EmployeeStatus.Traveling,
            };

            var employee2 = new Employee
            {
                Speed = 5F, // change this?
                CurrentLocation = new Vector3(0, 0, 0),
                Status = Employee.EmployeeStatus.Traveling,
            };

            warehouse.Objects.Add(employee1);
            warehouse.Objects.Add(employee2);

            // Generate warehouseCompartments
            for (var x = 0; x < 17; x++)
            {
                for (var y = 0; y < 24; y+=2)
                {
                    if (x != 8)
                    {
                        for (var z = 0; z < 5; z++)
                        {
                            var warehouseCompartment = new WarehouseCompartment
                            {
                                Location = new Vector3(x + 2, y + 2, z),
                            };
                            var ips = new ItemProductStatic
                            {
                                Name = $"MuchAwesomeIps{x}-{y}-{z}",
                                WarehouseCompartment = warehouseCompartment,
                            };
                            warehouseCompartment.Objects.Add(ips);
                            warehouse.Objects.Add(warehouseCompartment);
                        }
                    }
                }
            }

            // Generate some trolleys
            for (var i = 0; i < 100; i++)
            {
                warehouse.Objects.Add(new Trolley());
            }

            simulationSystem.World.Objects.Add(warehouse);

            return simulationSystem;
        }
    }
}