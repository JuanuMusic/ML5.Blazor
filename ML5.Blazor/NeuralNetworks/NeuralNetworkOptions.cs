using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ML5.Blazor.NeuralNetworks
{
    public class NeuralNetworkOptions
    {
        public enum TrainingTask
        {
            Regression = 0,
            Classification = 1
        }

        [JsonProperty("task")]
        public TrainingTask Task { get; set; }

        [JsonProperty("debug")]
        public bool Debug { get; set; }
    }
}
