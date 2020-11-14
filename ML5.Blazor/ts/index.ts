import { ML5Interop } from "./interop/ml5Interop";



/**
 * Expose ML5INterop to the ml5Interop property on window.
 */
window['ml5Interop'] = new ML5Interop();


// Export all
export * from "./neuralNetworks";