using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float attackSpeed;
    public VariableJoystick variableJoystick;
    public Rigidbody rbody;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rbody.AddForce(direction * attackSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}