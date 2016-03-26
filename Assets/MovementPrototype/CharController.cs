using UnityEngine;
using System;
using System.Collections;

public class CharController : MonoBehaviour {

    BaseInput input;
    Rigidbody rbody;

    public float moveSpeed = 4f;
    public float dashForce = 12f;
    float maxAcceleration = 2f;
    public float turnSpeed = 0.2f;
    Vector3 velocity;


    public void Awake() {
        input = new ControllerInput(ControllerId.One);
        rbody = GetComponent<Rigidbody>();
    }

    public void Update() {
        input.Update();
        Look();
    }

    public void FixedUpdate() {
        Move();
        Dash();
        input.FixedUpdate();
    }

    public void Move() {
        // Calculate how fast we should be moving
        var relativeVelocity = input.move.vector * moveSpeed;
        // Calcualte the delta velocity
        var velocityChange = relativeVelocity - velocity;
        velocityChange.y = 0;
        // Limit acceleration
        if (velocityChange.magnitude > maxAcceleration) {
            velocityChange = velocityChange.normalized * maxAcceleration;
        }

        velocity += velocityChange;
        rbody.MovePosition(transform.position + (velocity * Time.deltaTime));

    }

    public void Look() {
        var vec = input.look.vector;
        if (vec.magnitude > 0.25f) {
            transform.forward = Vector3.Lerp(transform.forward, vec, turnSpeed);
        }
    }

    public float dashSpeed = 20f;
    Vector3 dashVelocity;

    public void Dash() {
        if (dashVelocity.magnitude > 0.3) {
            dashVelocity = Vector3.Lerp(dashVelocity, Vector3.zero , 0.2f );
            rbody.MovePosition(transform.position + dashVelocity * Time.deltaTime);
        } else if (input.dash) {
            Debug.Log("Dodge");
            Vector3 direction = input.move.vector.normalized;
            dashVelocity = direction * dashSpeed;
            rbody.MovePosition(transform.position + dashVelocity * Time.deltaTime);
        }
    }
}
