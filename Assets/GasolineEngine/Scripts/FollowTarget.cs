using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform leftHand;
    public Transform leftController;
    public Vector3 positionOffset = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;

    void LateUpdate()
    {
        if (leftHand != null && leftController != null)
        {
            Vector3 averagePosition = (leftHand.position + leftController.position) / 2f;
            transform.position = averagePosition + transform.TransformVector(positionOffset);
            transform.rotation = Quaternion.Slerp(leftHand.rotation, leftController.rotation, 0.5f)
                                  * Quaternion.Euler(rotationOffset);
        }
        else if (leftController != null)
        {
            transform.position = leftController.position + leftController.TransformVector(positionOffset);
            transform.rotation = leftController.rotation * Quaternion.Euler(rotationOffset);
        }
        else if (leftHand != null)
        {
            transform.position = leftHand.position + leftHand.TransformVector(positionOffset);
            transform.rotation = leftHand.rotation * Quaternion.Euler(rotationOffset);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        leftController = newTarget;
    }
}