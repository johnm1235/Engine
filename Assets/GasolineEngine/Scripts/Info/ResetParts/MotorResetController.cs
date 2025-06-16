using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorResetController : MonoBehaviour
{
    private MotorPart[] motorParts;

    void Start()
    {
        motorParts = GetComponentsInChildren<MotorPart>();
    }

    public void ResetAllParts()
    {
        foreach (var part in motorParts)
        {
            part.ResetPart();
        }
    }

}
