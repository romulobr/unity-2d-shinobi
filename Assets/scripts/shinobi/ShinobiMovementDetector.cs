using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class ShinobiMovementDetector
{
    public enum Movement
    {
        Left,
        Right,
        None
    }

    public Movement DetectMoviment()
    {
        var horizontal = Input.GetAxis("Horizontal");
        Movement detectedMovement;
        if (horizontal > 0.5f)
        {
            detectedMovement = Movement.Right;
        }
        else if (horizontal < -0.5f)
        {
            detectedMovement = Movement.Left;
        }
        else
        {
            detectedMovement = Movement.None;
        }
        return detectedMovement;
    }
}