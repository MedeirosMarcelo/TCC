using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Down
{
    public class DownWindUp : AttackState
    {
        const float speed = 1f;
        public DownWindUp(AttackFsm fsm) : base(fsm)
        {
            Name = "DOWNWINDUP";
            nextState = "DOWNSWING";
            totalTime = 0.2f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("DownWindup");
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}
