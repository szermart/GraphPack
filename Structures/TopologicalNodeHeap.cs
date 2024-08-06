using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.Heap;

namespace GraphPack
{
    internal class TopologicalNodeHeap :HeapBase<GraphNode>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal TopologicalNodeHeap() : base(HeapTypes.Ascending, true) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement comparator
        //
        public override int CompareItems(GraphNode ItemA, GraphNode ItemB)
        {
            int c = ItemA.Order.CompareTo(ItemB.Order);
            return c;
        }

    }
}
