using ML5.Blazor.InteropUtils;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5.Blazor.NeuralNetworks
{

    public class ClassificationResult
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }

    public interface INeuralNetwork<TInputDataModel, TOutputDataModel>
    {
        /// <summary>
        /// Train the data using the options.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task TrainAsync(TrainingOptions options);

        /// <summary>
        /// Normalizes the data.
        /// </summary>
        /// <returns></returns>
        Task NormalizeDataAsync();

        /// <summary>
        /// Adds data to the NeuralNetwork data array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        Task AddData(TInputDataModel input, TOutputDataModel output);
    }

    public class NeuralNetwork<TInputDataModel, TOutputDataModel> : BaseML5InteropObject, INeuralNetwork<TInputDataModel, TOutputDataModel>
    {
        /// <summary>
        /// Reference to the instance if this object to be passed to JS interop
        /// </summary>
        protected DotNetObjectReference<NeuralNetwork<TInputDataModel, TOutputDataModel>> dotNetReference;

        public event EventHandler<EventArgs> TrainingCompleted;

        public NeuralNetwork() : base() => dotNetReference = DotNetObjectReference.Create(this);
        public NeuralNetwork(JSRuntime jSRuntime) : base(jSRuntime) => dotNetReference = DotNetObjectReference.Create(this);

        /// <summary>
        /// For JS ONLY!
        /// Callback called by JS when training completes.
        /// </summary>
        [JSInvokable]
        public void _onFinishedTraining() => TrainingCompleted?.Invoke(this, new EventArgs());

        List<(TInputDataModel, TOutputDataModel)> m_data = new List<(TInputDataModel, TOutputDataModel)>();

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="outputData"></param>
        public async Task AddData(TInputDataModel inputData, TOutputDataModel outputData)
        {
            await JSRuntime.InvokeVoidAsync($"{ML5Core.INTEROP_GLOBAL_VARIABLE}.neuralNetwork.addData", InstanceID, inputData, outputData);
            m_data.Add((inputData, outputData));
        }

        /// <summary>
        /// Train and pass the object reference of this instance for callbacks
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task TrainAsync(TrainingOptions options) => await JSRuntime.InvokeVoidAsync($"{ML5Core.INTEROP_GLOBAL_VARIABLE}.neuralNetwork.train", InstanceID, options, dotNetReference, nameof(_onFinishedTraining));

        /// <summary>
        /// Useful to keep track of instances
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"NeuralNetwork.InstanceID:{InstanceID}";

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async Task NormalizeDataAsync() => await JSRuntime.InvokeVoidAsync($"{ML5Core.INTEROP_GLOBAL_VARIABLE}.neuralNetwork.normalizeData", InstanceID);
    }
}
