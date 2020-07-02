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
            simulationSystem.AddSimulationThread(new SimulationThread(new SimulationEngine[] { new EmployeeAdjustmentEngine()  }.ToList()));

            var warehouse = new Warehouse();
            var employee1 = new Employee
            {
                Speed = 2.5F, // change this?
                CurrentLocation = new Vector3(0, 0, 0),
                Status = Employee.EmployeeStatus.Traveling,
            };

            var employee2 = new Employee
            {
                Speed = 2.1F, // change this?
                CurrentLocation = new Vector3(0, 0, 0),
                Status = Employee.EmployeeStatus.Traveling,
        };

            warehouse.Objects.Add(employee1);
            warehouse.Objects.Add(employee2);

            // Generate warehouseCompartments
            for (var i = 0; i < 17; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    if (i != 8)
                    {
                        for (var k = 0; k < 5; k++)
                        {
                            var warehouseCompartment = new WarehouseCompartment
                            {
                                Location = new Vector3(i + 2, j + 2, k),
                            };
                            var ips = new ItemProductStatic
                            {
                                Name = $"MuchAwesomeIps{i}-{j}-{k}",
                                WarehouseCompartment = warehouseCompartment,
                        };
                            warehouseCompartment.Objects.Add(ips);
                            warehouse.Objects.Add(warehouseCompartment);
                        }
                    }
                }
            }

            // Generate some trolleys
            for (var i = 0; i < 10; i++)
            {
                warehouse.Objects.Add(new Trolley());
            }

            simulationSystem.World.Objects.Add(warehouse);

            return simulationSystem;
        }
    }
}