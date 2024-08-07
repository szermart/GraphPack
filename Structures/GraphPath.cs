using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphPath
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************
        internal int Size { get; private set; }
        internal double Metric { get; private set; }
        internal GraphNode SourceNode { get; private set; }        
        internal GraphNode SinkNode { get; private set; }
        internal int NodeCount
        {
            get
            {
                return GetNodeCount();
            }
        }

        
        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphPath(GraphNode SourceNode, int MetricIndex)
        {
            this.SourceNode = SourceNode;
            this.SinkNode = SourceNode;
            this.MetricIndex = MetricIndex;
            this.InternalEdge = null;
            this.Size = 0;                        
            this.Metric = 0;
            this.InnerPath = null;
        }
        internal GraphPath(GraphEdge E, GraphPath P)
        {
            this.SourceNode = P.SourceNode;
            this.SinkNode = E.SinkNode;
            this.MetricIndex = P.MetricIndex;
            this.InternalEdge = E;
            this.Size = P.Size + 1;            
            this.Metric = P.Metric + InternalEdge.Metric[MetricIndex];            
            this.InnerPath = P;
        }        


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************        

        //
        //Check if graph path contains specified sink node
        //
        internal bool Contains(string NodeID)
        {
            GraphPath P = this;
            bool HasNode = (P.SourceNode.ID == NodeID);
            while (!HasNode && P.Size > 0)
            {
                HasNode = (P.SinkNode.ID == NodeID);
                P = P.InnerPath;
            }
            return HasNode;
        }

        //TODO--> Cleanup
        //To edges
        //
        //internal GraphEdge[] ToEdgeSet()
        //{
        //    GraphPath P = this;
        //    List<GraphEdge> L = new List<GraphEdge>();
        //    while (P.InternalEdge != null) 
        //    {
        //        L.Add(P.InternalEdge);
        //        P = P.InnerPath;
        //    }
        //    L.Reverse();
        //    return L.ToArray();            
        //}

        //TODO--> Cleanup
        //To node set
        //
        //internal GraphNode[] ToNodeSet()
        //{
        //    GraphNodeIndex GNI = new GraphNodeIndex();
        //    GNI.Insert(SourceNode);
        //    GNI.Insert(SinkNode);
        //    GraphPath P = this;
        //    while ((P = P.InnerPath) != null)
        //        GNI.Insert(P.SinkNode);            
        //    return GNI.Scan();
        //}       
        
        //
        //Convert path to graph
        //
        internal Graph ToGraph(Graph Template)
        {
            Graph G = Template.Shell();
            foreach (GraphEdge E in ToEdgeSet())
                G.AddEdge(E);
            return G;
        }



        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Get node size
        //
        private int GetNodeCount()
        {
            GraphNode[] N = ToNodeSet();
            int n = N.Length;
            return n;
        }

        //
        //To node set
        //
        private GraphNode[] ToNodeSet()
        {
            GraphNodeIndex GNI = new GraphNodeIndex();
            GNI.Insert(SourceNode);
            GNI.Insert(SinkNode);
            GraphPath P = this;
            while ((P = P.InnerPath) != null)
                GNI.Insert(P.SinkNode);
            return GNI.Scan();
        }


        //
        //To edges
        //
        private GraphEdge[] ToEdgeSet()
        {
            GraphPath P = this;
            List<GraphEdge> L = new List<GraphEdge>();
            while (P.InternalEdge != null)
            {
                L.Add(P.InternalEdge);
                P = P.InnerPath;
            }
            L.Reverse();
            return L.ToArray();
        }

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "Source = " + SourceNode.ID + " ; ";
            s = s + "Sink = " + SinkNode.ID + " ; ";
            s = s + "Metric = " + Convert.ToString(Metric) + " ; ";
            s = s + "Hops = " + Convert.ToString(Size);
            return s;
        }


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private GraphEdge InternalEdge;
        private GraphPath InnerPath;
        private int MetricIndex;

    }
}
