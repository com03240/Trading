﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Trading
{
    public struct Tick : IMarketDataPoint
    {
        public bool IsValid { get { return Time != default(DateTime); } }
        
        public Tick(DateTime time, double bid = double.NaN, double ask = double.NaN) {
            this.Time = time;
            Bid = bid;
            Ask = ask;
        }

        public DateTime Time { get; set; }
        public double Bid;
        public double Ask;

        public bool HasBid { get { return !double.IsNaN(Bid); } }
        public bool HasAsk { get { return !double.IsNaN(Ask); } }

        public static Tick Invalid { get; set; }
        private static readonly Tick invalid = new Tick { Time = default(DateTime), Bid=double.NaN, Ask = double.NaN  };

        public override string ToString()
        {
            var bid = double.IsNaN(Bid) ? "" : " b:"+Bid.ToString();
            var ask = double.IsNaN(Ask) ? "" : " a:"+Ask.ToString();
            return $"{Time}{bid}{ask}";
        }
    }
}
