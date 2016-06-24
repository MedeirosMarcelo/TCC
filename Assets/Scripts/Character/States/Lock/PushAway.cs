using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Character.States.Lock
{
    public class PushAway : AnimatedState
    {
        Collider collider;
        Vector3 direction;
        const float speed = 1.5f;

        public PushAway(CharacterFsm fsm) : base(fsm)
        {
            Name = "LOCK/PUSHAWAY";
            nextState = "MOVEMENT";
            totalTime = 0.5f;
            Animation = "PushAway";
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length > 0);
            Assert.IsTrue(args[0] is Collider);
            collider = (Collider)args[0];
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            direction = Transform.position - collider.transform.position;
            direction = direction.xz();
            direction.Normalize();
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((direction * speed) * Time.fixedDeltaTime));
        }

        public override void Look()
        {
            const float turnUnit = Mathf.PI / 30; // 12'
            var forward = Vector3.RotateTowards(
                           Transform.forward,
                           direction * -1,
                           turnUnit * turnRate * lockedTurnModifier,
                           1f);
            forward.y = 0f;
            Character.Forward(forward);
        }
    }
}