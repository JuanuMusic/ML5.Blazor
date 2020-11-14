import { NeuralNetworkInterop } from "./neuralNetworkInterop";


export class ML5Interop {
    
    version() {
        return ml5.version;
    }

    neuralNetwork = new NeuralNetworkInterop(); 
}

