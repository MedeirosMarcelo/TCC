using Assets.MovementPrototype.Character.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States
{
    public class AttackFsm : BaseFsm, IState
    {
        public CController Character { get; protected set; }
        const float speed = 16f;

        public AttackFsm(CFsm fsm)
            : base(fsm)
        {
            Name = "ATTACK";
            Character = fsm.Character;
            var loader = new StateLoader<AttackFsm>();
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates.Right");
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates.Left");
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates.Down");
            Current = dict["WINDUP"];
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = (InputEvent.Attack)args[0];

            Vector3 moveDirection = evt.Move.vector.normalized;
            if (moveDirection.x < -0.2f)
            {
                Current = dict["RIGHTWINDUP"];
            }
            else if (moveDirection.x > 0.2f)
            {
                Current = dict["WINDUP"];
            }
            else
            {
                Current = dict["DOWNWINDUP"];
            }

            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }
}