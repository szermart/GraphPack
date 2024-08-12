using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    internal class GraphGlobals
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphGlobals() {}


        //****************************************************************************************************************************************************************
        //Global constants
        //****************************************************************************************************************************************************************
        internal const int NullItemLevel = -1;
        internal const int NodeTreeOrder = 10;
        internal const int InitialItemLevel = 0;
        internal const int AdjacencyTreeOrder = 10;
        internal const int GraphEdgeTreeOrder = 10;
        internal const int DefaultTopologicalOrder = 0;

    }
    

    //****************************************************************************************************************************************************************
    //Enumerations
    //****************************************************************************************************************************************************************        
    internal enum PathTypes
    {
        None = 0,
        Short = 1,
        Critical = 2,
        HamiltonianPath = 3,
        HamiltonianCircuit = 4
    };

}
