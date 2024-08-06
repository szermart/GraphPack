using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.BTree;

namespace GraphPack
{
    internal class GraphNodeIndex : BTree<GraphNode>
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphNodeIndex() : base(GraphGlobals.NodeTreeOrder) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement key builder
        //
        protected override string BuildItemKey(GraphNode Item)
        {
            string Key = Item.ID;
            return Key;
        }

    }
}
