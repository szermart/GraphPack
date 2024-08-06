using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.BTree;

namespace GraphPack
{
    internal class GraphEdgeIndex : BTree<GraphEdge>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphEdgeIndex() : base(GraphGlobals.GraphEdgeTreeOrder) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement key builder
        //
        protected override string BuildItemKey(GraphEdge Item)
        {
            string Key = Item.SourceNode.ID;
            Key = Key + Item.SinkNode.ID;
            Key = Key + Item.Metric.IndexKey;
            Key = Key + Item.IsDirected.ToString();
            return Key;
        }

    }
}
