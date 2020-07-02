using System;
using System.Collections.Generic;
using System.Linq;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype.Engines
{
    public class RandomPickingTourGenerator : SimulationEngine
    {
        private int secondsOnAverageBetweenNewPickingTours = 10;

        private DateTime nextPickingTourGenerationTime = DateTime.Now.AddHours(-1);

        private Random randomGenerator = new Random();

        public override void UpdateWorld(SimulationContext context, TimeSpan step)
        {
            var warehouse = context.World.Objects.First() as Warehouse;
            if (nextPickingTourGenerationTime < DateTime.Now && randomGenerator.Next(0,3) == 1)
            {
                var amountOfPicks = randomGenerator.Next(minValue: 1, maxValue: 5);
                var ipsAlreadyInPickingTour = warehouse
                    .PickingTours
                    .SelectMany(pt => pt.Picks)
                    .SelectMany(p => p.ItemProductStatics)
                    .Select(ips => ips.Name);
                var picks = new List<Pick>();

                for (var i = 0; i < amountOfPicks; i++)
                {
                    var ipsNotYetInPickingTour = warehouse.WarehouseCompartments
                                    .Where(
                                        wc => wc.ItemProductStatics.Any())
                                    .SelectMany(wc => wc.ItemProductStatics)
                                    .Where(ips => !ipsAlreadyInPickingTour.Contains(ips.Name))
                                    .ToList();
                    var ips = ipsNotYetInPickingTour[randomGenerator.Next(0, ipsNotYetInPickingTour.Count - 1)];
                    picks.Add(new Pick
                    {
                        ItemProductStatics = new List<ItemProductStatic>
                        {
                            ips,
                        },
                        WarehouseCompartment = ips.WarehouseCompartment,
                    });
                }

                warehouse.Objects.Add(new PickingTour
                {
                    Picks = picks,
                    State = PickingTour.PickingTourState.New
                });

                nextPickingTourGenerationTime = DateTime.Now + new TimeSpan(
                    0,
                    0,
                    Convert.ToInt32(randomGenerator.NextDouble() * secondsOnAverageBetweenNewPickingTours * 2));
            }
        }
    }
}