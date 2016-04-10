using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class Recover : AttackState
    {
        public Recover(AttackFsm fsm) : base(fsm)
        {
            Name = "RECOVER";
            totalTime = 0.2f;
        }
        public override void PreUpdate()
        {
            if (elapsed >= totalTime)
            {
                Fsm.Fsm.ChangeState("IDLE");
            }
        }
    }
}
