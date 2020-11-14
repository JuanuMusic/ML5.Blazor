"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ML5Container = void 0;
var neuralNetworks_1 = require("./neuralNetworks");
/**
 * ML5 objects container.
 * Stores the ML5 objects instances.
 */
var ML5Container = /** @class */ (function () {
    function ML5Container() {
        /**
         * Contains the instances to the ML5 neural networks.
         */
        this.neuralNetworks = new neuralNetworks_1.NeuralNetworkContainer();
    }
    return ML5Container;
}());
exports.ML5Container = ML5Container;
//# sourceMappingURL=ml5Container.js.map