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
                    && employee.Status == Employee.EmployeeStatus.Traveling
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
                if (employee.PickingTour != null
                    && employee.Status == Employee.EmployeeStatus.Traveling)
                {
                    // Take a trolley
                    if (!warehouse.Trolleys.Any(t => t.Employee == employee))
                    {
                        warehouse.Trolleys.First(t => t.Employee == null).Employee = employee;
                    }

                    MoveEmployeeTowards(
                        employee,
                        employee.PickingTour.CurrentPick.WarehouseCompartment.Location,
                        Convert.ToSingle(employee.Speed * step.TotalSeconds), context);
                }

                if (employee.Status == Employee.EmployeeStatus.ReturningToHome)
                {
                    MoveEmployeeTowards(employee, new Vector3(0, 0, 0), Convert.ToSingle(employee.Speed * step.TotalSeconds), context);

                    if (Convert.ToInt32(employee.CurrentLocation.X) == 0
                        && Convert.ToInt32(employee.CurrentLocation.Y) == 0)
                    {
                        employee.PickingTour = null;
                        employee.Status = Employee.EmployeeStatus.Traveling;
                    }
                }
            }
        }

        private void MoveEmployeeTowards(Employee employee, Vector3 target, float distance, SimulationContext context)
        {
            var travelPath = employee.CurrentLocation.X == target.X && employee.CurrentLocation.Y == target.Y
                ? target - employee.CurrentLocation
                : Pathfinder.GetNextLocation(employee.CurrentLocation, target, context) - employee.CurrentLocation;
            var normalizedVector = travelPath.Normalize();
            var pathTravelledInTimeDelta = new Vector3(
                normalizedVector.X * distance,
                normalizedVector.Y * distance,
                normalizedVector.Z * distance);
            employee.CurrentLocation += pathTravelledInTimeDelta;
        }
    }
}