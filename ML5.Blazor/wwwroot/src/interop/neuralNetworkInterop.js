"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.NeuralNetworkInterop = void 0;
;
;
var NeuralNetworkInterop = /** @class */ (function () {
    function NeuralNetworkInterop() {
        /**
         * Contains the last instance id created.
         * This ID is used to keep track of each NN instance.
         */
        this._lastInstanceId = 0;
        /**
         * Stores the Neural network instances loaded on this session.
         */
        this._neuralNetworks = {};
    }
    /**
     * Builds and returns a new neural network instance.
     * The instance is assigned a new instanceId and stored on a Neuralnetworks dictionary.
     * @param options Neural Network options to build the NN
     */
    NeuralNetworkInterop.prototype.buildNeuralNetwork = function (options) {
        // Task can be 'regression' or 'classification'. It can be assigned from C# as an enum (number) or from JS (string).
        // When coming from C# we need to convert number (enum) to string.
        options.task = typeof options.task === "string" ? options.task : (options.task == 0 ? 'regression' : 'classification');
        // instantiate the new nn
        var neuralNetwork = ml5.neuralNetwork(options);
        // assign the instance id
        neuralNetwork.instanceId = this._lastInstanceId;
        // increment instance id for next instance
        this._lastInstanceId++;
        // add new instance to the dictionary of NNs
        this._neuralNetworks[neuralNetwork.instanceId] = neuralNetwork;
        // return the instance 
        return neuralNetwork;
    };
    /**
     * Returns a specific neural Network instance by its ID.
     * @param instanceId
     */
    NeuralNetworkInterop.prototype.get = function (instanceId) {
        return this._neuralNetworks[instanceId];
    };
    /**
     * Adds data to a specific NN instance.
     * @param instanceId
     * @param input
     * @param output
     */
    NeuralNetworkInterop.prototype.addData = function (instanceId, input, output) {
        this.get(instanceId).addData(input, output);
    };
    /**
     * Normalizes the data on a specific NN instance.
     * @param instanceId
     */
    NeuralNetworkInterop.prototype.normalizeData = function (instanceId) {
        try {
            this.get(instanceId).normalizeData();
        }
        catch (e) {
            console.error("Error while normalizing the data");
            console.error(e);
        }
    };
    /**
     * Runs train() on a specific NN Instance
     *
     * @param instanceId
     * @param trainingOptions
     * @param dotNetHelper reference to the dotnethelper to invoke callback method _onFinishedTraining
     * @param callbackName name of the function to invoke on the dotNetHelper once training is completed.
     */
    NeuralNetworkInterop.prototype.train = function (instanceId, trainingOptions, dotNetHelper, callbackName) {
        if (callbackName === void 0) { callbackName = "onTrainingCompleted"; }
        console.log("Training started...", trainingOptions);
        this.get(instanceId).train(trainingOptions, function () {
            console.log("Training finished");
            dotNetHelper.invokeMethod(callbackName);
        });
    };
    /**
     * Runs classify() on a specific NN instance and
     * @param instanceId
     * @param input
     * @param dotNetHelper reference to the dotnethelper to invoke callback method _onHandleResults
     * @param callbackName name of the function to invoke on the dotNetHelper once classification is completed. Default is "onHandleResults"
     */
    NeuralNetworkInterop.prototype.classify = function (instanceId, input, dotNetHelper, callbackName) {
        if (callbackName === void 0) { callbackName = "onClassificationCompleted"; }
        console.log("Input", input);
        this.get(instanceId).classify(input, function (error, result) {
            dotNetHelper.invokeMethod(callbackName, error, result);
        });
    };
    return NeuralNetworkInterop;
}());
exports.NeuralNetworkInterop = NeuralNetworkInterop;
//# sourceMappingURL=neuralNetworkInterop.js.map