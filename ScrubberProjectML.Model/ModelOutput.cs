using System;
using Microsoft.ML.Data;

namespace ScrubberProjectML.Model
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Score { get; set; }
    }
}
