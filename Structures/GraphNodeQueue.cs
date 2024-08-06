using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.QueueStack;

namespace GraphPack
{
    internal class GraphNodeQueue : QueueBase<GraphNode>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphNodeQueue() : base(false) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement comparator
        //
        public override int CompareItems(GraphNode ItemA, GraphNode ItemB)
        {
            int c = ItemA.CompareTo(ItemB);
            return c;
        }

    }
}
