// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.MyPathFindingSystem`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VRage.Collections;

namespace VRage.Algorithms
{
    public class MyPathFindingSystem<V> : IEnumerable<V>, IEnumerable where V : class, IMyPathVertex<V>
    {
        private long m_timestamp;
        private Stack<V> m_dfsStack;
        private List<V> m_reachableList;
        private MyBinaryHeap<float, MyPathfindingData<V>> m_openVertices;
        private MyPathFindingSystem<V>.Enumerator m_enumerator;
        private bool m_enumerating;

        public MyPathFindingSystem(int stackInitSize = 128)
        {
            this.m_dfsStack = new Stack<V>(stackInitSize);
            this.m_reachableList = new List<V>(128);
            this.m_openVertices = new MyBinaryHeap<float, MyPathfindingData<V>>(128, (IComparer<float>) null);
            this.m_timestamp = 0L;
            this.m_enumerating = false;
            this.m_enumerator = new MyPathFindingSystem<V>.Enumerator();
        }

        public MyPath<V> FindPath(V start, V end, Predicate<V> vertexTraversable = null,
            Predicate<IMyPathEdge<V>> edgeTraversable = null)
        {
            ++this.m_timestamp;
            MyPathfindingData<V> pathfindingData1 = start.PathfindingData;
            this.Visit(pathfindingData1);
            pathfindingData1.Predecessor = (MyPathfindingData<V>) null;
            pathfindingData1.PathLength = 0.0f;
            IMyPathVertex<V> myPathVertex = (IMyPathVertex<V>) null;
            float num1 = float.PositiveInfinity;
            this.m_openVertices.Insert(start.PathfindingData, start.EstimateDistanceTo((IMyPathVertex<V>) end));
            while (this.m_openVertices.Count > 0)
            {
                MyPathfindingData<V> myPathfindingData = this.m_openVertices.RemoveMin();
                V parent = myPathfindingData.Parent;
                float num2 = myPathfindingData.PathLength;
                if (myPathVertex == null || (double) num2 < (double) num1)
                {
                    for (int index = 0; index < parent.GetNeighborCount(); ++index)
                    {
                        IMyPathEdge<V> edge = parent.GetEdge(index);
                        if (edge != null && (edgeTraversable == null || edgeTraversable(edge)))
                        {
                            V otherVertex = edge.GetOtherVertex(parent);
                            if ((object) otherVertex != null &&
                                (vertexTraversable == null || vertexTraversable(otherVertex)))
                            {
                                float num3 = myPathfindingData.PathLength + edge.GetWeight();
                                MyPathfindingData<V> pathfindingData2 = otherVertex.PathfindingData;
                                if ((object) otherVertex == (object) end && (double) num3 < (double) num1)
                                {
                                    myPathVertex = (IMyPathVertex<V>) otherVertex;
                                    num1 = num3;
                                }
                                if (this.Visited(pathfindingData2))
                                {
                                    if ((double) num3 < (double) pathfindingData2.PathLength)
                                    {
                                        pathfindingData2.PathLength = num3;
                                        pathfindingData2.Predecessor = myPathfindingData;
                                        this.m_openVertices.ModifyUp(pathfindingData2,
                                            num3 + otherVertex.EstimateDistanceTo((IMyPathVertex<V>) end));
                                    }
                                }
                                else
                                {
                                    this.Visit(pathfindingData2);
                                    pathfindingData2.PathLength = num3;
                                    pathfindingData2.Predecessor = myPathfindingData;
                                    this.m_openVertices.Insert(pathfindingData2,
                                        num3 + otherVertex.EstimateDistanceTo((IMyPathVertex<V>) end));
                                }
                            }
                        }
                    }
                }
                else
                    break;
            }
            this.m_openVertices.Clear();
            if (myPathVertex == null)
                return (MyPath<V>) null;
            else
                return this.ReturnPath(myPathVertex.PathfindingData, (MyPathfindingData<V>) null, 0);
        }

        private MyPath<V> ReturnPath(MyPathfindingData<V> vertexData, MyPathfindingData<V> successor,
            int remainingVertices)
        {
            if (vertexData.Predecessor == null)
            {
                return new MyPath<V>(remainingVertices + 1)
                {
                    {
                        (IMyPathVertex<V>) vertexData.Parent,
                        (IMyPathVertex<V>) (successor != null ? successor.Parent : default (V))
                    }
                };
            }
            else
            {
                MyPath<V> myPath = this.ReturnPath(vertexData.Predecessor, vertexData, remainingVertices + 1);
                myPath.Add((IMyPathVertex<V>) vertexData.Parent,
                    (IMyPathVertex<V>) (successor != null ? successor.Parent : default (V)));
                return myPath;
            }
        }

        public bool Reachable(V from, V to)
        {
            this.PrepareTraversal(from, (Predicate<V>) null, (Predicate<V>) null, (Predicate<IMyPathEdge<V>>) null);
            foreach (V v in this)
            {
                if (v.Equals((object) to))
                    return true;
            }
            return false;
        }

        public void FindReachable(IEnumerable<V> fromSet, List<V> reachableVertices, Predicate<V> vertexFilter = null,
            Predicate<V> vertexTraversable = null, Predicate<IMyPathEdge<V>> edgeTraversable = null)
        {
            ++this.m_timestamp;
            foreach (V v in fromSet)
            {
                if (!this.Visited(v))
                    this.FindReachableInternal(v, reachableVertices, vertexFilter, vertexTraversable, edgeTraversable);
            }
        }

        public void FindReachable(V from, List<V> reachableVertices, Predicate<V> vertexFilter = null,
            Predicate<V> vertexTraversable = null, Predicate<IMyPathEdge<V>> edgeTraversable = null)
        {
            this.FindReachableInternal(from, reachableVertices, vertexFilter, vertexTraversable, edgeTraversable);
        }

        private void FindReachableInternal(V from, List<V> reachableVertices, Predicate<V> vertexFilter = null,
            Predicate<V> vertexTraversable = null, Predicate<IMyPathEdge<V>> edgeTraversable = null)
        {
            this.PrepareTraversal(from, vertexFilter, vertexTraversable, edgeTraversable);
            foreach (V v in this)
                reachableVertices.Add(v);
        }

        private void Visit(V vertex)
        {
            vertex.PathfindingData.Timestamp = this.m_timestamp;
        }

        private void Visit(MyPathfindingData<V> vertexData)
        {
            vertexData.Timestamp = this.m_timestamp;
        }

        private bool Visited(V vertex)
        {
            return vertex.PathfindingData.Timestamp == this.m_timestamp;
        }

        private bool Visited(MyPathfindingData<V> vertexData)
        {
            return vertexData.Timestamp == this.m_timestamp;
        }

        public void PrepareTraversal(V startingVertex, Predicate<V> vertexFilter = null,
            Predicate<V> vertexTraversable = null, Predicate<IMyPathEdge<V>> edgeTraversable = null)
        {
            this.m_enumerator.Init(this, startingVertex, vertexFilter, vertexTraversable, edgeTraversable);
        }

        private MyPathFindingSystem<V>.Enumerator GetEnumeratorInternal()
        {
            return this.m_enumerator;
        }

        public IEnumerator<V> GetEnumerator()
        {
            return (IEnumerator<V>) this.GetEnumeratorInternal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumeratorInternal();
        }

        public class Enumerator : IEnumerator<V>, IDisposable, IEnumerator
        {
            private V m_currentVertex;
            private MyPathFindingSystem<V> m_parent;
            private Predicate<V> m_vertexFilter;
            private Predicate<V> m_vertexTraversable;
            private Predicate<IMyPathEdge<V>> m_edgeTraversable;

            public V Current
            {
                get { return this.m_currentVertex; }
            }

            object IEnumerator.Current
            {
                get { return (object) this.m_currentVertex; }
            }

            public void Init(MyPathFindingSystem<V> parent, V startingVertex, Predicate<V> vertexFilter = null,
                Predicate<V> vertexTraversable = null, Predicate<IMyPathEdge<V>> edgeTraversable = null)
            {
                this.m_parent = parent;
                this.m_vertexFilter = vertexFilter;
                this.m_vertexTraversable = vertexTraversable;
                this.m_edgeTraversable = edgeTraversable;
                ++this.m_parent.m_timestamp;
                this.m_parent.m_enumerating = true;
                this.m_parent.m_dfsStack.Push(startingVertex);
            }

            public void Dispose()
            {
                this.m_parent.m_enumerating = false;
                this.m_parent.m_dfsStack.Clear();
            }

            public bool MoveNext()
            {
                while (Enumerable.Count<V>((IEnumerable<V>) this.m_parent.m_dfsStack) != 0)
                {
                    this.m_currentVertex = this.m_parent.m_dfsStack.Pop();
                    this.m_currentVertex.PathfindingData.Timestamp = this.m_parent.m_timestamp;
                    V v = default (V);
                    for (int index = 0; index < this.m_currentVertex.GetNeighborCount(); ++index)
                    {
                        if (this.m_edgeTraversable == null)
                        {
                            v = (V) this.m_currentVertex.GetNeighbor(index);
                            if ((object) v == null)
                                continue;
                        }
                        else
                        {
                            IMyPathEdge<V> edge = this.m_currentVertex.GetEdge(index);
                            if (this.m_edgeTraversable(edge))
                            {
                                v = edge.GetOtherVertex(this.m_currentVertex);
                                if ((object) v == null)
                                    continue;
                            }
                            else
                                continue;
                        }
                        if ((this.m_vertexTraversable == null || this.m_vertexTraversable(v)) &&
                            v.PathfindingData.Timestamp != this.m_parent.m_timestamp)
                            this.m_parent.m_dfsStack.Push(v);
                    }
                    if (this.m_vertexFilter == null || this.m_vertexFilter(this.m_currentVertex))
                        return true;
                }
                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}