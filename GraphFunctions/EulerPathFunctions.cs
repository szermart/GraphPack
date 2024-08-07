using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class EulerPathFunctions
    {        


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal EulerPathFunctions(Graph SourceGraph)
        {
            this.SourceGraph = SourceGraph;
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Find euler path
        //
        internal Graph Path()
        {
            Graph G = HasPath() ? BuildPath() : SourceGraph.Shell();
            return G;
        }

        //
        //Find Euler circuit
        //
        internal Graph Circuit()
        {
            Graph G = HasCircuit() ? BuildPath() : SourceGraph.Shell();            
            return G;
        }        


        //****************************************************************************************************************************************************************
        //Support methods
        //****************************************************************************************************************************************************************

        //
        //Check if graph has Euler Path
        //
        private bool HasPath()
        {
            Graph G = SourceGraph;
            int n = OddNodeCount(G);
            bool b = (n == 2); 
            b = b && G.IsConnected;
            return b;
        }

        //
        //Check if graph has Euler Circuit
        //
        private bool HasCircuit()
        {
            Graph G = SourceGraph;
            int n = OddNodeCount(G);
            bool b = (n == 0);
            b = b && G.IsConnected;
            return b;
        }

        //
        //Build path (Fleury's algorithm)
        //
        private Graph BuildPath()
        {

            //Initialize
            Graph G = SourceGraph.Copy();
            Graph K = G.Shell();            

            //Walk through graph to build path            
            GraphNode N = GetFirstNode();            
            while (G.EdgeCount > 0)
            {

                //Select graph edge
                int i = -1;
                int j = N.OutBoundEdges.Length - 1;
                GraphEdgeIndex Bridges = FindNodeBridges(G, N);
                while ((++i <= j) && (Bridges.Find(N.OutBoundEdges[i]) != null));
                i = (i > j) ? 0 : i;                                
                GraphEdge E = N.OutBoundEdges[i];
                K.AddEdge(E);

                //Setup for next iteration.                
                GraphEdge F = E.ToDirected(false);
                G.RemoveEdge(F);
                G = G.RemoveOrphans();
                N = G.FindNode(E.SinkNode.ID);

            }

            //Wrap up
            return K;

        }

        //
        //Get first node 
        //
        private GraphNode GetFirstNode()
        {
            Graph G = SourceGraph;
            GraphNode N = G.NodeSet[0];
            int n = OddNodeCount(G);
            if (n != 0)
            {
                int i = -1;
                int j = G.NodeCount - 1;
                while ((++i <= j) && (!IsOdd((N = G.NodeSet[i]))));
            }
            return N;            
        }        

        //
        //Odd valence node count
        //
        private int OddNodeCount(Graph G)
        {
            int n = 0;
            foreach (GraphNode N in G.NodeSet)
                n = IsOdd(N) ? n + 1 : n;
            return n;
        }

        //
        //Check valence modulus
        //
        private bool IsOdd(GraphNode N)
        {
            double m = (double)N.Valence / 2.0;
            int r = (int)((m - (int)(m)) * 2);
            bool b = (r == 1);
            return b;
        }

        //
        //Build set of graph bridges
        //
        private GraphEdgeIndex FindNodeBridges(Graph G, GraphNode N)
        {

            //Initialize
            List<GraphEdge> Bridges = new List<GraphEdge>();

            //Test node edges for connectivity
            foreach (GraphEdge E in N.OutBoundEdges)
            {
                Graph H = G.Copy();
                GraphEdge F = E.ToDirected(false);
                H.RemoveEdge(F);
                if (!H.IsConnected)
                    Bridges.Add(F);                
            }            

            //Wrap up
            GraphEdgeIndex GEI = new GraphEdgeIndex();
            GEI.Insert(Bridges.ToArray());
            return GEI;            

        }


        //**************************************************************************************************************************************************************
        //Locals
        //**************************************************************************************************************************************************************
        private Graph SourceGraph;

    }
}
