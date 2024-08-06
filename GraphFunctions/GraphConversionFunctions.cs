using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphConversionFunctions
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphConversionFunctions() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Reverse graph
        //
        internal Graph Reverse(Graph G)
        {

            //Initialize
            Graph H = G.Clone();

            //Scan graph edges
            foreach (GraphEdge E in G.EdgeSet)
            {
                GraphEdge R = E.Reverse();
                H.AddEdge(R);
            }

            //Wrap up
            return H;

        }

        //
        //Convert to unit graph
        //
        internal UnitGraph ToUnitGraph(Graph G)
        {

            //Initialize
            UnitGraphFactory UGF = new UnitGraphFactory();
            UnitGraph U = (UnitGraph)UGF.CreateGraph(G.ID);

            //Copy nodes
            foreach (GraphNode N in G.NodeSet)
                U.AddNode(N.ID);

            //Create unit edges
            foreach (GraphEdge E in G.EdgeSet)
                U.AddEdge(E.SourceNode.ID, E.SinkNode.ID, E.IsDirected);

            //Wrap up
            return U;

        }

        //
        //To complete graph
        //
        internal UnitGraph ToCompleteGraph(Graph G)
        {

            //Initialize
            UnitGraphFactory UGF = new UnitGraphFactory();
            UnitGraph U = (UnitGraph)UGF.CreateGraph(G.ID);

            //Copy nodes
            foreach (GraphNode N in G.NodeSet)
                U.AddNode(N.ID);

            //Create edges
            foreach (GraphNode N in U.NodeSet)
                foreach (GraphNode V in U.NodeSet)
                    U.AddEdge(N.ID, V.ID, true);

            //Wrap up
            return U;

        }

        //
        //To directed graph
        //
        internal Graph ToDirectedGraph(Graph G, string NodeID)
        {

            //Initialize
            Graph D = G.Copy();
            GraphSearchFunctions GSF = new GraphSearchFunctions();
            D.SetTopologicalOrder(NodeID);
            D.ClearEdges();

            //Create directed edges according to graph topology
            foreach (GraphEdge E in G.EdgeSet)
            {                
                int SourceOrder = D.FindNode(E.SourceNode.ID).Order;
                int SinkOrder = D.FindNode(E.SinkNode.ID).Order;
                if (SinkOrder > SourceOrder)
                    D.AddEdge(E.SourceNode.ID, E.SinkNode.ID, E.Metric.ToArray(), true);
            }

            //Wrap up
            return D;

        }

    }
}
