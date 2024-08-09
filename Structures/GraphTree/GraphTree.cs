using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public abstract class GraphTree<T>
    {

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        

        //
        //Constructor
        //
        public GraphTree()
        {
            InitializeComponent();
        }

        //
        //Common initializer
        //
        private void InitializeComponent()
        {
            this.ItemIndex = new GraphTreeItemIndex<T>(this.BuildItemKey);
            this.InternalGraph = new UnitGraph();
        }


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Insert item
        //
        public void AddNode(T Item)
        {
            ItemIndex.Insert(Item);
            InternalGraph.AddNode(BuildItemKey(Item));
        }

        //
        //Remove node
        //
        public void RemoveNode(T Item)
        {
            ItemIndex.Delete(Item);
            InternalGraph.RemoveNode(BuildItemKey(Item));
        }

        //
        //Add edge
        //
        public void AddEdge(T SourceItem, T SinkItem, bool IsDirected)
        {
            InternalGraph.AddEdge(BuildItemKey(SourceItem), BuildItemKey(SinkItem), IsDirected);
        }

        //
        //Remove edge
        //
        public void RemoveEdge(T SourceItem, T SinkItem, bool IsDirected)
        {
            InternalGraph.RemoveEdge(BuildItemKey(SourceItem), BuildItemKey(SinkItem), IsDirected);
        }

        //
        //Get node set
        //
        public T[] GetNodeSet()
        {
            T[] Items = ItemIndex.Scan();
            return Items;
        }

        //
        //Get topological node set
        //
        private T[] GetTopologicalNodeSet(T SourceItem)
        {
            List<T> Items = new List<T>();
            string ItemKey = BuildItemKey(SourceItem);
            InternalGraph.SetTopologicalOrder(ItemKey);
            foreach (GraphNode N in InternalGraph.NodeSet)
            {
                T Item = ItemIndex.Find(N.ID);
                Items.Add(Item);
            }
            return Items.ToArray();
        }

        //
        //Get item
        //
        public T GetItem(string ItemKey)
        {
            T Item = ItemIndex.Find(ItemKey);
            return Item;
        }

        //
        //Build item key
        //
        protected abstract string BuildItemKey(T Item);


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private UnitGraph InternalGraph;
        private GraphTreeItemIndex<T> ItemIndex;
        internal delegate string ItemKeyBuilder(T Item);

    }
}
