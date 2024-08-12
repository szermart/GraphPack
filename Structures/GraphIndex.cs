using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.BTree;

namespace GraphPack
{
    internal class GraphIndex : BTree<Graph>
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphIndex() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement key builder
        //
        protected override string BuildItemKey(Graph Item)
        {
            string Key = Item.ID;
            return Key;
        }

    }
}
