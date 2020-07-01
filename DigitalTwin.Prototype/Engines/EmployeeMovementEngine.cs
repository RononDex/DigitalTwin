using System;
using System.Linq;
using System.Numerics;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype.Engines
{
    public class EmployeeMovementEngine : SimulationEngine
    {
        public override void UpdateWorld(SimulationContext context, TimeSpan step)
        {
            var warehouse = context.World.Objects.First() as Warehouse;
            foreach (var employee in warehouse.Employees)
            {
                // If employee is idle, find new picking tour to start
                if (employee.PickingTour == null
                    && employee.Status != Employee.EmployeeStatus.ReturningToHome
                    && warehouse.PickingTours.Any(pt => pt.State == PickingTour.PickingTourState.New))
                {
                    employee.PickingTour = warehouse.PickingTours.First(pt => pt.State == PickingTour.PickingTourState.New);
                    var firstPick = employee.PickingTour.Picks.First();
                    employee.PickingTour.Picks.Remove(firstPick);
                    employee.PickingTour.CurrentPick = firstPick;
                    employee.PickingTour.State = PickingTour.PickingTourState.InProgress;
                    employee.Status = Employee.EmployeeStatus.Traveling;
                }

                // If employee has a packing tour, move the employee to its next picking target
                if (employee.PickingTour != null)
                {
                    var travelPath = employee.PickingTour.CurrentPick.WarehouseCompartment.Location - employee.CurrentLocation;
                    var normalizedVector = travelPath.Normalize();
                    var factor = Convert.ToSingle(employee.Speed * step.TotalSeconds);
                    var pathTravelledInTimeDelta = new Vector3(
                        normalizedVector.X * factor,
                        normalizedVector.Y * factor,
                        normalizedVector.Z * factor);
                    employee.CurrentLocation += pathTravelledInTimeDelta;
                }
            }
        }
    }
}