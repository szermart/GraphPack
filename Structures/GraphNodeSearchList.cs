using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.SList;

namespace GraphPack
{
    internal class GraphNodeSearchList : SearchList<GraphNode>
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphNodeSearchList() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement comparator
        //
        protected override string BuildID(GraphNode Item)
        {
            string Key = Item.ID;
            return Key;
        }

    }
}
