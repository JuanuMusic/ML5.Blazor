using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5.Blazor.NeuralNetworks
{
    public interface IClassification<TInputDataModel>
    {
        /// <summary>
        /// Classify input data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Classify(TInputDataModel input);
    }


    public class ClassificationNeuralNetwork<TInputDataModel, TOutputDataModel> : NeuralNetwork<TInputDataModel, TOutputDataModel>, IClassification<TInputDataModel>
    {
        public event Action<object, ClassificationResult[]> ClassificationCompleted;

        /// <summary>
        /// For JS ONLY!
        /// Callback called by JS after classification completes.
        /// </summary>
        /// <param name="error">Error if any</param>
        /// <param name="result">The classification results</param>
        [JSInvokable]
        public void _onHandleResults(object error, ClassificationResult[] result) => ClassificationCompleted?.Invoke(error, result);

        /// <summary>
        /// Classify an <paramref name="input"/>. 
        /// Raises <see cref="ClassificationCompleted"/> event when results are ready.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Classify(TInputDataModel input) => await JSRuntime.InvokeVoidAsync($"{ML5Core.INTEROP_GLOBAL_VARIABLE}.neuralNetwork.classify", InstanceID, input, dotNetReference, nameof(_onHandleResults));

    }
}
