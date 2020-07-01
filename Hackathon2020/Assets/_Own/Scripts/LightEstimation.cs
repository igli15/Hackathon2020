using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    [SerializeField] private ARCameraManager _arCameraManager;
    
    private Light _light;
    
    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        _arCameraManager.frameReceived += frameUpdated;
    }

    private void OnDisable()
    {
        _arCameraManager.frameReceived -= frameUpdated;
    }

    private void frameUpdated(ARCameraFrameEventArgs pArgs)
    {
        if (pArgs.lightEstimation.averageBrightness.HasValue)
        {
            _light.intensity = pArgs.lightEstimation.averageBrightness.Value;
        }
        
        if (pArgs.lightEstimation.averageColorTemperature.HasValue)
        {
            _light.colorTemperature = pArgs.lightEstimation.averageColorTemperature.Value;
        }
        
        if (pArgs.lightEstimation.colorCorrection.HasValue)
        {
            _light.color  = pArgs.lightEstimation.colorCorrection.Value;
        }
    }
}
