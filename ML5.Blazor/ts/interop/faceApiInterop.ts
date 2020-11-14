import { IFaceApi } from "../faceApi";

interface IDetectionOptions { withLandmarks: boolean, withDescriptors: boolean };

export class FaceApiInterop {

    /**
     * Last instance id created.
     * Used to keep track of each new FaceApi instance.
     */
    _lastInstanceId: number = 0;

    /**
     * Stores the FaceApi instances on the current session
     */
    _faceApiModels: { [id: number]: IFaceApi } = {};

    /**
     * Builds a FaceApi model.
     * @param detectionOptions
     * @param dotNetReference A reference to the .NET object where the callback should be made
     * @param modelLoadedCallbackName Name of the callback function to call when the model is loaded.
     */
    public buildFaceApi(detectionOptions: IDetectionOptions, dotNetReference, modelLoadedCallbackName = "onFaceApiModelLoaded"): IFaceApi {
        const newInstance = ml5.faceApi(detectionOptions, () => {
            // assign instance id.
            newInstance.instanceId = this._lastInstanceId;
            // increment instance id for next instance
            this._lastInstanceId++;
            // execute callback passing new instance
            dotNetReference.invokeMethod(modelLoadedCallbackName, newInstance);
        });
    }

    /**
     * Runs the detect function on a specific faceApi instance.
     * @param instanceId ID of the faceApi instance
     * @param image Image to run the detection
     * @param dotNetReference A reference to the .NET object where teh callback should be made
     * @param detectCompletedCallbackName Name of the callback function to call when the detection completes.
     */
    public detect(instanceId, image, dotNetReference, detectCompletedCallbackName = "onDetectCompleted") {
        this._faceApiModels[instanceId].detect(image, (err, results) => dotNetReference.invokeMethod(detectCompletedCallbackName, err, results))
    }
}