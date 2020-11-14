using ML5.Blazor.NeuralNetworks;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using ML5.Blazor.InteropUtils;
using ML5.Blazor.FaceApi;

namespace ML5.Blazor
{
    public class ML5Core : BaseML5InteropObject
    {
        internal const string INTEROP_GLOBAL_VARIABLE = "ml5Interop";

        private ML5Core() : base() { }
        public ML5Core(IJSRuntime jsRuntime) : base(jsRuntime) { }
        /// <summary>
        /// Returns the ML5.js version.
        /// </summary>
        /// <returns></returns>
        public ValueTask<string> Version() => JSRuntime.InvokeAsync<string>($"{INTEROP_GLOBAL_VARIABLE}.version");

        #region Neural Networks
        /// <summary>
        /// Gets a new instance of <see cref="NeuralNetwork"/>.
        /// </summary>
        /// <typeparam name="TInputDataModel"></typeparam>
        /// <typeparam name="TOutputDataModel"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<NeuralNetwork<TInputDataModel, TOutputDataModel>> BuildNeuralNetwork<TInputDataModel, TOutputDataModel>(NeuralNetworkOptions options)
        {
            NeuralNetwork<TInputDataModel, TOutputDataModel> retVal = null;

            // Classification NN
            if (options.Task == NeuralNetworkOptions.TrainingTask.Classification)
                retVal = await JSRuntime.InvokeAsync<ClassificationNeuralNetwork<TInputDataModel, TOutputDataModel>>($"{INTEROP_GLOBAL_VARIABLE}.neuralNetwork.buildNeuralNetwork", options);
            // Regression NN
            else if (options.Task == NeuralNetworkOptions.TrainingTask.Regression)
                retVal = await JSRuntime.InvokeAsync<RegressionNeuralNetwork<TInputDataModel, TOutputDataModel>>($"{INTEROP_GLOBAL_VARIABLE}.neuralNetwork.buildNeuralNetwork", options);

            // Set JS runtime.
            retVal.SetJSRuntime(this.JSRuntime);

            return retVal;
        }

        /// <summary>
        /// Gets a new instance of <see cref="ClassificationNeuralNetwork{TInputDataModel, TOutputDataModel}"/>.
        /// </summary>
        /// <typeparam name="TInputDataModel"></typeparam>
        /// <typeparam name="TOutputDataModel"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<ClassificationNeuralNetwork<TInputDataModel, TOutputDataModel>> BuildClassificationNeuralNetwork<TInputDataModel, TOutputDataModel>(NeuralNetworkOptions options)
        {
            // Force options task to be classification
            options.Task = NeuralNetworkOptions.TrainingTask.Classification;
            return await BuildNeuralNetwork<TInputDataModel, TOutputDataModel>(options) as ClassificationNeuralNetwork<TInputDataModel, TOutputDataModel>;
        }

        /// <summary>
        /// Gets a new instance of <see cref="RegressionNeuralNetwork{TInputDataModel, TOutputDataModel}{TInputDataModel, TOutputDataModel}"/>.
        /// </summary>
        /// <typeparam name="TInputDataModel"></typeparam>
        /// <typeparam name="TOutputDataModel"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<RegressionNeuralNetwork<TInputDataModel, TOutputDataModel>> BuildRegressionNeuralNetwork<TInputDataModel, TOutputDataModel>(NeuralNetworkOptions options)
        {
            // Force options task to be classification
            options.Task = NeuralNetworkOptions.TrainingTask.Regression;
            return await BuildNeuralNetwork<TInputDataModel, TOutputDataModel>(options) as RegressionNeuralNetwork<TInputDataModel, TOutputDataModel>;
        }
        #endregion

        #region FaceApi
        TaskCompletionSource<FaceApi.FaceApi> m_buildFaceApiTask;
        /// <summary>
        /// Builds and returns a <see cref="FaceApi.FaceApi"/> model.
        /// </summary>
        /// <param name="detectionOptions"></param>
        /// <returns></returns>
        public async ValueTask<FaceApi.FaceApi> BuildFaceApi(DetectionOptions detectionOptions)
        {
            var builder = new FaceApiBuilder(JSRuntime);
            return await builder.Build(detectionOptions);
        }
        #endregion
    }
}
