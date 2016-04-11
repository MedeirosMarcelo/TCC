using Assets.MovementPrototype.Character.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States
{
    public class DashFsm : BaseFsm, IState
    {
        public CController Character { get; protected set; }
        public Vector3 Velocity { get; set; }
        const float speed = 16f;

        public DashFsm(CFsm fsm) : base(fsm)
        {
            Name = "DASH";
            Character = fsm.Character;
            var loader = new StateLoader<DashFsm>();
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
            Current = dict["ACCEL"];
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = (InputEvent.Dash)args[0];
            Velocity = evt.Move.vector.normalized * speed;
            Current = dict["ACCEL"];
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }
}