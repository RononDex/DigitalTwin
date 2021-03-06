
using System.Collections.Generic;
using Simulation;

namespace DigitalTwin.Prototype.Objects
{
    public class PickingTour : SimulationObject
    {
        public IList<Pick> Picks
        {
            get => (IList<Pick>)this[nameof(Picks)];
            set => this[nameof(Picks)] = value;
        }

        public Pick CurrentPick
        {
            get => (Pick)this[nameof(CurrentPick)];
            set => this[nameof(CurrentPick)] = value;
        }

        public PickingTourState State
        {
            get => (PickingTourState)this[nameof(State)];
            set => this[nameof(State)] = value;
        }

        public enum PickingTourState
        {
            New,
            InProgress,
            Finished,
        }
    }
}
