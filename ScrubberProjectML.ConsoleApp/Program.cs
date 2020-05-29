using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using ScrubberProjectML.Model;

namespace ScrubberProjectML.ConsoleApp
{
    class Program
    {
        private const string DATA_FILEPATH = @"D:\test-posts.csv";

        static void Main(string[] args)
        {
            List<ModelInput> sampleData = CreateSingleDataSample(DATA_FILEPATH);

            foreach(var data in sampleData)
			{
                var predictionResult = ConsumeModel.Predict(data);

                Console.WriteLine("================================================================================================");
                Console.WriteLine($"Title: {data.Title}");
                //Console.WriteLine($"Author: {data.Author}");
                //Console.WriteLine($"Description: {data.Description}");
                //Console.WriteLine($"NumberOfViews: {data.NumberOfViews}");
                Console.WriteLine($"Actual Like: {data.Like} \nPredicted Like: {predictionResult.Prediction}");
            }
        }

        #region CreateSingleDataSample
        private static List<ModelInput> CreateSingleDataSample(string dataFilePath)
        {
            MLContext mlContext = new MLContext();

            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ';',
                                            allowQuoting: true,
                                            allowSparse: false);

            List<ModelInput> sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false).ToList();
            return sampleForPrediction;
        }
        #endregion
    }
}
