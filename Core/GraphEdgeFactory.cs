using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphEdgeFactory
    {
        
        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphEdgeFactory() {}


        //****************************************************************************************************************************************************************
        //Factory method
        //****************************************************************************************************************************************************************
        internal GraphEdge Create(GraphNode SourceNode, GraphNode SinkNode, EdgeMetric Metric, bool IsDirected)
        {
            GraphEdge E = new GraphEdge(SourceNode, SinkNode, Metric, IsDirected);
            return E;
        }

    }
}
