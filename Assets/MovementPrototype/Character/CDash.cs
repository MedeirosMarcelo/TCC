using UnityEngine;
using System;
using System.Collections;

public class CDash : CState {
    public CDash(CFsm fsm) : base(fsm, CStateEnum.Dash) {
    }

    public override void PreUpdate() {
        if (state == State.Ended) {
            base.PreUpdate();
        }
    }

    const float speed = 12f;
    Vector3 velocity;

    public override void Enter() {
        state = State.Accel;
        velocity = input.move.vector.normalized * speed;
    }

    enum State {
        Accel,
        Platou,
        Deccel,
        Ended
    };
    State state;

    const float totalTime  = 0.2f;
    const float accelTime  = 0.2f * totalTime;
    const float platouTime = 0.5f * totalTime;
    const float deccelTime = 0.3f * totalTime;

    float elapsed = 0f;
    public override void Update() {
        float propVelocity = 0f;
        elapsed += Time.fixedDeltaTime;

        switch (state) {
            case State.Accel:
                if (elapsed < accelTime) {
                    propVelocity = (elapsed / accelTime);
                    break;
                } else {
                    elapsed -= accelTime;
                    state = State.Platou;
                    goto case State.Platou;
                }

            case State.Platou:
                if (elapsed < platouTime) {
                    propVelocity = 1;
                    break;
                } else {
                    elapsed -= platouTime;
                    state = State.Deccel;
                    goto case State.Deccel;
                }

            case State.Deccel:
                if (elapsed < deccelTime) {
                    propVelocity = (1 - (elapsed / deccelTime));
                } else {
                    state = State.Ended;
                }
                break;
        }

        Debug.Log("Vprop = " + propVelocity);
        rbody.MovePosition(position + (velocity * propVelocity) * Time.fixedDeltaTime);
    }

    public override void Exit() {
        velocity = Vector3.zero;
        elapsed = 0;
    }
}