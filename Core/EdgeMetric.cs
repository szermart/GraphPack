using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GraphPack
{
    public abstract class EdgeMetric
    {

        //****************************************************************************************************************************************************************
        //Properties
        //****************************************************************************************************************************************************************
        internal string IndexKey { get; private set; }        
        internal protected double this[int Index]
        {
            get
            {
                return GetMetric(Index);
            }
        }        


        //****************************************************************************************************************************************************************
        //Construction and initialization
        //****************************************************************************************************************************************************************        

        //
        //Constructor
        //
        public EdgeMetric(double[] Metrics) 
        {
            this.Metrics = CopyMetrics(Metrics);
            InitializeComponent();
        }

        //
        //Common initializer
        //
        private void InitializeComponent()
        {
            this.IndexKey = BuildKey();
        }


        //****************************************************************************************************************************************************************
        //Operating functions
        //****************************************************************************************************************************************************************

        //
        //To array
        //
        internal double[] ToArray()
        {
            double[] d = CopyMetrics(Metrics);
            return d;
        }


        //****************************************************************************************************************************************************************
        //Support functions
        //****************************************************************************************************************************************************************        

        //
        //Build key
        //
        private string BuildKey()
        {
            string Key = "";
            foreach (double d in Metrics)
                Key = Key + "-" + d.ToString();
            return Key;
        }

        //
        //Get metric
        //
        private double GetMetric(int Index)
        {
            int i = (Index < 0) || (Index >= Metrics.Length) ? -1 : Index;
            if (i == -1)
            {
                //TODO--> Build exception
            }
            return Metrics[i];
        }

        //
        //Copy metrics
        //
        private double[] CopyMetrics(double[] Metrics)
        {
            double[] M = new double[Metrics.Length];
            Metrics.CopyTo(M, 0);
            return Metrics;
        }

        //
        //Implement ToString
        //
        public override string ToString()
        {
            string s = "";
            foreach (double d in Metrics)
                s = s + " ; " + d.ToString();
            s = s.Substring(3);
            s = "[" + s + "]";
            return s;
        }


        //****************************************************************************************************************************************************************
        //Locals
        //****************************************************************************************************************************************************************
        private double[] Metrics;

    }
}
