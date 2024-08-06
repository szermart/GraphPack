using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.BTree;

namespace GraphPack
{
    internal class AdjacencyIndex : BTree<Adjacency>
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal AdjacencyIndex() : base(GraphGlobals.AdjacencyTreeOrder) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement key builder
        //
        protected override string BuildItemKey(Adjacency Item)
        {
            string Key = Item.SinkNode.ID;
            Key = Key + "-";
            Key = Key + Item.Metric.IndexKey;
            return Key;
        }

    }
}
