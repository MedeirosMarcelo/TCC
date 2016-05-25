using UnityEngine;

namespace Assets.AIPrototype.States
{
    public class Circle : MinionState
    {
        const float angleStep = 20f * Mathf.Deg2Rad; // as radians 
        const float spiralStep = 0.9f; // close in by (spiralStep * Distance)

        TimerBehaviour timer;
        public Circle(MinionFsm fsm) : base(fsm)
        {
            Name = "CIRCLE";
            staminaCost = 0.025f;
            timer = new TimerBehaviour(this);
            timer.TotalTime = 1f;
            timer.OnFinish = () => NextState();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Look();
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            var destination = (Transform.position - Target.position);                         // vector from target to minion
            destination = Vector3.RotateTowards(destination, Target.forward, -angleStep, 0f); // rotate it towards target's back
            destination = destination * spiralStep;                                           // get closer in
            destination += Target.position;                                                   // get global position
            Minion.SetDestination(destination, updateRotation: false);
        }
        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
            Minion.Stop();
        }
    }
}