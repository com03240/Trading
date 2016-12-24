﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionFire.Trading
{
    //public class TimeFrameBar : TimedBar
    //{
    //    public string TimeFrame { get; set; }
    //}

    public enum DataStatus
    {
        Invalid = 0,
        Valid = 1,
        ValidTimeNoData = 2,
        NotLoaded = 3,
    }

    public struct TimedBar : ITimedBar
    {

        public bool IsValid { get { return DataStatus == DataStatus.Valid; } }

        public DataStatus DataStatus
        {
            get
            {
                if (OpenTime == default(DateTime))
                { return DataStatus.Invalid; }
                else if (OpenTime == DateTime.MaxValue)
                { return DataStatus.NotLoaded; }
                else
                {
                    if (double.IsNaN(Open) ||
                        double.IsNaN(High) ||
                        double.IsNaN(Low) ||
                        double.IsNaN(Close)
                        )
                    {
                        return DataStatus.ValidTimeNoData;
                    }
                    else
                    {
                        return DataStatus.Valid;
                    }
                }
            }

        }

        DateTime IMarketDataPoint.Time
        {
            get { return OpenTime; }
        }

        public DateTime OpenTime { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }


        #region Construction

        public static TimedBar New
        {
            get
            {
                return new TimedBar()
                {
                    OpenTime = default(DateTime),
                    High = double.NaN,
                    Low = double.NaN,
                    Open = double.NaN,
                    Close = double.NaN,
                    Volume = double.NaN,
                };
            }
        }

        public static TimedBar Invalid { get; private set; }

        static TimedBar()
        {
            Invalid = New;
        }


        //private TimedBar() { }
        //public TimedBar() {
        //    OpenTime = default(DateTime);
        //    High = double.NaN;
        //    Low = double.NaN;
        //    Open = double.NaN;
        //    Close = double.NaN;
        //    Volume = double.NaN;
        //}

        public TimedBar(DateTime date, double open, double high, double low, double close, double volume)
        {
            this.OpenTime = date;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
            this.Volume = volume;
        }

        public TimedBar(SymbolBar b)
        {
            this.OpenTime = b.Time;
            this.High = b.Bar.High;
            this.Low = b.Bar.Low;
            this.Open = b.Bar.Open;
            this.Close = b.Bar.Close;
            this.Volume = b.Bar.Volume;
        }
        public TimedBar(ITimedBar b)
        {
            this.OpenTime = b.OpenTime;
            this.High = b.High;
            this.Low = b.Low;
            this.Open = b.Open;
            this.Close = b.Close;
            this.Volume = b.Volume;
        }
        public static implicit operator TimedBar(TimedBarStruct b)
        {
            return new TimedBar(b);
        }

        #endregion

        #region Clone/Merge Methods

        public TimedBar Merge(TimedBar next)
        {

            var current = this;
            if (next.OpenTime != null)
            {
                High = Math.Max(current.High, next.High);
                Low = Math.Min(current.Low, next.Low);
                Close = next.Close;
                Volume = current.Volume + next.Volume;
            }

            return this;

            //return new TimedBar
            //{
            //    OpenTime = current.OpenTime,
            //    Open = current.Open,
            //    Close = next.Close,
            //    High = Math.Max(current.High, next.High),
            //    Low = Math.Min(current.Low, next.Low),
            //    Volume = current.Volume + next.Volume
            //};
        }

        public TimedBar Clone()
        {
            return new TimedBar
            {
                OpenTime = this.OpenTime,
                Open = this.Open,
                High = this.High,
                Low = this.Low,
                Close = this.Close,
                Volume = this.Volume,
            };
        }

        #endregion

        #region Misc

        public override string ToString()
        {
            var date = OpenTime.ToDefaultString();

            var chars = 8;
            var padChar = ' ';
            //var padChar = '0';
            var vol = Volume > 0 ? $" [v:{Volume.ToString().PadLeft(chars)}]" : "";
            return $"{date} o:{Open.ToString().PadRight(chars, padChar)} h:{High.ToString().PadRight(chars, padChar)} l:{Low.ToString().PadRight(chars, padChar)} c:{Close.ToString().PadRight(chars, padChar)}{vol}";

        }

        #endregion

    }
}
