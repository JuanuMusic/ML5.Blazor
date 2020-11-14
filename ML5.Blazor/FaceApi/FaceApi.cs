using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML5.Blazor.FaceApi
{
    public interface IFaceApi
    {
        Task<object> Detect(object image);
    }
    public class FaceApi : InteropUtils.BaseML5InteropObject, IFaceApi
    {
        TaskCompletionSource<object> m_detectTsc;
        DotNetObjectReference<FaceApi> m_objReference;
        
        #region Constructors
        public FaceApi() : base() => m_objReference = DotNetObjectReference.Create<FaceApi>(this);
        public FaceApi(JSRuntime jSRuntime) : base(jSRuntime) => m_objReference = DotNetObjectReference.Create<FaceApi>(this); 
        #endregion

        /// <summary>
        /// Detect faces on an image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Task<object> Detect(object image)
        {
            m_detectTsc = new TaskCompletionSource<object>();
            JSRuntime.InvokeVoidAsync($"{ML5Core.INTEROP_GLOBAL_VARIABLE}.faceApi.detect", InstanceID, image, m_objReference, nameof(_onDetectCompleted));
            return m_detectTsc.Task;
        }

        /// <summary>
        /// For JS Only.
        /// Callback for when faceApi completed detection.
        /// </summary>
        [JSInvokable]
        public void _onDetectCompleted(string error, object results)
        {
            if (!string.IsNullOrWhiteSpace(error))
                m_detectTsc.SetException(new Exception(error));
            else
                m_detectTsc.SetResult(results);
        }
    }
}
