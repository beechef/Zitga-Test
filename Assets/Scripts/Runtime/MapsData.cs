using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu]
    public class MapsData : ScriptableObject
    {
        public List<Map> maps;

        public int currentMap;

        public Node[,] maze;

        public Vector2Int targetPos;
    }
}