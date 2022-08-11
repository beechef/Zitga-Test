using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private MapsData mapsData;
        [SerializeField] private int width = 10;
        [SerializeField] private int height = 13;

        private void Awake()
        {
            Random.InitState(mapsData.maps[Mathf.Clamp(mapsData.currentMap - 1, 0, mapsData.maps.Count)].level);
            mapsData.maze = CreateMaze();
            Generate(mapsData.maze);

            Random.InitState(DateTime.Now.Millisecond);
            mapsData.targetPos = new Vector2Int(Random.Range(1, mapsData.maze.GetLength(0)),
                Random.Range(1, mapsData.maze.GetLength(1)));
        }


        private Node[,] CreateMaze()
        {
            var maze = new Node[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = new Node {linkNodes = {[0] = null, [1] = null, [2] = null, [3] = null}};
                }
            }

            return maze;
        }


        private void Generate(Node[,] maze, int x = 0, int y = 0)
        {
            if (!IsValidateIndex(x, y) || maze[x, y].isVisited) return;

            Node node = maze[x, y];
            node.x = x;
            node.y = y;
            node.isVisited = true;


            List<int> randomNumbers = new List<int>();

            while (randomNumbers.Count != 4)
            {
                var randomNumber = Random.Range(0, 4);
                if (randomNumbers.Contains(randomNumber)) continue;
                randomNumbers.Add(randomNumber);

                int x1 = 0;
                int y1 = 0;
                switch (randomNumber)
                {
                    case Node.Left:
                    {
                        x1 = x + Node.LeftDir.x;
                        y1 = y + Node.LeftDir.y;

                        if (!IsValidateIndex(x1, y1) || maze[x1, y1].isVisited) continue;

                        var nextNode = maze[x1, y1];

                        node.linkNodes[Node.Left] = nextNode;
                        nextNode.linkNodes[Node.Right] = node;

                        Generate(maze, x1, y1);

                        break;
                    }

                    case Node.Right:
                    {
                        x1 = x + Node.RightDir.x;
                        y1 = y + Node.RightDir.y;

                        if (!IsValidateIndex(x1, y1) || maze[x1, y1].isVisited) continue;

                        var nextNode = maze[x1, y1];

                        node.linkNodes[Node.Right] = nextNode;
                        nextNode.linkNodes[Node.Left] = node;

                        Generate(maze, x1, y1);

                        break;
                    }

                    case Node.Top:
                    {
                        x1 = x + Node.TopDir.x;
                        y1 = y + Node.TopDir.y;

                        if (!IsValidateIndex(x1, y1) || maze[x1, y1].isVisited) continue;

                        var nextNode = maze[x1, y1];

                        node.linkNodes[Node.Top] = nextNode;
                        nextNode.linkNodes[Node.Bottom] = node;

                        Generate(maze, x1, y1);

                        break;
                    }

                    case Node.Bottom:
                    {
                        x1 = x + Node.BottomDir.x;
                        y1 = y + Node.BottomDir.y;

                        if (!IsValidateIndex(x1, y1) || maze[x1, y1].isVisited) continue;

                        var nextNode = maze[x1, y1];

                        node.linkNodes[Node.Bottom] = nextNode;
                        nextNode.linkNodes[Node.Top] = node;

                        Generate(maze, x1, y1);

                        break;
                    }
                }
            }
        }

        private bool IsValidateIndex(int x, int y)
        {
            return !(x < 0 || x >= width || y < 0 || y >= height);
        }
    }
}