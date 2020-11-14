using Newtonsoft.Json;

namespace ML5.Blazor.NeuralNetworks
{
    public class TrainingOptions
    {
        [JsonProperty("epochs")]
        public int Epochs { get; set; }

        [JsonProperty("batchSize")]
        public int BatchSize { get; set; }
    }
}
