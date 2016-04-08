using Assets.MovementPrototype.Character.States;
using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class DashFsm : BaseFsm, IState
    {
        public CController Character { get; protected set; }
        public Vector3 Velocity { get; set; }
        public const float dashTime = 0.2f;
        const float speed = 8f;

        public DashFsm(CFsm fsm) : base(fsm)
        {
            Name = "DASH";
            Character = fsm.Character;
            var loader = new StateLoader<DashFsm>();
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.DashStates");
            Current = dict["ACCEL"];
        }

        public override void Enter(StateTransitionArgs args)
        {
            DashTransitionArgs dashArgs = (DashTransitionArgs)args;
            Velocity = dashArgs.Event.move.vector.normalized * speed;
            Current = dict["ACCEL"];
            Current.Enter(args);
        }
        public override void Exit(StateTransitionArgs args)
        {
            Current.Exit(args);
        }

        public override void PreUpdate()
        {
            if (Current.Name == "ENDED")
            {
                Fsm.ChangeState("IDLE");
            }
            else
            {
                base.PreUpdate();
            }
        }
    }
}