using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphSetFunctions
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphSetFunctions() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Union
        //
        internal UnitGraph Union(Graph G, Graph H)
        {
            UnitGraph U = G.ToUnitGraph();
            foreach (GraphNode N in H.NodeSet)
                U.AddNode(N.ID);
            foreach (GraphEdge E in H.EdgeSet)
                U.AddEdge(E.SourceNode.ID, E.SinkNode.ID, E.IsDirected);
            return U;
        }

        //
        //Intersection
        //
        internal Graph Intersect(Graph G, Graph H)
        {
            Graph I = G.Copy();
            foreach(GraphNode N in G.NodeSet)
                if (H.FindNode(N.ID) == null)
                    I.RemoveNode(N.ID);           
            return I;
        }

        //
        //Difference
        //
        internal Graph Subtract(Graph G, Graph H)
        {
            Graph K = G.Copy();
            foreach (GraphNode N in G.NodeSet)
                if (H.FindNode(N.ID) != null)
                    K.RemoveNode(N.ID);
            return K;
        }        

        //
        //Invert graph
        //
        internal UnitGraph Invert(Graph G)
        {
            UnitGraph U = G.ToUnitGraph();
            U = U.ToCompleteGraph();
            foreach (GraphEdge E in U.EdgeSet)
                if (G.FindEdge(E.SourceNode.ID, E.SinkNode.ID) != null)
                    U.RemoveEdge(E);
            return U;
        }

        //
        //Dominating set
        //
        internal Graph BackBone(Graph K)
        {

            //Initialize
            Graph G = K.Copy();
            Graph I = K.Shell();
            NodeValenceHeap H = new NodeValenceHeap();

            //Iterative search for dominant nodes
            while (G.EdgeCount > 0) 
            {
                H.InArray(G.NodeSet);
                GraphNode N = H.Pop();
                I.AddNode(N.ID);
                G.RemoveNode(N.ID);
            }

            //Wrap up
            G = K.Copy();
            G = G.Intersect(I);            
            return G;

        }

    }
}
