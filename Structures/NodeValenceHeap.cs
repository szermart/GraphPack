using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDataStructures.Heap;

namespace GraphPack
{
    internal class NodeValenceHeap : HeapBase<GraphNode>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        internal NodeValenceHeap() : base(HeapTypes.Descending, true) {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement comparator
        //
        public override int CompareItems(GraphNode ItemA, GraphNode ItemB)
        {
            int c = ItemA.Valence.CompareTo(ItemB.Valence);
            return c;
        }

    }
}
