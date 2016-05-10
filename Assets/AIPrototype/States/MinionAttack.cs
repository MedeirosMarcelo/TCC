using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class MinionAttack : MinionState
    {
        public MinionAttack(MinionFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
            totalTime = 1f;
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SwordCollider.enabled = true;
            nextState = (Random.value > 0.2) ? "ATTACK" : "WANDER";
            Minion.Animator.SetFloat("Speed", 1f / totalTime);
            Minion.Animator.Play((Random.value > 0.5) ? "LeftSwing" : "RightSwing");
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.SwordCollider.enabled = false;
            Minion.Animator.Play("Idle");
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
        public void Look()
        {
            var forward = Vector3.RotateTowards(
                Transform.forward,
                (Target.transform.position - Transform.position).normalized,
                Minion.NavAgent.angularSpeed * Time.fixedDeltaTime,
                1f);
            Minion.Forward(forward);
        }
    }
}
