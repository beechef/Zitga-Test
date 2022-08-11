using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    public static class MapUtilities
    {
        private const byte MaxStars = 3;

        public static List<Map> CreateMaps(int mapCapacity)
        {
            List<Map> maps = new List<Map>(mapCapacity);

            for (int i = 0; i < mapCapacity; i++)
            {
                Map map = new Map()
                {
                    isLock = i != 0,
                    level = i + 1,
                    stars = 0
                };

                maps.Add(map);
            }

            return maps;
        }

        public static void ResetMaps(this List<Map> maps)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                maps[i].stars = 0;
                maps[i].isLock = i != 0;
            }
        }

        public static void RandomStars(this List<Map> maps, int endMap)
        {
            int endIndex = maps.Count < endMap ? maps.Count : endMap;

            for (int i = 0; i < maps.Count; i++)
            {
                maps[i].stars = (byte) (i > endIndex ? 0 : Random.Range(0, MaxStars + 1));
                maps[i].isLock = i > endIndex;
            }
        }

        public static int CountStars(this List<Map> maps)
        {
            int count = 0;
            foreach (var map in maps)
            {
                count += map.stars;
            }

            return count;
        }
    }
}