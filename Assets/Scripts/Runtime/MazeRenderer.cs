using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Runtime
{
    public class MazeRenderer : MonoBehaviour
    {
        [SerializeField] private MapsData mapsData;
        [SerializeField] private List<Tilemap> tilemaps;
        [SerializeField] private Sprite leftEdgeSprite;
        [SerializeField] private Sprite rightEdgeSprite;
        [SerializeField] private Sprite topEdgeSprite;
        [SerializeField] private Sprite bottomEdgeSprite;

        [SerializeField] private GameObject target;

        private Tile _leftEdgeTile;
        private Tile _rightEdgeTile;
        private Tile _topEdgeTile;
        private Tile _bottomEdgeTile;

        private void Awake()
        {
            _leftEdgeTile = ScriptableObject.CreateInstance<Tile>();
            _leftEdgeTile.sprite = leftEdgeSprite;

            _rightEdgeTile = ScriptableObject.CreateInstance<Tile>();
            _rightEdgeTile.sprite = rightEdgeSprite;


            _topEdgeTile = ScriptableObject.CreateInstance<Tile>();
            _topEdgeTile.sprite = topEdgeSprite;

            _bottomEdgeTile = ScriptableObject.CreateInstance<Tile>();
            _bottomEdgeTile.sprite = bottomEdgeSprite;
        }

        private void Start()
        {
            Render(mapsData.maze);
            target.transform.localPosition = new Vector3(mapsData.targetPos.x, mapsData.targetPos.y);
        }


        private void Clear()
        {
            foreach (var tilemap in tilemaps)
            {
                tilemap.ClearAllTiles();
            }
        }

        private void Render(Node[,] maze)
        {
            Clear();
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    var node = maze[i, j];
                    int x = i;
                    int y = j;

                    if (node.linkNodes[Node.Left] == null)
                        tilemaps[0].SetTile(new Vector3Int(x, y), _leftEdgeTile);
                    if (node.linkNodes[Node.Right] == null)
                        tilemaps[1].SetTile(new Vector3Int(x, y), _rightEdgeTile);
                    if (node.linkNodes[Node.Top] == null)
                        tilemaps[2].SetTile(new Vector3Int(x, y), _topEdgeTile);
                    if (node.linkNodes[Node.Bottom] == null)
                        tilemaps[3].SetTile(new Vector3Int(x, y), _bottomEdgeTile);
                }
            }
        }
    }
}