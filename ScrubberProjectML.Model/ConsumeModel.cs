using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ML;
using ScrubberProjectML.Model;

namespace ScrubberProjectML.Model
{
    public class ConsumeModel
    {
        public static ModelOutput Predict(ModelInput input)
        {
            MLContext mlContext = new MLContext();

            string modelPath = @"C:\Users\Andrii\AppData\Local\Temp\MLVSTools\ScrubberProjectML\ScrubberProjectML.Model\MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            ModelOutput result = predEngine.Predict(input);
            return result;
        }
    }
}
