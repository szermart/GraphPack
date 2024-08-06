using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class AdjacencyFactory
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal AdjacencyFactory() {}


        //****************************************************************************************************************************************************************
        //Factory method
        //****************************************************************************************************************************************************************
        internal Adjacency Create(GraphNode N, EdgeMetric Metric)
        {
            Adjacency A = new Adjacency(N, Metric);
            return A;
        }

    }
}
