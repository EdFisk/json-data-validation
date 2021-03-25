using System.Collections.Generic;
using Arup.Compute.DotNetSdk;

namespace JsonDataValidation.Helpers
{
    public class Result
    {
        public static ArupComputeResult AddResult(bool isValid, List<string> errors)
        {
            ArupComputeResultItem resultItem = new ArupComputeResultItem();
            resultItem.Value = isValid;
            resultItem.Description = (errors.Count != 0) ? "Valid" : "Error! Valid";
            resultItem.Symbol = "n/a";

            ArupComputeResult result = new ArupComputeResult();
            result.Errors = new List<string>();
            foreach (string error in errors)
            {
                result.Errors.Add(error);
            }
            result.ArupComputeResultItems = new List<ArupComputeResultItem>();
            result.ArupComputeResultItems.Add(resultItem);
            result.ArupComputeReport_HTML = Report.reportStr;
            return result;
        }
    }
}