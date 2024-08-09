using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public class GraphDendogram : Dendogram<Graph>
    {        

        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        
        public GraphDendogram() {}


        //****************************************************************************************************************************************************************
        //Operating methods
        //****************************************************************************************************************************************************************

        //
        //Implement key builder
        //
        protected override string BuildItemKey(Graph Item)
        {
            string Key = Item.ID;
            return Key;
        }

    }
}
