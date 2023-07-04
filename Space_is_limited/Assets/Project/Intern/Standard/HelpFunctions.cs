using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains help functions for velocity and acceleration
/// </summary>
public static class HelpFunctions
{
    /// <summary>
    /// Returns the acceleration to reach the target velocity
    /// Is used to reach a certain speed
    /// </summary>
    /// <param name="targetVelocity">Target velocity of the object</param>
    /// <param name="currentVelocity">Current Velocity of the object</param>
    /// <param name="maxAcceleration">Maximum Acceleration, can cap the speed if the drag is to high</param>
    public static float GetAccelerationVelocity(float targetVelocity, float currentVelocity, float maxAcceleration)
    {
        var deltaV = targetVelocity - currentVelocity; // Delta V = Vf - Vi
        var accel = deltaV / Time.deltaTime; // a = delta V / delta T

        accel = Mathf.Clamp(accel, -maxAcceleration, maxAcceleration); // Clamp the acceleration
        return accel;
    }

    /// <summary>
    /// Returns the acceleration to reach the target velocity
    /// Is used to reach a certain speed in 2D
    /// </summary>
    /// <param name="targetVelocity">Target velocity of the object</param>
    /// <param name="currentVelocity">Current velocity of the object</param>
    /// <param name="maxAcceleration">Maximum Acceleration</param>
    /// <returns></returns>
    public static Vector2 GetAccelerationVelocity(Vector2 targetVelocity, Vector2 currentVelocity, float maxAcceleration)
    {
        var deltaV = targetVelocity - currentVelocity; // Delta V = Vf - Vi
        var accel = deltaV / Time.deltaTime; // a = delta V / delta T

        if (accel.magnitude > maxAcceleration)
        {
            accel = accel.normalized * maxAcceleration;
        }

        return accel;
    }
}
