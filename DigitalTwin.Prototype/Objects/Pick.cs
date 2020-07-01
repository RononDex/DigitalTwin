
using System.Collections.Generic;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class Pick : SimulationObject
    {

        public IList<ItemProductStatic> ItemProductStatics
        {
            get => (List<ItemProductStatic>)this[nameof(ItemProductStatics)];
            set => this[nameof(ItemProductStatics)] = value;
        }


        public WarehouseCompartment WarehouseCompartment
        {
            get => (WarehouseCompartment)this[nameof(WarehouseCompartment)];
            set => this[nameof(WarehouseCompartment)] = value;
        }
    }
}
