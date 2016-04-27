using UnityEngine;

namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{

    public abstract class BaseWindUp : AnimatedState
    {
        const float speed = 1.5f;
        public BaseWindUp(CharFsm fsm) : base(fsm)
        {
            turnRate = 0f;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}

