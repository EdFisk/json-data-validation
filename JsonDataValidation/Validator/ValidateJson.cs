using System;
using System.Collections.Generic;
using Arup.Compute.DotNetSdk;
using Arup.Compute.DotNetSdk.Enums;
using Arup.Compute.DotNetSdk.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JsonDataValidation.Validator
{
    public class resultsMessage
    {
        public static string validMessage { get; set; } = "The json input is valid";
        public static string invalidMessage { get; set; } = "The json input is invalid";
    }
    public static class ValidateJson
    {

        [Calculation("Json validator", "Validate a json input against a json schema", "ed.fisk@arup.com", LevelOfReview.Complete)]
        public static ArupComputeResult ValidateJsonInput(
            [Input("Json","A json input as a string","","")]
            string input_json,
            [Input("JsonSchema", "A json schema input as a string", "", "")]
            string input_schema
        )
        {
            try
            {
                // Convert schema string to LINQ schema
                JSchema schema = JSchema.Parse(input_schema);

                // Creates a JSON object
                JObject json = JObject.Parse(input_json);

                // Validates the object against the schema
                IList<ValidationError> errorMessages;

                bool isValid = json.IsValid(schema, out errorMessages);

                string isValidStr = (isValid == true) ? resultsMessage.validMessage : resultsMessage.invalidMessage;

                ArupComputeResultItem acResult = new ArupComputeResultItem();
                acResult.Value = isValidStr;
                acResult.Description = "";
                acResult.Symbol = "";

                ArupComputeResult result = new ArupComputeResult();
                result.ArupComputeResultItems = new List<ArupComputeResultItem>();
                result.ArupComputeResultItems.Add(acResult);

                result.ArupComputeReport_HTML = @"<h1>Json Data Validator Results</h1>
                                                <h2>" + isValidStr + "!</h2>";

                if (isValid == false)
                {
                    foreach (ValidationError message in errorMessages)
                    {
                        result.ArupComputeReport_HTML += $"<h3>Error: {message.Message} Line {message.LineNumber}. Position {message.LinePosition}.</h3>";
                    }
                }

                return result;
            }
            catch
            {
                ArupComputeResult result = new ArupComputeResult();
                result.Errors = new List<string>();
                result.Errors.Add("The input json or schema was in the incorrect format");
                return result;
            }
        }
    }
}
