﻿namespace CostEffectiveCode.Akka.Messages
{
    public class FetchRequestMessageBase
    {
        public FetchRequestMessageBase(bool single)
        {
            Single = single;
            Page = null;
            Limit = null;
        }

        public FetchRequestMessageBase(int page, int limit)
        {
            Single = false;
            Page = page;
            Limit = limit;
        }

        public FetchRequestMessageBase(int limit)
        {
            Single = false;
            Page = null;
            Limit = limit;
        }

        public bool Single { get; set; }

        public int? Page { get; set; }

        public int? Limit { get; set; }

    }
}