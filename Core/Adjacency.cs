using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class Adjacency
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************
        internal GraphNode SinkNode { get; private set; }
        internal EdgeMetric Metric { get; private set; }


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal Adjacency(GraphNode SinkNode, EdgeMetric Metric)
        {
            this.SinkNode = SinkNode;
            this.Metric = Metric;
        }


        //****************************************************************************************************************************************************************
        //Local functions
        //****************************************************************************************************************************************************************        

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "SinkNodeID = " + SinkNode.ID;
            s = s + " ; ";
            s = s + "Metric = " + Metric.ToString();
            return s;
        }

    }
}
