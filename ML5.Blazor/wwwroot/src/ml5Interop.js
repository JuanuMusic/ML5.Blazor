"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ml5Container_1 = require("./ml5Container");
var container = new ml5Container_1.ML5Container();
;
;
window['ml5Interop'] = {
    version: function () {
        return ml5.version;
    },
    neuralNetwork: {
        buildNeuralNetwork: function (options) {
            var neuralNetwork = ml5.neuralNetwork(options);
            return container.neuralNetworks.add(neuralNetwork);
        },
        addData: function (instanceId, input, output) {
            console.log("Adding", input, output);
            container.neuralNetworks.get(instanceId).addData(input, output);
        },
        normalizeData: function (instanceId) {
            console.log("Normalizing");
            try {
                container.neuralNetworks.get(instanceId).normalizeData();
                console.log("Normalized");
            }
            catch (e) {
                console.error(e);
            }
        },
        train: function (instanceId, trainingOptions, dotNetHelper) {
            console.log("Training started...", trainingOptions);
            container.neuralNetworks.get(instanceId).train(trainingOptions, function () {
                console.log("Training finished");
                dotNetHelper.invokeMethod("OnFinishedTraining");
            });
        },
        classify: function (instanceId, input, dotNetHelper) {
            console.log("Input", input);
            container.neuralNetworks.get(instanceId).classify(input, function (error, result) {
                console.log("error", error);
                console.log("result", result);
                dotNetHelper.invokeMethod("OnHandleResults", error, result);
            });
        }
    }
};
//# sourceMappingURL=ml5Interop.js.map