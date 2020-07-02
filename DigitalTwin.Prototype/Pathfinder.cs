using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DigitalTwin.Prototype.Objects;
using Simulation;

namespace DigitalTwin.Prototype{
    public class Pathfinder{
        public static Vector3 GetNextLocation(Vector3 currentLocation, Vector3 targetLocation, SimulationContext context){
            Location current = null;
            var start = new Location { XYZ = new Vector3((float)Math.Floor(currentLocation.X), (float)Math.Floor(currentLocation.Y), (float)Math.Floor(currentLocation.Z))};
            var target = new Location { XYZ = targetLocation };
            var openList = new List<Location>();
            var closedList = new List<Location>();
            int g = 0;
 
            // start by adding the original position to the open list
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list
                closedList.Add(current);
                
                // if we added the destination to the closed list, we've found a path
                if (current.XYZ.X == target.XYZ.X && current.XYZ.Y == target.XYZ.Y-1)
                    break;
                
                // remove it from the open list
                openList.Remove(current);

                var adjacentSquares = GetWalkableAdjacentSquares(current.XYZ, context);
                g++;

                foreach(var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.XYZ.X == adjacentSquare.XYZ.X
                            && l.XYZ.Y == adjacentSquare.XYZ.Y) != null)
                        continue;
                
                    // if it's not in the open list...
                    if (openList.FirstOrDefault(l => l.XYZ.X == adjacentSquare.XYZ.X
                            && l.XYZ.Y == adjacentSquare.XYZ.Y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.XYZ, target.XYZ);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                
                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            var nextStep = closedList.FirstOrDefault(l => l.XYZ.X == target.XYZ.X && l.XYZ.Y == target.XYZ.Y-1);
            while(nextStep.Parent.XYZ != start.XYZ){
                nextStep = nextStep.Parent;
            }

            return nextStep.XYZ;
        }

        private static int ComputeHScore(Vector3 current, Vector3 target)
        {
            return (int)Math.Floor(Math.Abs(target.X - current.X) + Math.Abs(target.Y - current.Y));
        }

        private static List<Location> GetWalkableAdjacentSquares(Vector3 current, SimulationContext context)
        {
            var warehouse = context.World.Objects.First() as Warehouse;
            
            var proposedLocations = new List<Location>()
            {
                new Location { XYZ = new Vector3(current.X+1, current.Y+1, current.Z) },
                new Location { XYZ = new Vector3(current.X+1, current.Y, current.Z) },
                new Location { XYZ = new Vector3(current.X+1, current.Y-1, current.Z) },
                new Location { XYZ = new Vector3(current.X, current.Y+1, current.Z) },
                new Location { XYZ = new Vector3(current.X, current.Y, current.Z) },
                new Location { XYZ = new Vector3(current.X, current.Y-1, current.Z) },
                new Location { XYZ = new Vector3(current.X-1, current.Y+1, current.Z) },
                new Location { XYZ = new Vector3(current.X-1, current.Y, current.Z) },
                new Location { XYZ = new Vector3(current.X-1, current.Y-1, current.Z) },
            };

            return proposedLocations
                .Where(l => !warehouse.WarehouseCompartments.Any(wc => wc.Location == l.XYZ))
                .Where(l => !warehouse.Employees.Any(e => e.CurrentLocation == l.XYZ))
                .Where(l => l.XYZ.X >= 0 && l.XYZ.Y >= 0 && l.XYZ.X < warehouse.WarehouseCompartments.Max(wc => wc.Location.X) + 7 && l.XYZ.Z < warehouse.WarehouseCompartments.Max(wc => wc.Location.Y) + 7)
                .ToList();
        }

        private class Location{
            public Vector3 XYZ;
            public int F;
            public int G;
            public int H;
            public Location Parent;
        }
    }
}