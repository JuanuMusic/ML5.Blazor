using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML5.Blazor.FaceApi
{
    public class DetectionOptions
    {
        [JsonProperty("withlandmarks")]
        public bool WithLandmarks { get; set; }

        [JsonProperty("widthDescriptors")]
        public bool WithDescriptors { get; set; }
    }
}
