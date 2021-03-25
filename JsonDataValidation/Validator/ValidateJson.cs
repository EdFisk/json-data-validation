using System.Collections.Generic;
using Arup.Compute.DotNetSdk;
using Arup.Compute.DotNetSdk.Enums;
using Arup.Compute.DotNetSdk.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using JsonDataValidation.Helpers;

namespace JsonDataValidation.Validator
{
    public static class ValidateJson
    {

        [Calculation("Json validator", "Validate a json input against a json schema", "ed.fisk@arup.com", LevelOfReview.Complete)]
        public static ArupComputeResult ValidateJsonInput(
            [Input("Json","A json input as a string","","")]
            string input_json,
            [Input("Schema", "A json schema input as a string", "", "")]
            string input_schema
        )
        {
            // Is the input format correct
            try
            {
                // Convert schema string to LINQ schema
                JSchema schema = JSchema.Parse(input_schema);

                // Creates a JSON object
                JObject json = JObject.Parse(input_json);

                // Validates the object against the schema
                IList<ValidationError> errorMessages;

                // Validate the json
                bool isValid = json.IsValid(schema, out errorMessages);
                string isValidStr = (isValid == true) ? "The json input was VALID against the schema." : "The json input was INVALID against the schema.";

                // Add items to the report
                Report.AddTitle("Json validation", Stylings.NormalBig);
                Report.AddText(isValidStr, Stylings.NormalMedium);

                List<string> errorsList = new List<string> { };
                // Add errors to the report
                if (isValid == false)
                {
                    List<string> errorMessagesStr = new List<string> { };
                    foreach (ValidationError error in errorMessages)
                    {
                        string errorString = $"{error.Message} Line {error.LineNumber}. Position {error.LinePosition}.";
                        errorMessagesStr.Add(errorString);
                        errorsList.Add(errorString);
                    }
                    Report.AddText("Errors:", Stylings.ErrorMedium);
                    Report.AddList(errorMessagesStr, Stylings.ErrorSmall);
                }

                // Create result
                ArupComputeResult result = Result.AddResult(isValid, errorsList);
                return result;
            }
            // Is the input format incorrect
            catch
            {
                List<string> errorsList = new List<string> { };
                errorsList.Add("The input json or schema was not able to be loaded.");
                Report.AddTitle("Json validation", Stylings.NormalBig);
                Report.AddText("Errors:", Stylings.ErrorMedium);
                Report.AddList(errorsList, Stylings.ErrorSmall);

                ArupComputeResult result = Result.AddResult(false, errorsList);

                return result;
            }
        }
    }
}
