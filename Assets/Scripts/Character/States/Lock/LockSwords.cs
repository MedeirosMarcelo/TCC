using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Character.States.Lock
{
    public class LockSwords : AnimatedState
    {
        Collider collider;
        public LockSwords(CharacterFsm fsm) : base(fsm)
        {
            Name = "LOCK/LOCKSWORDS";
            nextState = "LOCK/PUSHAWAY";
            totalTime = 0.5f;
            Animation = "LockSwords";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length > 0);
            Assert.IsTrue(args[0] is Collider);
            collider = (Collider)args[0];
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }

        public override void PreUpdate()
        {
            if (totalTime > 0f && elapsed >= totalTime)
            {
                Fsm.ChangeState(nextState, totalTime - elapsed, collider);
            }
        }
    }
}