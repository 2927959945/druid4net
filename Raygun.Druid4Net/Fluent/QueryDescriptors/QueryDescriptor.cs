﻿using System;
using System.Collections.Generic;

namespace Raygun.Druid4Net
{
  public abstract class QueryDescriptor<TRequest> : IQueryDescriptor where TRequest : QueryRequestData
  {
    internal List<string> IntervalsValue;

    internal string DataSourceValue;

    internal object GranularityValue = "all";

    internal IFilterSpec FilterValue;

    protected QueryDescriptor()
    {
      IntervalsValue = new List<string>();
    }

    public IQueryDescriptor Interval(DateTime from, DateTime to)
    {
      AddInterval(new Interval(from, to));      

      return this;
    }

    public IQueryDescriptor Intervals(params Interval[] intervals)
    {
      foreach (var interval in intervals)
      {
        AddInterval(interval);
      }

      return this;
    }

    public IQueryDescriptor Intervals(IEnumerable<Interval> intervals)
    {
      foreach (var interval in intervals)
      {
        AddInterval(interval);
      }

      return this;
    }

    private void AddInterval(Interval interval)
    {
      IntervalsValue.Add(interval.ToInterval());
    }

    public IQueryDescriptor DataSource(string dataSource)
    {
      DataSourceValue = dataSource;

      return this;
    }

    public IQueryDescriptor Granularity(Granularities granularity)
    {
      switch (granularity)
      {
        case Granularities.All:
          GranularityValue = "all";
          break;
        case Granularities.None:
          GranularityValue = "none";
          break;
        case Granularities.Second:
          GranularityValue = "second";
          break;
        case Granularities.Minute:
          GranularityValue = "minute";
          break;
        case Granularities.FifteenMinute:
          GranularityValue = "fifteen_minute";
          break;
        case Granularities.ThirtyMinute:
          GranularityValue = "thirty_minute";
          break;
        case Granularities.Hour:
          GranularityValue = "hour";
          break;
        case Granularities.Day:
          GranularityValue = "day";
          break;
        case Granularities.Week:
          GranularityValue = "week";
          break;
        case Granularities.Month:
          GranularityValue = "month";
          break;
        case Granularities.Quarter:
          GranularityValue = "quarter";
          break;
        case Granularities.Year:
          GranularityValue = "year";
          break;
      }

      return this;
    }

    public IQueryDescriptor Granularity(IGranularitySpec granularitySpec)
    {
      GranularityValue = granularitySpec;

      return this;
    }


    public IQueryDescriptor Filter(IFilterSpec filterSpec)
    {
      FilterValue = filterSpec;

      return this;
    }

    protected void SetCommonContextProperties(ContextSpec context, int? timeout, long? maxScatterGatherBytes, int? priority, string queryId, bool? useCache, bool? populateCache, bool? bySegment, bool? finalize, string chunkPeriod, bool? serializeDateTimeAsLong, bool? serializeDateTimeAsLongInner)
    {
      context.Timeout = timeout;
      context.MaxScatterGatherBytes = maxScatterGatherBytes;
      context.Priority = priority;
      context.QueryId = queryId;
      context.UseCache = useCache;
      context.PopulateCache = populateCache;
      context.BySegment = bySegment;
      context.Finalize = finalize;
      context.ChunkPeriod = chunkPeriod;
      context.SerializeDateTimeAsLong = serializeDateTimeAsLong;
      context.SerializeDateTimeAsLongInner = serializeDateTimeAsLongInner;
    }

    internal abstract IDruidRequest<TRequest> Generate();
  }
}