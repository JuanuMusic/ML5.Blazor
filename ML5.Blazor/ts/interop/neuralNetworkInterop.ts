import { INeuralNetworkInstance } from "..";

interface INeuralNetworkOptions { task: number | string, debug: boolean };
interface ITrainingOptions { epochs: number, batchSize: number };

export class NeuralNetworkInterop {

    /**
     * Contains the last instance id created.
     * This ID is used to keep track of each NN instance.
     */
    _lastInstanceId: number = 0;

    /**
     * Stores the Neural network instances loaded on this session.
     */
    _neuralNetworks: { [id: number]: INeuralNetworkInstance } = {};

    /**
     * Builds and returns a new neural network instance.
     * The instance is assigned a new instanceId and stored on a Neuralnetworks dictionary.
     * @param options Neural Network options to build the NN
     */
    public buildNeuralNetwork(options: INeuralNetworkOptions): INeuralNetworkInstance {

        // Task can be 'regression' or 'classification'. It can be assigned from C# as an enum (number) or from JS (string).
        // When coming from C# we need to convert number (enum) to string.
        options.task = typeof options.task === "string" ? options.task : (options.task == 0 ? 'regression' : 'classification');

        // instantiate the new nn
        const neuralNetwork = ml5.neuralNetwork(options);

        // assign the instance id
        neuralNetwork.instanceId = this._lastInstanceId;
        // increment instance id for next instance
        this._lastInstanceId++;
        // add new instance to the dictionary of NNs
        this._neuralNetworks[neuralNetwork.instanceId] = neuralNetwork;
        // return the instance 
        return neuralNetwork;
    }

    /**
     * Returns a specific neural Network instance by its ID.
     * @param instanceId
     */
    public get(instanceId: number): INeuralNetworkInstance {
        return this._neuralNetworks[instanceId];
    }

    /**
     * Adds data to a specific NN instance.
     * @param instanceId
     * @param input
     * @param output
     */
    public addData(instanceId: number, input, output) {
        this.get(instanceId).addData(input, output);
    }

    /**
     * Normalizes the data on a specific NN instance.
     * @param instanceId
     */
    public normalizeData(instanceId: number) {
        try {
            this.get(instanceId).normalizeData();
        }
        catch (e) {
            console.error("Error while normalizing the data");
            console.error(e);
        }
    }

    /**
     * Runs train() on a specific NN Instance
     * 
     * @param instanceId
     * @param trainingOptions
     * @param dotNetHelper reference to the dotnethelper to invoke callback method _onFinishedTraining
     * @param callbackName name of the function to invoke on the dotNetHelper once training is completed.
     */
    public train(instanceId: number, trainingOptions: ITrainingOptions, dotNetHelper, callbackName: string = "onTrainingCompleted") {
        console.log("Training started...", trainingOptions);
        this.get(instanceId).train(trainingOptions, () => {
            console.log("Training finished");
            dotNetHelper.invokeMethod(callbackName);
        });
    }

    /**
     * Runs classify() on a specific NN instance and 
     * @param instanceId
     * @param input
     * @param dotNetHelper reference to the dotnethelper to invoke callback method _onHandleResults
     * @param callbackName name of the function to invoke on the dotNetHelper once classification is completed. Default is "onHandleResults"
     */
    public classify(instanceId: number, input, dotNetHelper, callbackName = "onClassificationCompleted") {
        console.log("Input", input);
        this.get(instanceId).classify(input, (error, result) => {
            dotNetHelper.invokeMethod(callbackName, error, result);
        });
    }

}