using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class WarehouseCompartment : SimulationGroup
    {
        public IList<ItemProductStatic> ItemProductStatics
        {
            get => Objects
                .Where(o => o is ItemProductStatic)
                .Select(o => o as ItemProductStatic)
                .ToList();
        }

        public Vector3 Location
        {
            get => (Vector3)this[nameof(Location)];
            set => this[nameof(Location)] = value;
        }
    }
}
