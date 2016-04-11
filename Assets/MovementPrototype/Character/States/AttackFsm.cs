using Assets.MovementPrototype.Character.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States
{
    public class AttackFsm : BaseFsm, IState
    {
        public CController Character { get; protected set; }
        public Vector3 Velocity { get; set; }
        const float speed = 16f;

        public AttackFsm(CFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
            Character = fsm.Character;
            var loader = new StateLoader<AttackFsm>();
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates");
            Current = dict["WINDUP"];
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = (InputEvent.Attack)args[0];
            Velocity = evt.Move.vector.normalized * speed;
            Current = dict["WINDUP"];
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }
}