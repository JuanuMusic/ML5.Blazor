import { FaceApiInterop } from "./faceApiInterop";
import { NeuralNetworkInterop } from "./neuralNetworkInterop";


export class ML5Interop {
    
    version() {
        return ml5.version;
    }

    /**
     * Neural networks Interop helper
     */ 
    neuralNetwork = new NeuralNetworkInterop(); 

    /**
     * Face API interop helper
     */
    faceApi = new FaceApiInterop();
}

