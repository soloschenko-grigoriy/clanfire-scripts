using System.Collections.Generic;
using System.Linq;

namespace GS.Units.Helpers
{
    public static class StoppingDistance
    {
        public static readonly SortedDictionary<int, int> StoppingDistanceMap = new SortedDictionary<int, int>() {
            { 1, 2 },
            { 4, 2 },
            { 12, 2 },
            { 20, 2 },
            { 28, 3 },
            { 48, 3 }
        };

        private static readonly int[] STOPPING_DISTANCE_MAP_KEYS = StoppingDistanceMap.Keys.ToArray();
        
        public static int FindByKey(int key)
        {
            // too small
            if (key < STOPPING_DISTANCE_MAP_KEYS[0])
            {
                return StoppingDistanceMap[STOPPING_DISTANCE_MAP_KEYS[0]];
            }
            
            // about right
            for (int i = 0; i < STOPPING_DISTANCE_MAP_KEYS.Length - 1; i++)
            {
                var item = STOPPING_DISTANCE_MAP_KEYS[i];
                var next = STOPPING_DISTANCE_MAP_KEYS[i + 1];
                if (key >= item && key < next)
                {
                    return StoppingDistanceMap[item];
                }
            }

            // too large
            return StoppingDistanceMap[STOPPING_DISTANCE_MAP_KEYS[STOPPING_DISTANCE_MAP_KEYS.Length - 1]];
        }
    }
}
