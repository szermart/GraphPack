using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphCommunityFunctions
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************        
        internal int CommunitySizeTolerance { get; private set; }


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        

        //
        //Constructor
        //
        internal GraphCommunityFunctions(Graph SourceGraph, int CommunitySizeTolerance)
        {
            this.SourceGraph = SourceGraph.Copy();
            this.CommunitySizeTolerance = CommunitySizeTolerance;
            InitializeComponent();
        }

        //
        //Common initializer
        //
        private void InitializeComponent()
        {
            this.Dendogram = ParseCommunities();
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Get community by level
        //

        internal Graph[] GetCommunities(int Level)
        {
            Graph[] G = Dendogram.GetLevelSlice(Level);
            return G;
        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************

        //
        //Parse communities
        //
        private GraphDendogram ParseCommunities()
        {

            //Initialize
            GraphDendogram GD = new GraphDendogram(SourceGraph);

            //Recursively build classifications
            SubParseCommunities(SourceGraph, GD);

            //Wrap up
            return GD;

        }

        //
        //Recursive support for dendogram classification
        //
        private void SubParseCommunities(Graph G, GraphDendogram GD)
        {

            //Check size tolerance
            if (G.NodeCount > CommunitySizeTolerance)
            {
                Graph[] C = IdentifyCommunities(G);
                foreach (Graph K in C)
                {
                    GD.AddEdge(G, K);
                    SubParseCommunities(K, GD);
                }
            }
        }

        //TODO--> Build this
        //Idenfify communities
        //
        private Graph[] IdentifyCommunities(Graph G)
        {
            Graph[] C = new Graph[0];
            return C;
        }


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private Graph SourceGraph;
        private GraphDendogram Dendogram;

    }
}
