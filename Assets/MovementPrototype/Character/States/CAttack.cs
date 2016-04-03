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

        public override void Update()
        {
            Character.Look();
        }


    }
}