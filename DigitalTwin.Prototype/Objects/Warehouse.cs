using System.Collections.Generic;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class Warehouse : SimulationGroup
    {
        public IList<WarehouseCompartment> WarehouseCompartments
        {
            get => (List<WarehouseCompartment>)this[nameof(WarehouseCompartments)];
            set => this[nameof(WarehouseCompartments)] = value;
        }

    }
}
