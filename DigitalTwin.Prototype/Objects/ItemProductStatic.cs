using System;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class ItemProductStatic : SimulationObject
    {
        public string Name
        {
            get => Convert.ToString(this[nameof(Name)]);
            set => this[nameof(Name)] = value;
        }

        public WarehouseCompartment? WarehouseCompartment
        {
            get => (WarehouseCompartment?)this[nameof(WarehouseCompartment)];
            set => this[nameof(WarehouseCompartment)] = value;
        }
    }
}
