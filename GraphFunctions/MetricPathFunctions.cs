using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace GraphPack
{
    internal class MetricPathFunctions
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal MetricPathFunctions() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Build short path
        //
        public Graph ShortPath(Graph G, string SourceNodeID, string SinkNodeID, int MetricIndex)
        {
            Graph K = MetricPath(G, SourceNodeID, SinkNodeID, MetricIndex, PathTypes.Short);
            return K;
        }

        //
        //Build long path
        //
        public Graph CriticalPath(Graph G, string SourceNodeID, string SinkNodeID, int MetricIndex)
        {
            Graph K = MetricPath(G, SourceNodeID, SinkNodeID,MetricIndex, PathTypes.Critical);
            return K;
        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Dykstra path algorithm
        //
        private Graph MetricPath (Graph G, string SourceNodeID, string SinkNodeID, int MetricIndex, PathTypes PathType)
        {

            //Initialize
            GraphPath P = null;
            GraphPathIndex GPI = new GraphPathIndex();
            GraphPathHeap H = new GraphPathHeap(PathType);
            G.ClearTopologicalOrder();
            int NodeOrder = GraphGlobals.DefaultTopologicalOrder;
            double NullPathMetric = (PathType == PathTypes.Short ? double.MaxValue : -1);

            //Validate source and sink nodes
            GraphNode N = G.FindNode(SinkNodeID);
            bool b = (N != null);
            N = G.FindNode(SourceNodeID);
            b = b && (N != null);            
            if (b)
            {

                //Initialize graph traversal          
                N.SetTopologicalOrder(++NodeOrder);
                P = new GraphPath(N, MetricIndex);
                H.Push(P);
                GPI.Insert(P);                

                //Traverse graph edges
                while ((P = H.Pop()) != null)                
                {
                    GraphNode V = P.SinkNode;
                    int StartOrder = G.FindNode(V.ID).Order;
                    GraphPath StartPath = GPI.Find(V.ID);
                    foreach (GraphEdge E in V.OutBoundEdges)
                    {
                        GraphNode W = E.SinkNode;                        
                        if (G.FindNode(W.ID).Order == 0)
                            W.SetTopologicalOrder(++NodeOrder);                        
                        if (W.Order > StartOrder)                         
                        {
                            GraphPath EndPath = new GraphPath(E, StartPath);
                            P = GPI.Find(EndPath);                            
                            double PathMetric = (P == null) ? NullPathMetric: P.Metric;
                            int c = EndPath.Metric.CompareTo(PathMetric);
                            c = (PathType == PathTypes.Short ? ((c < 0) ? 0 : 1) : ((c > 0) ? 0 : 1));
                            if (c == 0)   
                            {                                
                                GPI.Insert(EndPath);
                                H.Push(EndPath);
                            }
                        }
                    }                    
                }

            }

            //Build path graph
            Graph K = G.Shell();
            P = GPI.Find(SinkNodeID);
            GraphEdge[] Path = P.ToEdgeSet();
            foreach (GraphEdge E in Path)
                K.AddEdge(E);

            //Wrap up
            G.ClearTopologicalOrder();
            return K;

        }

    }
}
