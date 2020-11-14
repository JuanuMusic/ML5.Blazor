"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FaceApiInterop = void 0;
var FaceApiInterop = /** @class */ (function () {
    function FaceApiInterop() {
        /**
         * Last instance id created.
         * Used to keep track of each new FaceApi instance.
         */
        this._lastInstanceId = 0;
        /**
         * Stores the FaceApi instances on the current session
         */
        this._faceApiModels = {};
    }
    return FaceApiInterop;
}());
exports.FaceApiInterop = FaceApiInterop;
//# sourceMappingURL=faceApiInterop.js.map