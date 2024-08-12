namespace GraphPack
{
    public abstract class Graph
    {

        //*****************************************************************************************************************************************************************
        //Properties
        //*****************************************************************************************************************************************************************
        public string ID { get; private set; }
        internal int NodeCount
        {
            get
            {
                return GetNodeCount();
            }
        }
        internal int EdgeCount
        {
            get
            {
                return GetEdgeCount();
            }
        }
        internal GraphNode[] NodeSet
        {
            get
            {
                return GetNodeSet();
            }
        }        
        internal string[] NodeList
        {
            get
            {
                return GetNodeList();
            }
        }        
        internal GraphNode[] TopologicalNodeSet
        {
            get
            {
                return GetTopologicalNodeSet();
            }
        }
        internal GraphEdge[] EdgeSet
        {
            get
            {
                return GetEdgeSet();
            }
        }
        internal bool IsDirected
        {
            get
            {
                return CheckIsDirected();
            }
        }
        internal bool IsConnected
        {
            get
            {
                return CheckIsConnected();
            }
        }


        //*****************************************************************************************************************************************************************
        //Construction and initialization
        //*****************************************************************************************************************************************************************

        //
        //Constructors
        //              
        public Graph(string ID)
        {
            this.ID = ID;
            this.MyFactory = new UnitGraphFactory();            
            this.IsUnitGraph = true;
            InitializeComponent();
        }
                      
        public Graph(string ID, GraphFactory GF)
        {
            this.ID = ID;
            this.MyFactory = GF;
            this.IsUnitGraph = false;
            InitializeComponent();
        }

        //
        //Common initializer
        //
        private void InitializeComponent()
        {
            this.GNI = new GraphNodeIndex();
        }


        //*****************************************************************************************************************************************************************
        //Operating methods
        //*****************************************************************************************************************************************************************

        //
        //Add graph node
        //
        public GraphNode AddNode(string NodeID)
        {
            GraphNode N = GNI.Find(NodeID);
            if (N == null)
            {
                GraphNodeFactory GNF = new GraphNodeFactory();
                N = GNF.Create(NodeID);
                GNI.Insert(N);
            }
            return N;
        }

        //
        //Remove node
        //
        public void RemoveNode(string NodeID)
        {
            GraphNode N = GNI.Find(NodeID);
            if (N != null)
                foreach (GraphNode V in GNI.Scan())
                    V.RemoveAdjacency(N);
            GNI.Delete(NodeID);
        }

        //
        //Find specified node
        //
        public GraphNode FindNode(string NodeID)
        {
            GraphNode N = GNI.Find(NodeID);
            return N;
        }

        //
        //Add edge
        //
        internal void AddEdge(GraphEdge E)
        {
            EdgeMetric EM = IsUnitGraph ? new UnitEdgeMetricFactory().Create() : E.Metric;            
            AddEdge(E.SourceNode.ID, E.SinkNode.ID, EM.ToArray(), E.IsDirected);            
        }

        //
        //Add edge
        //
        internal protected void AddEdge(string SourceNodeID, string SinkNodeID, double[] Metrics, bool IsDirected)
        {
            GraphNode N = AddNode(SourceNodeID);
            GraphNode V = AddNode(SinkNodeID);
            EdgeMetric M = MyFactory.CreateEdgeMetric(Metrics); 
            N.AddAdjacency(V, M);
            if (!IsDirected)
                V.AddAdjacency(N, M);
        }

        //
        //Remove edge
        //
        internal void RemoveEdge(GraphEdge E)
        {
            RemoveEdge(E.SourceNode.ID, E.SinkNode.ID, E.Metric.ToArray(), E.IsDirected);
        }        

        //
        //Remove edge
        //
        protected void RemoveEdge(string SourceNodeID, string SinkNodeID, double[] Metrics, bool IsDirected)
        {
            GraphNode N = GNI.Find(SourceNodeID);
            GraphNode V = GNI.Find(SinkNodeID);
            if ((N != null) && (V != null))
            {
                EdgeMetric M = MyFactory.CreateEdgeMetric(Metrics);
                AdjacencyFactory AF = new AdjacencyFactory();
                Adjacency A = AF.Create(V, M);
                N.RemoveAdjacency(A);
                if (!IsDirected)
                {
                    A = AF.Create(N, M);
                    V.RemoveAdjacency(A);
                }
            }
        }

        //
        //Find edge specified by source node and sink node
        //
        public GraphEdge FindEdge(string SourceNodeID, string SinkNodeID)
        {            
            GraphNode N = GNI.Find(SourceNodeID);
            GraphEdge E = (N == null) ? null : N.FindAdjacency(SinkNodeID);            
            return E;
        }

        //
        //Clear graph
        //
        public void Clear()
        {
            GNI.Clear();
        }

        //
        //Clear edges
        //
        public void ClearEdges()
        {
            foreach (GraphNode N in GNI.Scan())
                N.ClearAdjacencies();
        }

        //*****************************************************************************************************************************************************************
        //Advanced graph functions
        //*****************************************************************************************************************************************************************

        //
        //Set topological order
        //
        public void SetTopologicalOrder(string NodeID)
        {
            ClearTopologicalOrder();
            UnitGraph B = ToBFSGraph(NodeID);
            SetTopology(B);
        }        

        //
        //Clear topological order
        //
        public void ClearTopologicalOrder()
        {
            foreach (GraphNode N in NodeSet)
                N.ClearOrder();
        }

        //
        //Graph union
        //
        public UnitGraph Union(Graph G)
        {
            GraphSetFunctions GSF = new GraphSetFunctions();
            UnitGraph U = GSF.Union(this, G);
            return U;            
        }

        //
        //Graph intersection
        //
        public Graph Intersect(Graph G)
        {
            GraphSetFunctions GSF = new GraphSetFunctions();
            Graph I = GSF.Intersect(this, G);
            return I;
        }

        //
        //Graph difference
        //
        public Graph Subtract(Graph G)
        {
            GraphSetFunctions GSF = new GraphSetFunctions();
            Graph S = GSF.Subtract(this, G);
            return S;
        }

        //
        //Invert graph
        //
        public UnitGraph Invert()
        {
            GraphSetFunctions GSF = new GraphSetFunctions();
            UnitGraph U = GSF.Invert(this);
            return U;
        }

        //
        //Build BFS graph
        //
        public UnitGraph ToBFSGraph(string NodeID)
        {
            GraphSearchFunctions GSF = new GraphSearchFunctions();            
            UnitGraph U = GSF.ToBFSGraph(this, NodeID);
            return U;
        }

        //
        //Build DFS graph
        //
        internal UnitGraph ToDFSGraph(string NodeID)
        {
            GraphSearchFunctions GSF = new GraphSearchFunctions();
            UnitGraph U = GSF.ToDFSGraph(this, NodeID);
            return U;
        }

        //
        //Convert to unit graph
        //
        internal UnitGraph ToUnitGraph()
        {        
            GraphConversionFunctions GCF = new GraphConversionFunctions();            
            UnitGraph U = GCF.ToUnitGraph(this);
            return U;            
        }

        //
        //Convert to complete graph
        //
        public UnitGraph ToCompleteGraph()
        {
            GraphConversionFunctions GCF = new GraphConversionFunctions();                
            UnitGraph U = GCF.ToCompleteGraph(this);
            return U;
        }

        //
        //Remove orphans
        //
        internal Graph RemoveOrphans()
        {
            Graph G = this.Copy();
            foreach (GraphNode N in G.NodeSet)
                if (N.Valence == 0)
                    G.RemoveNode(N.ID);
            return G;            
        }

        //
        //Get outbound neighbours
        //
        internal GraphNode[] OutBoundNeighbours(string NodeID)
        {
            GraphNodeSearchList L = new GraphNodeSearchList();
            foreach (GraphEdge E in OutBoundEdges(NodeID))
                L.Add(E.SinkNode);
            return L.ToList().ToArray();
        }

        //
        //Get outbound edges
        //
        internal GraphEdge[] OutBoundEdges(string NodeID)
        {            
            GraphNode N = GNI.Find(NodeID);
            GraphEdge[] E = (N == null) ? new GraphEdge[0] : N.OutBoundEdges;
            return E;
        }

        //
        //Get inbound neighbours
        //
        internal GraphNode[] InBoundNeighbors(string NodeID)
        {
            GraphNodeSearchList L = new GraphNodeSearchList();
            foreach (GraphEdge E in InBoundEdges(NodeID))
                L.Add(E.SourceNode);
            return L.ToList().ToArray();
        }

        //
        //Get inbound edges
        //
        internal GraphEdge[] InBoundEdges(string NodeID)
        {
            List<GraphEdge> L = new List<GraphEdge>();
            GraphNode V = GNI.Find(NodeID);
            if (V != null)
                foreach (GraphNode N in NodeSet)
                    foreach (GraphEdge E in N.OutBoundEdges)
                        if (E.SinkNode.ID == NodeID)
                            L.Add(E);
            return L.ToArray();
        }

        //
        //Dominating set
        //
        public Graph BackBone()
        {
            GraphSetFunctions GSF = new GraphSetFunctions();
            Graph G = GSF.BackBone(this);
            return G;
        }

        //
        //Apply graph filter
        //
        public Graph Filter(GraphFilter GF)
        {
            Graph G = this.Clone();
            foreach (GraphEdge E in EdgeSet)
                if (GF.UseEdge(E))
                    G.AddEdge(E);
            return G;
        }
        
        //
        //Reverse graph
        //
        public Graph Reverse()
        {
            GraphConversionFunctions GCF = new GraphConversionFunctions();            
            Graph G = GCF.Reverse(this);
            return G;
        }

        //
        //Build short path
        //
        public Graph ShortPath(string SourceNodeID, string SinkNodeID, int MetricIndex)
        {
            MetricPathFunctions MPF = new MetricPathFunctions(this);
            Graph G = MPF.ShortPath(SourceNodeID, SinkNodeID, MetricIndex);
            return G;
        }

        //
        //Build critical path
        //
        public Graph CriticalPath(string SourceNodeID, string SinkNodeID, int MetricIndex)
        {
            MetricPathFunctions MPF = new MetricPathFunctions(this);
            Graph G = MPF.CriticalPath(SourceNodeID, SinkNodeID, MetricIndex);
            return G;
        }

        //
        //Find Euler path
        //
        public Graph EulerPath()
        {
            EulerPathFunctions EPF = new EulerPathFunctions(this);
            Graph G = EPF.Path();
            return G;
        }

        //
        //Find Euler Circuit
        //
        public Graph EulerCircuit()
        {
            EulerPathFunctions EPF = new EulerPathFunctions(this);
            Graph G = EPF.Circuit();
            return G;
        }

        //
        //Find Hamiltonian path
        //
        public Graph HamiltonianPath(string NodeID)
        {
            HamiltonianFunctions HF = new HamiltonianFunctions(this);
            Graph G = HF.Path(NodeID);
            return G;
        }

        //
        //Find Hamiltonian circuit
        //
        public Graph HamiltonianCircuit(string NodeID)
        {
            HamiltonianFunctions HF = new HamiltonianFunctions(this);
            Graph G = HF.Circuit(NodeID);
            return G;
        }

        //
        //Find maximal cliques
        //
        public Graph[] MaximalCliques()
        {
            CliqueFunctions CF = new CliqueFunctions(this);
            Graph[] G = CF.MaximalCliques();
            return G;
        }

        //
        //Build adjacency matrix
        //
        public double[,] ToAdjacencyMatrix(int MetricIndex)
        {
            double[,] AM = ToAdjacencyMatrix(MetricIndex, -1);
            return AM;
        }

        //
        //Build adjacency matrix
        //
        public double[,] ToAdjacencyMatrix(int MetricIndex, double DefaultValue)
        {

            //Initialize matrix
            int i = -1;
            int j = 0;
            int n = this.NodeCount;            
            double[,] AM = new double[n, n];
            n = n - 1;            
            while (++i <= n) 
            {
                j = -1;
                while (++j <= n)
                    AM[i, j] = DefaultValue;
            }

            //Build matrix values from graph edge metrics
            foreach (GraphEdge E in EdgeSet)
            {
                i = GetNodeIndex(E.SourceNode.ID);
                j = GetNodeIndex(E.SinkNode.ID);
                double m = E.Metric[MetricIndex];
                AM[i,j] = m;
            }

            //Wrap up
            return AM;

        }


        //******************************************************************************************************************************************************************
        //Copy functions
        //******************************************************************************************************************************************************************

        //
        //Create new graph shell
        //
        internal Graph Shell()
        {
            Graph G = MyFactory.CreateGraph(ID);
            return G;
        }

        //
        //Clone graph
        //
        public Graph Clone()
        {
            Graph G = Shell();
            foreach (GraphNode N in GNI.Scan())
                G.AddNode(N.ID);
            return G;
        }

        //
        //Copy graph
        //
        public Graph Copy()
        {
            Graph G = Clone();
            foreach (GraphEdge E in EdgeSet)
                G.AddEdge(E.SourceNode.ID, E.SinkNode.ID, E.Metric.ToArray(), E.IsDirected);
            return G;
        }

        //
        //Copy node topology
        //
        public Graph TopologicalCopy()
        {
            Graph G = Copy();
            G.SetTopology(this);            
            return G;
        }


        //*****************************************************************************************************************************************************************
        //Support functions
        //*****************************************************************************************************************************************************************        

        //
        //Get node set
        //
        private GraphNode[] GetNodeSet()
        {
            GraphNode[] NSet = GNI.Scan();
            return NSet;
        }

        //
        //Get node list
        //
        private string[] GetNodeList()
        {
            List<string> L = new List<string>();
            foreach (GraphNode N in GNI.Scan())
                L.Add(N.ID);
            return L.ToArray();
        }
        
        //
        //Get topological node set
        //
        private GraphNode[] GetTopologicalNodeSet()
        {
            TopologicalNodeHeap H = new TopologicalNodeHeap();
            H.InArray(NodeSet);
            return H.ToList().ToArray();
        }

        //
        //Get edge set
        //
        private GraphEdge[] GetEdgeSet()
        {
            GraphEdgeFactory EF = new GraphEdgeFactory();
            GraphEdgeIndex GEI = new GraphEdgeIndex();
            foreach (GraphNode N in GNI.Scan())
            {
                foreach (GraphEdge E in N.OutBoundEdges)
                {
                    GraphEdge R = E.Reverse();
                    GraphEdge V = GEI.Find(R);
                    if (V == null)
                        GEI.Insert(E);
                    else
                    {
                        GEI.Delete(R);
                        V = EF.Create(R.SourceNode, R.SinkNode, R.Metric, false);
                        GEI.Insert(V);
                    }
                }
            }
            return GEI.Scan();
        }        

        //
        //Get node count
        //
        private int GetNodeCount()
        {
            int n = GNI.Count;
            return n;
        }

        //
        //Get edge count
        //
        private int GetEdgeCount()
        {
            int n = EdgeSet.Length;
            return n;
        }

        //
        //Check if graph is connected
        //
        private bool CheckIsConnected()
        {
            bool b = false;
            GraphNode N = (NodeCount == 0) ? null : NodeSet[0];
            if (N != null)
            {
                GraphSearchFunctions GS = new GraphSearchFunctions();
                UnitGraph B = GS.ToBFSGraph(this, N.ID);
                int c = B.NodeCount.CompareTo(NodeCount);
                b = (c == 0);
            }
            return b;
        }

        //
        //Transfer topology
        //
        private void SetTopology(Graph G)
        {
            foreach (GraphNode N in G.NodeSet)
                FindNode(N.ID).SetOrder(N.Order);
        }        

        //
        //Get nodex index
        //
        private int GetNodeIndex(string NodeID)
        {
            int i = -1;
            GraphNode[] N = NodeSet;
            while (N[++i].ID != NodeID);
            return i;
        }

        //
        //Check if graph is directed
        //
        private bool CheckIsDirected()
        {
            int i = -1;
            GraphEdge[] E = EdgeSet;
            int j = E.Length - 1;
            while ((++i <= j) && !E[i].IsDirected);
            i = (i > j) ? -1 : 1;
            bool b = (i != -1);
            return b;
        }

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "ID = " + ID + " ; ";
            s = s + "Nodes = " + Convert.ToString(NodeCount) + " ; ";
            s = s + "Edges = " + Convert.ToString(EdgeCount);
            return s;
        }


        //*****************************************************************************************************************************************************************
        //Locals
        //*****************************************************************************************************************************************************************
        private GraphNodeIndex GNI;        
        private GraphFactory MyFactory;
        private bool IsUnitGraph;

    }
}
