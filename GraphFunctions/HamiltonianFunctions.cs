using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class HamiltonianFunctions
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************                
        
        internal HamiltonianFunctions(Graph SourceGraph)
        {
            this.SourceGraph = SourceGraph.Copy();
        }       


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Get path
        //
        internal Graph Path(string NodeID)
        {
            Graph G = BuildPath(NodeID, PathTypes.HamiltonianPath);
            return G;
        }

        //
        //Get circuit
        //
        internal Graph Circuit(string NodeID)
        {
            Graph G = HasCircuit() ? BuildPath(NodeID, PathTypes.HamiltonianCircuit) : SourceGraph.Shell();
            return G;
        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Check for circuit (Dirac's theorem)
        //
        private bool HasCircuit()
        {
            int i = -1;            
            GraphNode[] N = SourceGraph.NodeSet;
            int j = N.Length - 1;
            double RequiredDegree = (double)SourceGraph.NodeCount / 2.0;
            while ((++i <= j) && (double)N[i].Valence > RequiredDegree);
            i = (i > j) ? -1 : 1;
            bool b = (i == -1);
            return b;
        }

        //
        //Find paths
        //
        private Graph BuildPath(string NodeID, PathTypes PathType)
        {

            //Initialize
            GraphPath P = null;
            GraphPathHeap H = new GraphPathHeap();

            //Traverse graph nodes
            GraphNode N = SourceGraph.FindNode(NodeID);
            if (N != null)
            {
                P = new GraphPath(N, 0);
                SubFindPaths(SourceGraph, P, H, PathType);                
            }

            //Wrap up
            P = H.Pop();
            Graph G = (P == null) ? SourceGraph.Shell() : P.ToGraph(SourceGraph);
            return G;
            
        }

        //
        //Find paths
        //
        private void SubFindPaths(Graph G, GraphPath P, GraphPathHeap H, PathTypes PathType)
        {

            //Check for detected path
            int n = G.NodeCount;
            if (P.NodeCount == n)
            {
                if (PathType == PathTypes.HamiltonianCircuit)
                {
                    GraphEdge E = G.FindEdge(P.SinkNode.ID, P.SourceNode.ID);
                    P = new GraphPath(E, P);
                }
                H.Push(P);
            }
            else
            {
                //Extend search to adjacent nodes
                foreach (GraphEdge E in G.FindNode(P.SinkNode.ID).OutBoundEdges)
                {
                    if (!P.Contains(E.SinkNode.ID))
                    {
                        GraphPath X = new GraphPath(E, P);
                        SubFindPaths(G, X, H, PathType);
                    }
                }
            }            

        }        


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************        
        private Graph SourceGraph;        

    }
}
