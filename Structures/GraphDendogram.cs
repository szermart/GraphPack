using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphDendogram
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************

        internal int Count
        {
            get
            {
                return GetCount();
            }
        }
        internal int MaxLevel
        {
            get
            {
                return GetMaxLevel();
            }
        }


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        

        //
        //Constructor
        //
        internal GraphDendogram(Graph SourceGraph) 
        {
            this.SourceGraph = SourceGraph;
            InitializeComponent();
            BuildSourceNode();
        }

        //
        //Common initializer
        //
        private void InitializeComponent()
        {
            this.ItemIndex = new GraphIndex();
            UnitGraphFactory F = new UnitGraphFactory();
            this.Dendogram = (UnitGraph)F.CreateGraph("Dendogram"); 
        }

        //
        //Build source node
        //
        private void BuildSourceNode()
        {
            AddNode(SourceGraph);
            GraphNode N = Dendogram.FindNode(SourceGraph.ID);
            N.SetOrder(GraphGlobals.InitialItemLevel);
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Add edge
        //
        internal void AddEdge(Graph SourceGraph, Graph SinkGraph)
        {
            AddNode(SourceGraph);
            AddNode(SinkGraph);
            Dendogram.AddEdge(SourceGraph.ID, SinkGraph.ID, false);
            SetNodeLevels();
        }

        //
        //Get nodes by level
        //
        internal Graph[] GetLevelSlice(int Level)
        {
            List<Graph> L = new List<Graph>();
            foreach (GraphNode N in Dendogram.NodeSet)
            {
                if (N.Order == Level)
                {
                    Graph G = ItemIndex.Find(N.ID);
                    L.Add(G);
                }
            }
            return L.ToArray();
        }

        //
        //Get community 
        //
        internal Graph GetCommunity(string ID)
        {
            Graph G = ItemIndex.Find(ID);
            return G;
        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Add node graph
        //
        private void AddNode(Graph G)
        {
            bool HasGraph = (ItemIndex.Find(G) != null);
            if (!HasGraph)
            {
                ItemIndex.Insert(G);
                Dendogram.AddNode(G.ID);
            }
        }

        //
        //Set node levels
        //
        private void SetNodeLevels()
        {
            GraphNode N = Dendogram.FindNode(SourceGraph.ID);
            SubSetNodeLevels(N, GraphGlobals.InitialItemLevel);
        }

        //
        //Recursive support to set node levels
        //
        private void SubSetNodeLevels(GraphNode N, int Level)
        {
            N.SetOrder(Level);
            foreach (GraphEdge E in N.OutBoundEdges)
                SubSetNodeLevels(E.SinkNode, Level + 1);
        }

        //
        //Get count
        //
        private int GetCount()
        {
            int n = Dendogram.NodeCount;
            return n;
        }

        //
        //Get max level
        //
        private int GetMaxLevel()
        {
            int m = int.MinValue;
            foreach (GraphNode N in Dendogram.NodeSet)
                m = Math.Max(m, N.Order);
            return m;
        }


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private Graph SourceGraph;
        private GraphIndex ItemIndex;
        private UnitGraph Dendogram;

    }
}
