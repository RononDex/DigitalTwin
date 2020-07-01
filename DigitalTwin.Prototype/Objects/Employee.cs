
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

        public void Pick()
        {
            if (PickingTour != null && PickingTour.Picks.Any())
            {
                var nextPick = PickingTour.Picks.First();
                var timeToWalkToNextCompartment = new TimeSpan(0, 0, (int)DistanceBetweenCompartments(CurrentLocation, nextPick.WarehouseCompartment.Location));
            }
        }

        private float DistanceBetweenCompartments(Vector3 beginning, Vector3 target)
        {
            return Math.Abs(beginning.X - target.X) * 3 + Math.Abs(beginning.Y - target.Y) * 2 +
                   Math.Abs(beginning.Z - target.Z) * 1;
        }

        public enum EmployeeStatus
        {
            Traveling,
            Picking,
            ReturningToHome,
        }
    }
}
