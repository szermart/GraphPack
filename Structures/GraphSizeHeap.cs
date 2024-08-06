using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.Heap;

namespace GraphPack
{
    internal class GraphSizeHeap : HeapBase<Graph>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphSizeHeap() : base(HeapTypes.Descending, true) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement comparator
        //
        public override int CompareItems(Graph ItemA, Graph ItemB)
        {
            int c = ItemA.NodeCount.CompareTo(ItemB.NodeCount);
            return c;
        }

    }
}
