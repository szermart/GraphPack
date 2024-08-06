using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphSearchFunctions
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphSearchFunctions() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************        

        //
        //Build BFS graph
        //
        internal UnitGraph ToBFSGraph(Graph G, string NodeID)
        {

            //Initialize            
            GraphNodeQueue Q = new GraphNodeQueue();
            UnitGraphFactory UGF = new UnitGraphFactory();
            UnitGraph U = (UnitGraph)UGF.CreateGraph();
            int NodeOrder = GraphGlobals.DefaultTopologicalOrder;

            //Initialize node queue
            GraphNode N = G.FindNode(NodeID);
            if (N != null)
            {
                U.AddNode(N.ID);
                U.FindNode(N.ID).SetTopologicalOrder(++NodeOrder);
                Q.Push(N);
            }

            //Traverse graph to build unit graph
            while ((N = Q.Pop()) != null)
            {
                foreach (GraphEdge E in N.Edges)
                {
                    GraphNode V = U.FindNode(E.SinkNode.ID);
                    if (V == null)
                    {
                        V = E.SinkNode;
                        U.AddEdge(E.SourceNode.ID, V.ID, true);
                        U.FindNode(V.ID).SetTopologicalOrder(++NodeOrder);
                        Q.Push(V);
                    }
                }
            }

            //Wrap up
            return U;
            
        }

        //
        //Build DFS graph
        //
        internal UnitGraph ToDFSGraph(Graph G, string NodeID)
        {

            //Initialize
            UnitGraphFactory UGF =new UnitGraphFactory();
            UnitGraph U = (UnitGraph)UGF.CreateGraph();
            GraphNode N = G.FindNode(NodeID);

            //Traverse graph to build unit graph
            if (N != null)
                SubDFS(G, U, N, 0);

            //Wrap up
            return U;

        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //DFS recursion support
        //
        private void SubDFS(Graph G, UnitGraph U, GraphNode N, int Order)
        {
            int TOrder = Order + 1;
            foreach (GraphEdge E in N.Edges)
            {
                GraphNode V = U.FindNode(E.SinkNode.ID);
                if (V == null)
                {
                    U.AddEdge(E.SourceNode.ID, V.ID, true);
                    U.FindNode(V.ID).SetTopologicalOrder(TOrder);
                    SubDFS(G, U, V, TOrder);
                }
            }
        }

    }
}
