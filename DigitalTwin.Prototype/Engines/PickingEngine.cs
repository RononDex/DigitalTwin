using System;
using System.Linq;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype.Engines
{
    public class PickingEngine : SimulationEngine
    {
        private static TimeSpan timeRequiredPerPick = new TimeSpan(hours: 0, minutes: 0, seconds: 10);

        public override void UpdateWorld(SimulationContext context, TimeSpan step)
        {
            var warehouse = context.World.Objects.First() as Warehouse;
            foreach (var employee in warehouse.Employees)
            {
                // Start picking
                if (employee.PickingTour != null
                    && Convert.ToInt32(employee.CurrentLocation.X) == Convert.ToInt32(employee.PickingTour.CurrentPick.WarehouseCompartment.Location.X)
                    && Convert.ToInt32(employee.CurrentLocation.Y) == Convert.ToInt32(employee.PickingTour.CurrentPick.WarehouseCompartment.Location.Y)
                    && employee.Status == Employee.EmployeeStatus.Traveling)
                {
                    employee.Status = Employee.EmployeeStatus.Picking;
                    employee.PickingEndTime = DateTime.Now + timeRequiredPerPick;
                }

                if (employee.Status == Employee.EmployeeStatus.Picking
                    && employee.PickingEndTime < DateTime.Now)
                {
                    var trolley = warehouse.Trolleys.First(t => t.Employee == employee);
                    var ips = employee.PickingTour!.CurrentPick.ItemProductStatics.First();
                    employee.PickingTour.CurrentPick.ItemProductStatics.Clear();
                    employee.PickingTour.CurrentPick.WarehouseCompartment.Objects.Remove(ips);
                    trolley.Objects.Add(ips);
                    employee.PickingTour.Picks.Remove(employee.PickingTour.CurrentPick);
                    if (employee.PickingTour.Picks.Count > 0)
                    {
                        employee.PickingTour.CurrentPick = employee.PickingTour.Picks.First();
                        employee.Status = Employee.EmployeeStatus.Traveling;
                    }
                    else
                    {
                        employee.Status = Employee.EmployeeStatus.ReturningToHome;
                        employee.PickingTour.State = PickingTour.PickingTourState.Finished;
                    }
                }
            }
        }
    }
}