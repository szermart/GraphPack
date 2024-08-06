using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class UnitEdgeMetric : EdgeMetric 
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal UnitEdgeMetric() : base(new double[1] { 1 }) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "UnitMetric";
            return s;
        }

    }
}
