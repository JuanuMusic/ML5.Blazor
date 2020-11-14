"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ML5Interop = void 0;
var neuralNetworkInterop_1 = require("./neuralNetworkInterop");
var ML5Interop = /** @class */ (function () {
    function ML5Interop() {
        this.neuralNetwork = new neuralNetworkInterop_1.NeuralNetworkInterop();
    }
    ML5Interop.prototype.version = function () {
        return ml5.version;
    };
    return ML5Interop;
}());
exports.ML5Interop = ML5Interop;
//# sourceMappingURL=ml5Interop.js.map