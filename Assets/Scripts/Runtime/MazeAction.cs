using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Runtime.Algorithm;
using UnityEngine;

namespace Runtime
{
    public class MazeAction : MonoBehaviour
    {
        [SerializeField] private MapsData mapsData;
        [SerializeField] private LineRenderer lineRenderer;

        public void Hint()
        {
            var path = GetPath();

            RenderHint(path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<Vector2Int> GetPath() => AStar.Run(mapsData.maze, Vector2Int.zero, mapsData.targetPos);

        private void RenderHint(List<Vector2Int> path)
        {
            var index = 0;
            lineRenderer.positionCount = path.Count;
            foreach (var node in path)
            {
                lineRenderer.SetPosition(index++, new Vector3(node.x, node.y));
            }
        }
    }
}