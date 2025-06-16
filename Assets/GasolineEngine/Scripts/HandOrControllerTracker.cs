using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandOrControllerTracker : MonoBehaviour
{
    [Header("Transforms that actually move with the XR rig")]
    public Transform leftController;   // e.g. “XR Origin/Camera Offset/LeftHand Controller”
    public Transform leftHand;         // e.g. “XR Origin/Camera Offset/LeftHand”
    public FollowTarget followTarget;  // the script that sticks the panel to its target

    [Tooltip("How often (seconds) we re-check the active device.")]
    public float pollInterval = 0.25f;

    float _nextPollTime;

    void Update()
    {
        if (Time.time < _nextPollTime) return;
        _nextPollTime = Time.time + pollInterval;

        // Get whatever device is currently representing XRNode.LeftHand
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (!device.isValid) return;   // nothing connected yet

        // If the device exposes handData → it’s a hand-tracking device
        bool isHand = device.TryGetFeatureValue(CommonUsages.handData, out Hand _);

        followTarget.SetTarget(isHand ? leftHand : leftController);
    }
}
