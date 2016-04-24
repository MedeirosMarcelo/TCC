using UnityEngine;

namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public abstract class BaseWindUp : AnimatedState
    {
        const float LookTurnRate = 0.25f;
        const float LockTurnRate = 0.15f;
        const float Speed = 1f;
        public BaseWindUp(CharFsm fsm) : base(fsm)
        {
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Look(LookTurnRate, LockTurnRate);
            Character.Move(Transform.position + ((Transform.forward * Speed) * Time.fixedDeltaTime));
        }
    }
}

