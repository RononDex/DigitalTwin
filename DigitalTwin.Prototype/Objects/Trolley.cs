using System.Collections.Generic;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class Trolley : SimulationObject
    {
        public IList<ItemProductStatic>? ItemProductStatics
        {
            get => (List<ItemProductStatic>?)this[nameof(ItemProductStatics)];
            set => this[nameof(ItemProductStatics)] = value;
        }

        public Employee Employee
        {
            get => (Employee) this[nameof(Employee)];
            set => this[nameof(Employee)] = value;
        }
    }
}
