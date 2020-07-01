using System.Collections.Generic;
using System.Linq;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class Warehouse : SimulationGroup
    {
        public IList<Employee> Employees
        {
            get => Objects
                .Where(o => o is Employee)
                .Select(o => o as Employee)
                .ToList();
        }

        public IList<WarehouseCompartment> WarehouseCompartments
        {
            get => Objects
                .Where(o => o is WarehouseCompartment)
                .Select(o => o as WarehouseCompartment)
                .ToList();
        }

        public IList<Trolley> Trolleys
        {
            get => Objects
                .Where(o => o is Trolley)
                .Select(o => o as Trolley)
                .ToList();
        }
    }
}
