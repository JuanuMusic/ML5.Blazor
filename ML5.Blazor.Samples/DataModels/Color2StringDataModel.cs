using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML5.Blazor.Samples.DataModels
{
    public interface IColorDataModelInput
    {
        float R { get; set; }
        float G { get; set; }
        float B { get; set; }
    }

    public class ColorDataModelInput : IColorDataModelInput
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
    }

    public interface IColorDataModelOutput
    {
        string Color { get; set; }
    }

    public class ColorDataModelOutput : IColorDataModelOutput
    {
        public string Color { get; set; }
    }

    public class Color2StringDataModel : IColorDataModelInput, IColorDataModelOutput
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public string Color { get; set; }

        public IColorDataModelInput Input => new ColorDataModelInput { R = this.R, G = this.G, B = this.B };
        public IColorDataModelOutput Output => new ColorDataModelOutput { Color = this.Color };
    }
}
