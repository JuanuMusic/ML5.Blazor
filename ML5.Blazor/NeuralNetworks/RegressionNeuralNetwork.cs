using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5.Blazor.NeuralNetworks
{
    public interface IRegression<TInputDataModel>
    {
        /// <summary>
        /// Classify
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ClassificationResult> Predict(TInputDataModel input);
    }

    public class RegressionNeuralNetwork<TInputDataModel, TOutputDataModel> : NeuralNetwork<TInputDataModel, TOutputDataModel>, IRegression<TInputDataModel>
    {
        public Task<ClassificationResult> Predict(TInputDataModel input)
        {
            throw new NotImplementedException();
        }
    }
}
