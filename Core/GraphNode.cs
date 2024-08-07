using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public class GraphNode
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************
        internal string ID { get; private set; }        
        internal int Order { get; private set; }
        internal GraphEdge[] OutBoundEdges { get; private set; }
        internal int Valence
        {
            get
            {
                return GetValence();
            }
        }
        internal GraphNode[] OutboundNeighbors
        {
            get
            {
                return GetOutboundNeighbors();
            }
        }


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        

        //
        //Constructor
        //
        internal GraphNode(string ID)
        {
            this.ID = ID;
            InitializeComponent();
        }

        //
        //Common initializer
        //
        private void InitializeComponent()
        {
            this.Order = 0;
            this.AI = new AdjacencyIndex();
            this.OutBoundEdges = new GraphEdge[0];
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Add adjacency
        //
        internal void AddAdjacency(GraphNode N, EdgeMetric Metric)
        {
            AdjacencyFactory AF = new AdjacencyFactory();
            Adjacency A = AF.Create(N, Metric);
            Adjacency X = AI.Find(A);
            if (X == null)
            {
                AI.Insert(A);
                OutBoundEdges = BuildEdgeSet();
            }
        }

        //
        //Remove adjacency
        //
        internal void RemoveAdjacency(Adjacency A)
        {
            AI.Delete(A);
            OutBoundEdges = BuildEdgeSet();
        }

        //
        //Remove adjacency by sink node
        //
        internal void RemoveAdjacency(GraphNode N)
        {
            foreach (Adjacency A in AI.Scan())
                if (A.SinkNode.CompareTo(N) == 0)
                    RemoveAdjacency(A);
        }

        //
        //Find adjacency
        //
        internal GraphEdge FindAdjacency(string SinkNodeID)
        {
            int i = -1;
            Adjacency[] A = AI.Scan();
            int j = A.Length - 1;
            while ((++i <= j) && (A[i].SinkNode.ID != SinkNodeID));
            i = (i > j) ? -1 : i;
            GraphEdgeFactory EF = new GraphEdgeFactory();
            GraphEdge E = (i == -1) ? null : EF.Create(this, A[i].SinkNode, A[i].Metric, true);
            return E;
        }

        //
        //Clear adjacencies
        //
        internal void ClearAdjacencies()
        {
            AI.Clear();
            OutBoundEdges = new GraphEdge[0];
        }

        //
        //Clear topological order
        //
        internal void ClearTopologicalOrder()
        {
            SetTopologicalOrder(GraphGlobals.DefaultTopologicalOrder);
        }

        //
        //Set topological order
        //
        internal void SetTopologicalOrder(int Order)
        {
            this.Order = Order;
        }

        //
        //Comparator
        //
        internal int CompareTo(GraphNode N)
        {
            int c = this.ID.CompareTo(N.ID);
            return c;
        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Get edges
        //
        private GraphEdge[] BuildEdgeSet()
        {
            GraphEdgeFactory EF = new GraphEdgeFactory();
            List<GraphEdge> L = new List<GraphEdge>();
            foreach (Adjacency A in AI.Scan())
            {
                GraphEdge E = EF.Create(this, A.SinkNode, A.Metric, true);
                L.Add(E);
            }
            return L.ToArray();
        }

        //
        //Get valence
        //
        private int GetValence()
        {
            int v = AI.Count;
            return v;
        }
        
        //
        //Get neighbors
        //
        private GraphNode[] GetOutboundNeighbors()
        {
            List<GraphNode> L = new List<GraphNode>();
            foreach (GraphEdge E in OutBoundEdges)
                L.Add(E.SinkNode);
            return L.ToArray();
        }

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "NodeID = " + ID;
            s = s + " ; ";
            s = s + "Order = " + Convert.ToString(Order);
            return s;
        }


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private AdjacencyIndex AI;


    }
}
