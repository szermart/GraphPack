using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class UnitGraphFactory : GraphFactory 
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal UnitGraphFactory() : base(new UnitEdgeMetricFactory()) {}


        //****************************************************************************************************************************************************************
        //Factory methods
        //****************************************************************************************************************************************************************        
        public override Graph CreateGraph()
        {
            UnitGraph U = new UnitGraph();
            return U;
        }
        public override Graph CreateGraph(string ID)
        {
            UnitGraph U = new UnitGraph();
            return U;
        }
        public override EdgeMetric CreateEdgeMetric(double[] Metrics)
        {
            UnitEdgeMetric M = new UnitEdgeMetric();
            return M;
        }

    }
}
