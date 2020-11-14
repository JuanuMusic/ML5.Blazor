using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML5.Blazor.InteropUtils
{
    public abstract class BaseML5InteropObject
    {
        [JsonProperty("instance_id")]
        public int InstanceID { get; set; }

        IJSRuntime m_jsRuntime;
        /// <summary>
        /// The JSRuntime used for interop
        /// </summary>
        public IJSRuntime JSRuntime => m_jsRuntime;

        /// <summary>
        /// Set the JSRuntime. Throws an exception if already set.
        /// </summary>
        /// <param name="jsRuntime"></param>
        public void SetJSRuntime(IJSRuntime jsRuntime)
        {
            if (m_jsRuntime != null) throw new Exception("JSRuntime already set.");
            m_jsRuntime = jsRuntime;
        }

        #region Constructors
        public BaseML5InteropObject() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsRuntime"></param>
        public BaseML5InteropObject(IJSRuntime jsRuntime) => m_jsRuntime = jsRuntime; 
        #endregion

    }
}
