interface IClassificationResult {
    label: string,
    confidence: number
}


/**
 * Defines a neural network interface
 */
export interface INeuralNetworkInstance {
    classify(input: any, callback: (error: any, result: IClassificationResult[]) => void);
    train(trainingOptions: any, callback: () => void);
    normalizeData();
    addData(input: any, output: any);
    instanceId?: number;
    isTrainer: boolean;
}