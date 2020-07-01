using System.Collections.Generic;
using System.Numerics;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class WarehouseCompartment : SimulationObject
    {
        public IList<ItemProductStatic> ItemProductStatics
        {
            get => (List<ItemProductStatic>)this[nameof(ItemProductStatics)];
            set => this[nameof(ItemProductStatics)] = value;
        }

        public Vector3 Location
        {
            get => (Vector3)this[nameof(Location)];
            set => this[nameof(Location)] = value;
        }
    }
}
