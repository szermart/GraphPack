using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Metrics;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public class GraphEdge
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************
        internal GraphNode SourceNode { get; private set; }
        internal GraphNode SinkNode { get; private set; }
        internal EdgeMetric Metric { get; private set; }
        internal bool IsDirected { get; private set; }


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphEdge(GraphNode SourceNode, GraphNode SinkNode, EdgeMetric Metric, bool IsDirected)
        {
            this.SourceNode = SourceNode;
            this.SinkNode = SinkNode;
            this.Metric = Metric;
            this.IsDirected = IsDirected;
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Reverse edge
        //
        internal GraphEdge Reverse()
        {
            GraphEdgeFactory EF = new GraphEdgeFactory();
            GraphEdge E = EF.Create(SinkNode, SourceNode, Metric, IsDirected);
            return E;
        }

        //
        //To directed
        //
        internal GraphEdge ToDirected(bool SetDirected)
        {
            GraphEdgeFactory EF = new GraphEdgeFactory();
            GraphEdge E = EF.Create(SinkNode, SourceNode, Metric, SetDirected);
            return E;
        }

        //
        //Get metric
        //
        public EdgeMetric GetMetric()
        {
            EdgeMetric M = this.Metric;
            return M;
        }        


        //****************************************************************************************************************************************************************
        //Support methods
        //****************************************************************************************************************************************************************

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "Source = " + SourceNode.ID + " ; ";
            s = s + "Sink = " + SinkNode.ID + " ; ";
            s = s + "Metric = " + Metric.ToString() + " ; ";
            s = s + "IsDirected = " + IsDirected;
            return s;
        }

    }
}
