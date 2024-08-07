using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class CliqueFunctions
    {


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal CliqueFunctions(Graph SourceGraph)
        {
            this.SourceGraph = SourceGraph.Copy();
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************
        
        //
        //Find maximal clique (Bron-Kerbosch algorithm)
        //
        internal Graph[] MaximalCliques()
        {

            //Initialize
            Graph G = null;
            Graph K = SourceGraph;
            List<string> R = new List<string>();
            List<string> P = K.NodeList.ToList();
            List<string> X = new List<string>();
            List<List<string>> C = new List<List<string>>();

            //Build clique set
            BK(R, P, X, K, C);

            //Convert node set to graphs            
            GraphSizeHeap H = new GraphSizeHeap();
            foreach (List<string> NSet in C)
            {
                if (NSet.Count > 2)
                {
                    G = TrimGraph(K, NSet);
                    H.Push(G);
                }
            }

            //Select largest graphs            
            G = H.Peek();
            List<Graph> L = new List<Graph>();  
            int n = (G == null) ? 0 : G.NodeCount;
            while (((G = H.Pop()) != null) && (G.NodeCount >= n))
                L.Add(G);

            //Wrap up
            return L.ToArray();

        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Implement Bron-Kerbosch algorithm
        //
        private void BK(List<string> R, List<string> P, List<string> X, Graph G, List<List<string>> L)
        {

            //Capture clique
            if ((P.Count == 0) && (X.Count == 0))
                L.Add(R);
            else
            {

                //Continue clique search
                List<string> Candidates = CopyList(P);
                foreach (string NodeID in Candidates)
                {
                    P.Remove(NodeID);
                    List<string> R2 = CopyList(R);
                    R2.Add(NodeID);
                    GraphNode V = G.FindNode(NodeID);
                    List<string> N = GetNeighbors(V);
                    List<string> P2 = Intersect(P, N);
                    List<string> X2 = Intersect(X, N);
                    BK(R2, P2, X2, G, L);
                    X.Add(NodeID);
                }

            }

        }

        //
        //Intersect list
        //
        private List<string> Intersect(List<string> A, List<string> B)
        {

            //Initialize
            string[] a = A.ToArray();
            string[] b = B.ToArray();
            List<string> L = new List<string>();

            //Copy string data
            foreach (string s in a)
                if (b.Contains(s))
                    L.Add(s);

            //Wrap up
            return L;

        }

        //
        //Get neighbours
        //
        private List<string> GetNeighbors(GraphNode N)
        {
            List<string> L = new List<string>();
            foreach (GraphNode V in N.OutboundNeighbors)
                L.Add(V.ID);
            return L;
        }        

        //
        //Trim graph
        //
        private Graph TrimGraph(Graph K, List<string> NSet)
        {
            Graph G = K.Shell();
            foreach (string NodeID in NSet)
                G.AddNode(NodeID);
            G = K.Intersect(G);
            return G;
        }

        //
        //Copy list
        //
        private List<string> CopyList(List<string> K)
        {
            List<string> L = new List<string>();
            L.AddRange(K);            
            return L;
        }


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private Graph SourceGraph;

    }
}
