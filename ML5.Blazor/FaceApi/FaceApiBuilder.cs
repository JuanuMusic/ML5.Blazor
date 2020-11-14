using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5.Blazor.FaceApi
{
    /// <summary>
    /// this class is in charge of building the API builder.
    /// Manages the asyncronous architecture of the build.
    /// </summary>
    public class FaceApiBuilder : InteropUtils.BaseML5InteropObject
    {
        TaskCompletionSource<FaceApi> m_tsc;
        
        /// <summary>
        /// FaceApi instance to return on build completion.
        /// </summary>
        FaceApi m_instance;

        /// <summary>
        /// Reference to the instance if this object to be passed to JS interop
        /// </summary>
        protected DotNetObjectReference<FaceApiBuilder> dotNetReference;

        private FaceApiBuilder() : base() => dotNetReference = DotNetObjectReference.Create<FaceApiBuilder>(this);
        public FaceApiBuilder(IJSRuntime jsRuntime) : base(jsRuntime) => dotNetReference = DotNetObjectReference.Create<FaceApiBuilder>(this);
        
        /// <summary>
        /// Builds the FaceApi with the specified options.
        /// </summary>
        /// <param name="detectionOptions"></param>
        /// <returns></returns>
        public async ValueTask<FaceApi> Build(DetectionOptions detectionOptions)
        {
            m_tsc = new TaskCompletionSource<FaceApi>();
            await JSRuntime.InvokeVoidAsync($"{ML5Core.INTEROP_GLOBAL_VARIABLE}.buildFaceApi", detectionOptions, dotNetReference, nameof(_onFaceApiModelLoaded));
            return await m_tsc.Task;
        }

        /// <summary>
        /// For JS only.
        /// Callback for when FaceApi model is loaded
        /// </summary>
        /// <param name="instance"></param>
        [JSInvokable]
        public void _onFaceApiModelLoaded(FaceApi instance)
        {
            // Make sure to pass the JSRuntime reference.
            instance.SetJSRuntime(this.JSRuntime);
            // Set the result.
            m_tsc.SetResult(instance);
        }
    }
}
