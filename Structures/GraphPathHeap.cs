using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.Heap;

namespace GraphPack
{
    internal class GraphPathHeap : HeapBase<GraphPath>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal GraphPathHeap() : base(HeapTypes.Ascending, true) { }
        internal GraphPathHeap(PathTypes PathType) : base((PathType == PathTypes.Short ? HeapTypes.Ascending : HeapTypes.Descending), true) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement comparator
        //
        public override int CompareItems(GraphPath ItemA, GraphPath ItemB)
        {
            int c = ItemA.Metric.CompareTo(ItemB.Metric);
            return c;
        }

    }
}
