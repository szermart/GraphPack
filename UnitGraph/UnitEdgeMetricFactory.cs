using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class UnitEdgeMetricFactory : EdgeMetricFactory 
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal UnitEdgeMetricFactory() {}


        //****************************************************************************************************************************************************************
        //Factory method
        //****************************************************************************************************************************************************************
        internal EdgeMetric Create()
        {
            UnitEdgeMetric M = new UnitEdgeMetric();
            return M;
        }
        internal protected override EdgeMetric Create(double[] Metrics)
        {
            UnitEdgeMetric M = new UnitEdgeMetric();
            return M;
        }

    }
}
