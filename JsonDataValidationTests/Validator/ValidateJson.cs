using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JsonDataValidation.Validator;
using Arup.Compute.DotNetSdk;
using System.IO;

namespace JsonDataValidationTests.Validator
{
    [TestClass]
    public class ValidateJsonTest
    {
        [TestMethod]
        public void TestMethod_AGSI_Valid_Both()
        {
            string schema_fp = @"../../../TestData/AGSI/AGSi_JSONSchema_v-0-5-0_Valid.json";
            string json_fp = @"../../../TestData/AGSI/DC2-test_in_agsi-sample_Valid.json";

            string json = File.ReadAllText(json_fp);
            string schema = File.ReadAllText(schema_fp);

            string Result_expected = "True";

            ArupComputeResult comparison = ValidateJson.ValidateJsonInput(json, schema);
            Assert.AreEqual(Result_expected, comparison.ArupComputeResultItems[0].Value);
        }
        [TestMethod]
        public void TestMethod_AGSI_Valid_Json()
        {
            string schema_fp = @"../../../TestData/AGSI/AGSi_JSONSchema_v-0-5-0_Fail.json";
            string json_fp = @"../../../TestData/AGSI/DC2-test_in_agsi-sample_Valid.json";

            string json = File.ReadAllText(json_fp);
            string schema = File.ReadAllText(schema_fp);

            string Result_expected = "False";

            ArupComputeResult comparison = ValidateJson.ValidateJsonInput(json, schema);
            Assert.AreEqual(Result_expected, comparison.ArupComputeResultItems[0].Value);
        }
        [TestMethod]
        public void TestMethod_AGSI_Invalid_Both()
        {
            string schema_fp = @"../../../TestData/AGSI/AGSi_JSONSchema_v-0-5-0_Fail.json";
            string json_fp = @"../../../TestData/AGSI/DC2-test_in_agsi-sample_Fail.json";

            string json = File.ReadAllText(json_fp);
            string schema = File.ReadAllText(schema_fp);

            string Result_expected = "False";

            ArupComputeResult comparison = ValidateJson.ValidateJsonInput(json, schema);
            Assert.AreEqual(Result_expected, comparison.ArupComputeResultItems[0].Value);
        }
        [TestMethod]
        public void TestMethod_AGSI_Invalid_Json()
        {
            string schema_fp = @"../../../TestData/AGSI/AGSi_JSONSchema_v-0-5-0_Valid.json";
            string json_fp = @"../../../TestData/AGSI/DC2-test_in_agsi-sample_Fail.json";

            string json = File.ReadAllText(json_fp);
            string schema = File.ReadAllText(schema_fp);

            string Result_expected = "False";

            ArupComputeResult comparison = ValidateJson.ValidateJsonInput(json, schema);
            Assert.AreEqual(Result_expected, comparison.ArupComputeResultItems[0].Value);
        }
        [TestMethod]
        public void TestMethod_AGSI_Invalid_Format()
        {
            string schema_fp = @"../../../TestData/AGSI/AGSi_JSONSchema_v-0-5-0_Valid.json";
            string json_fp = @"../../../TestData/AGSI/DC2-test_in_agsi-sample_Invalid_Format.json";

            string json = File.ReadAllText(json_fp);
            string schema = File.ReadAllText(schema_fp);

            double Errors_expected = 1;

            ArupComputeResult comparison = ValidateJson.ValidateJsonInput(json, schema);
            Assert.AreEqual(Errors_expected, comparison.Errors.Count);
        }
    }
}
