using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.BTree;

namespace GraphPack
{
    internal class GraphPathIndex : BTree<GraphPath>
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphPathIndex() : base(BTreeDuplicateHandlingOptions.Replace) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement key builder
        //
        protected override string BuildItemKey(GraphPath Item)
        {
            string Key = Item.SinkNode.ID;
            return Key;
        }

    }
}
