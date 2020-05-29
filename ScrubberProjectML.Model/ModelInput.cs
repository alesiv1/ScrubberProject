using Microsoft.ML.Data;

namespace ScrubberProjectML.Model
{
    public class ModelInput
    {
        [ColumnName("Title"), LoadColumn(0)]
        public string Title { get; set; }


        [ColumnName("Author"), LoadColumn(1)]
        public string Author { get; set; }


        [ColumnName("Description"), LoadColumn(2)]
        public string Description { get; set; }


        [ColumnName("NumberOfViews"), LoadColumn(3)]
        public float NumberOfViews { get; set; }


        [ColumnName("Like"), LoadColumn(4)]
        public bool Like { get; set; }


    }
}
