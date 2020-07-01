
using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class Employee : SimulationObject
    {
        public float Speed
        {
            get => Convert.ToSingle(this[nameof(Speed)]);
            set => this[nameof(Speed)] = value;
        }

        public Vector3 CurrentLocation
        {
            get => (Vector3)this[nameof(CurrentLocation)];
            set => this[nameof(CurrentLocation)] = value;
        }

        public PickingTour? PickingTour
        {
            get => (PickingTour?)this[nameof(PickingTour)];
            set => this[nameof(PickingTour)] = value;
        }

        public EmployeeStatus Status
        {
            get => (EmployeeStatus)this[nameof(Status)];
            set => this[nameof(Status)] = value;
        }

        public enum EmployeeStatus
        {
            Traveling,
            Picking,
            ReturningToHome,
        }
    }
}
