﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public abstract class GraphFilter
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        public GraphFilter() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************        

        //
        //Use edge
        //
        internal protected abstract bool UseEdge(GraphEdge E);      

    }
}
