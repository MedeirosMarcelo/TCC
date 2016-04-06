using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CAttack : CState
    {
        public CAttack(CFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
        }

        public override void Enter(StateTransitionArgs args)
        {
            Character.swordAnimator.SetTrigger("Attack");
        }

        public override void Exit(StateTransitionArgs args)
        {
        }

        public override void PreUpdate()
        {
        }

        public float speed = 2f;
        Vector3 velocity;

        public override void Update()
        {
            Character.Look(0.5f);
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}