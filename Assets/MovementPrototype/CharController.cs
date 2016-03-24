using UnityEngine;
using System;
using System.Collections;

public class CharController : MonoBehaviour {

    BaseInput input;
    Rigidbody rbody;

    float runSpeed = 4f;
    float dashForce = 10f;
    float maxAcceleration = 2f;

    public void Awake() {
        input = new ControllerInput(ControllerId.One);
        rbody = GetComponent<Rigidbody>();
    }

    public void Update() {
        input.Update();
        Move();
        Look();
        Dodge();
    }

    public void FixedUpdate() {
        input.FixedUpdate();
    }

    public void Move() {
        // Calculate how fast we should be moving
        var relativeVelocity = input.move.vector * runSpeed;
        // Calcualte the delta velocity
        var velocityChange = relativeVelocity - rbody.velocity;
        velocityChange.y = 0;
        // Limit acceleration
        if (velocityChange.magnitude > maxAcceleration) {
            velocityChange = velocityChange.normalized * maxAcceleration;
        }
        rbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void Dodge() {
        if (input.dash) {
            Debug.Log("Dodge");
            Vector3 dash = input.move.vector.normalized;
            rbody.AddForce(dash * dashForce, ForceMode.Impulse);
        }
    }

    public void Look() {
        var vec = input.look.vector;
        if (vec.magnitude > 0.25f) {
            transform.forward = vec;
        }
    }
}
