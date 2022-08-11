using UnityEngine;

namespace Runtime
{
    [System.Serializable]
    public class Node
    {
        public const int Left = 0;
        public const int Top = 1;
        public const int Right = 2;
        public const int Bottom = 3;

        public static readonly Vector2Int LeftDir = new Vector2Int(-1, 0);
        public static readonly Vector2Int TopDir = new Vector2Int(0, 1);
        public static readonly Vector2Int RightDir = new Vector2Int(1, 0);
        public static readonly Vector2Int BottomDir = new Vector2Int(0, -1);

        public int x;
        public int y;
        public bool isVisited;

        public Node[] linkNodes = new Node[4];
        
    }
}