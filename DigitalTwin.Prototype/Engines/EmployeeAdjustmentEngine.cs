using System;
using System.Linq;
using System.Numerics;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype.Engines
{
    public class EmployeeAdjustmentEngine : SimulationEngine
    {
        public override void UpdateWorld(SimulationContext context, TimeSpan step)
        {
            var random = new Random((int)step.Ticks);
            var warehouse = context.World.Objects.First() as Warehouse;
            if (warehouse.PickingTours.Count(p => p.State == PickingTour.PickingTourState.New) > 2)
            {
                warehouse.Objects.Add(
                    new Employee
                    {
                        Speed = random.Next(1, 3),
                        CurrentLocation = new Vector3(0, 0, 0),
                        Status = Employee.EmployeeStatus.Traveling,
                    }
                );
            }

            //while (warehouse.PickingTours.Count < warehouse.Employees.Count(e => e.PickingTour == null) && warehouse.Employees.Count < 2)
            //{
            //    var employeeToRemove = warehouse.Employees.FirstOrDefault(e => e.PickingTour == null);
            //    if (employeeToRemove != null)
            //    {
            //        warehouse.Employees.Remove(employeeToRemove);
            //    }
            //}
        }
    }
}