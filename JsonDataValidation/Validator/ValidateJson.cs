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
        public static string validMessage { get; set; } = "The Json is VALID";
        public static string invalidMessage { get; set; } = "The Json is INVALID";
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

                ArupComputeResult result = new ArupComputeResult();
                result.ArupComputeResultItems = new List<ArupComputeResultItem>();
                result.ArupComputeResultItems.Add(acResult);
                return result;
            }
            catch
            {
                ArupComputeResult result = new ArupComputeResult();
                result.Errors = new List<string>();
                result.Errors.Add("The input Json or Schema was in the incorrect format");
                return result;
            }
        }
    }
}
