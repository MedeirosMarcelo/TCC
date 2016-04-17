using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class AttackState : ProxyState
    {
        public AttackState(CFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
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
                    if (evt.isHeavy)
                    {
                        Fsm.ChangeState("RIGHT/HEAVY/WINDUP", additionalDeltaTime);
                    }
                    else
                    {
                        Fsm.ChangeState("RIGHT/LIGHT/WINDUP", additionalDeltaTime);
                    }
                }
                else if (moveDirection.x > 0.2f)
                {
                    if (evt.isHeavy)
                    {
                        Fsm.ChangeState("LEFT/HEAVY/WINDUP", additionalDeltaTime);
                    }
                    else
                    {
                        Fsm.ChangeState("LEFT/LIGHT/WINDUP", additionalDeltaTime);
                    }
                }
                else
                {
                    if (evt.isHeavy)
                    {
                        Fsm.ChangeState("DOWN/HEAVY/WINDUP", additionalDeltaTime);
                    }
                    else
                    {
                        Fsm.ChangeState("DOWN/LIGHT/WINDUP", additionalDeltaTime);
                    }
                }
            }
            else if (lastStateName == "RUN")
            {
                if (evt.isHeavy)
                {
                    Fsm.ChangeState("RIGHT/HEAVY/WINDUP", additionalDeltaTime);
                }
                else
                {
                    Fsm.ChangeState("RIGHT/LIGHT/WINDUP", additionalDeltaTime);
                }
            }
        }
    }
}
