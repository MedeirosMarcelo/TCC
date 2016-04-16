using Assets.MovementPrototype.Character.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States
{
    public class AttackFsm : BaseFsm, IState
    {
        public CController Character { get; protected set; }
        const float speed = 16f;

        public AttackFsm(CFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
            Character = fsm.Character;
            var loader = new StateLoader<AttackFsm>();
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates.Right");
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates.Left");
            loader.LoadStates(this, "Assets.MovementPrototype.Character.States.AttackStates.Down");
            Current = dict["DOWN/WINDUP"];
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = (InputEvent.Attack)args[0];

            if (lastStateName == "LOCK")
            {
                Vector3 moveDirection = Character.transform.InverseTransformDirection(evt.Move.vector.normalized);
                if (moveDirection.x < -0.2f)
                {
                    if (evt.isHeavy) Current = dict["RIGHT/HEAVY/WINDUP"];
                    else Current = dict["RIGHT/WINDUP"];
                }
                else if (moveDirection.x > 0.2f)
                {
                    if (evt.isHeavy) Current = dict["LEFT/HEAVY/WINDUP"];
                    else Current = dict["LEFT/WINDUP"];
                }
                else
                {
                    if (evt.isHeavy) Current = dict["DOWN/HEAVY/WINDUP"];
                    else Current = dict["DOWN/WINDUP"];
                }
            }
            else if (lastStateName == "RUN")
            {
                if (evt.isHeavy) Current = dict["RIGHT/HEAVY/WINDUP"];
                else Current = dict["RIGHT/WINDUP"];
            }

            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
        }
    }
}