using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    namespace Algorithm
    {
        public static class AStar
        {
            public static List<Vector2Int> Run(Node[,] nodes, Vector2Int startPos, Vector2Int targetPos)
            {
                List<State> opens = new List<State>()
                {
                    new State()
                    {
                        node = nodes[startPos.x, startPos.y],
                        g = 0,
                        h = 0,
                        parent = null,
                    }
                };

                List<State> closes = new List<State>();

                State targetState = null;

                while (opens.Count != 0)
                {
                    int minIndex = GetMinStateIndex(opens);
                    var minState = opens[minIndex];
                    opens.RemoveAt(minIndex);

                    closes.Add(minState);

                    var availableStates = GetAllAvailableStates(minState, targetPos);

                    foreach (var availableState in availableStates)
                    {
                        var isInOpen = false;
                        var isInClose = false;

                        if (availableState.node.x == targetPos.x && availableState.node.y == targetPos.y)
                        {
                            targetState = availableState;
                            break;
                        }

                        foreach (var open in opens)
                        {
                            if (open.node == availableState.node && open.g > availableState.g)
                            {
                                open.g = availableState.g;
                                open.h = availableState.h;
                                open.parent = availableState.parent;
                                isInOpen = true;
                                break;
                            }
                        }

                        foreach (var close in closes)
                        {
                            if (close.node == availableState.node) isInClose = true;
                            if (close.node == availableState.node && close.g > availableState.g)
                            {
                                closes.Remove(close);
                                opens.Add(availableState);
                                break;
                            }
                        }

                        if (!isInOpen && !isInClose) opens.Add(availableState);
                    }

                    if (targetState != null) break;
                }

                return GetPath(targetState);
            }

            private static int GetMinStateIndex(List<State> states)
            {
                float minF = float.MaxValue;
                int minIndex = 0;

                for (int i = 0; i < states.Count; i++)
                {
                    if (minF <= states[i].F) continue;
                    minF = states[i].F;
                    minIndex = i;
                }

                return minIndex;
            }

            private static List<State> GetAllAvailableStates(State state, Vector2Int targetPos)
            {
                List<State> availableStates = new List<State>();

                foreach (var linkNode in state.node.linkNodes)
                {
                    if (linkNode == null) continue;

                    availableStates.Add(new State()
                    {
                        node = linkNode,
                        h = Vector2Int.Distance(new Vector2Int(linkNode.x, linkNode.y), targetPos),
                        g = state.g + Vector2Int.Distance(new Vector2Int(linkNode.x, linkNode.y),
                            new Vector2Int(state.node.x, state.node.y)),
                        parent = state
                    });
                }

                return availableStates;
            }

            private static List<Vector2Int> GetPath(State state)
            {
                List<Vector2Int> path = new List<Vector2Int>();

                while (state != null)
                {
                    path.Add(new Vector2Int(state.node.x, state.node.y));
                    state = state.parent;
                }

                return path;
            }
        }

        public class State
        {
            public Node node;

            public float g;
            public float h;
            public float F => g + h;

            public State parent;
        }
    }
}