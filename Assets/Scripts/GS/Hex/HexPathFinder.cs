using System.Collections.Generic;
using GS.Helpers;

namespace GS.Hex
{
    public static class HexPathFinder
    {
        private static int Heuristic(HexCoordinates a, HexCoordinates b) => HexCoordinates.DistanceBetween(a, b);

        public static List<IHexCell> FindPath(IHexCell from, IHexCell to, bool includeStart = false, bool reverse = false)
        {
            var cameFrom = Trace(from, to);
            var current = to;
            var path = new List<IHexCell>();
            
            // destination cannot be reached
            if (!cameFrom.ContainsKey(current))
            {
                return path;
            }

            while (current != from) {
                path.Add(current);
                current = cameFrom[current];
            }

            if (includeStart) {
                path.Add(from);
            }

            if (reverse) {
                path.Reverse();
            }
            
            return path;
        }

        private static Dictionary<IHexCell, IHexCell> Trace(IHexCell start, IHexCell end) {
            var cameFrom = new Dictionary<IHexCell, IHexCell>();
            var cost = new Dictionary<IHexCell, int>();

            var frontier = new PriorityQueue<IHexCell>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            cost[start] = 0;

            while (frontier.Count > 0) {
                var current = frontier.Dequeue();
                if (current.Coordinates.Equals(end.Coordinates)) {
                    break;
                }

                foreach (var next in current.Neighbors) {
                    if (next.HasObstacle) {
                        continue;
                    }

                    if (!next.CanBeAccessed())
                    {
                        continue;
                    }
                    
                    int newCost = cost[current] + current.GetCost(next);
                    if (cost.ContainsKey(next) && newCost >= cost[next])
                    {
                        continue;
                    }

                    cost[next] = newCost;
                    int priority = newCost + Heuristic(next.Coordinates, end.Coordinates);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }

            return cameFrom;
        }
    }
}
