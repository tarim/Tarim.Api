using System;
using System.Collections.Generic;
using System.Data;
using Tarim.Api.Infrastructure.Common.Enums;

namespace Tarim.Api.Infrastructure.Common
{
    public class Results<T>
    {
        public T Object { get; set; }
        public string Message { get; set; }
        public int Value { get; set; }
        public IDbDataParameter OMessage { get; set; }
        public IDbDataParameter OValue { get; set; }
        public IDbDataParameter OStatus { get; set; }
        public ExecuteStatus Status { get; set; } = ExecuteStatus.Failed;


        public int Count { get; set; }
    }
    public class Result
    {
        public string Message { get; set; }
        public int Value { get; set; }
        public ExecuteStatus Status { get; set; } = ExecuteStatus.Failed;
    }
    public class Result<T>
    {
        public T Object { get; set; }
        public string Message { get; set; }
        public  int TotalCount { get; set; }
        public IList<string> Messages { get; set; }
        public int Value { get; set; }
        public ExecuteStatus Status { get; set; } = ExecuteStatus.Failed;
        public IList<ExecuteStatus> Statuses { get; set; }
    }
    public static class ResultsExtension
    {
        public static void Result<T>(this Results<T> results)
        {
            if (results.OValue != null)
            {
                if (results.OValue.Value != DBNull.Value ||
                    !string.Equals(results.OValue.Value.ToString(), "null", StringComparison.OrdinalIgnoreCase))
                    results.Value = Convert.ToInt32(results.OValue.Value.ToString());
                if (results.Value > 0 || results.Count > 0) { results.Status = ExecuteStatus.Success; return; }
            }
            if (results.OMessage == null) return;
            if (results.OMessage.Value != DBNull.Value ||
                !string.Equals(results.OMessage.Value.ToString(), "null", StringComparison.OrdinalIgnoreCase))
                results.Message = Convert.ToString(results.OMessage.Value);
            if (results.Message.Equals("SUCCESS") || results.Count > 0) results.Status = ExecuteStatus.Success;
        }
    }
}
