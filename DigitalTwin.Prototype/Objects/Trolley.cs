using System.Collections.Generic;
using System.Linq;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class Trolley : SimulationGroup
    {
        public List<ItemProductStatic> ItemProductStatics
        {
            get => Objects
                .Where(o => o is ItemProductStatic)
                .Select(o => o as ItemProductStatic)
                .ToList();
        }

        public Employee Employee
        {
            get => (Employee) this[nameof(Employee)];
            set => this[nameof(Employee)] = value;
        }
    }
}
