using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public class UnitGraph : Graph
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        public UnitGraph() : base("UnitGraph", new UnitGraphFactory()) {}        


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************        

        //
        //Add edge does not need to specify metric value.  Always use unit edge metric.
        //        
        internal void AddEdge(string SourceNodeID, string SinkNodeID, bool IsDirected)
        {
            UnitEdgeMetricFactory EMF = new UnitEdgeMetricFactory();
            UnitEdgeMetric M = (UnitEdgeMetric)EMF.Create();
            base.AddEdge(SourceNodeID, SinkNodeID, M.ToArray(), IsDirected);
        }

    }
}
