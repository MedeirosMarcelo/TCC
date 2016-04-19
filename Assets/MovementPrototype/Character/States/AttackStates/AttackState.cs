using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States.AttackStates
{
    public class AttackState : ProxyState
    {
        public AttackState(CharFsm fsm) : base(fsm)
        {
            Name = "ATTACK";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = args[0] as InputEvent.Attack;
            Assert.IsNotNull(evt);

            if (lastStateName == "MOVEMENT/LOCK" && evt.IsHigh)
            {
                if (evt.IsHeavy)
                {
                    Fsm.ChangeState("DOWN/HEAVY/WINDUP", additionalDeltaTime);
                }
                else
                {
                    Fsm.ChangeState("DOWN/LIGHT/WINDUP", additionalDeltaTime);
                }
            }
            else
            {
                if (evt.IsHeavy)
                {
                    if (Character.Stance == SwordStance.Left)
                    {
                        Fsm.ChangeState("LEFT/HEAVY/WINDUP", additionalDeltaTime);
                    }
                    else
                    {
                        Fsm.ChangeState("RIGHT/HEAVY/WINDUP", additionalDeltaTime);
                    }
                }
                else
                {
                    if (Character.Stance == SwordStance.Left)
                    {
                        Fsm.ChangeState("LEFT/LIGHT/WINDUP", additionalDeltaTime);
                    }
                    else
                    {
                        Fsm.ChangeState("RIGHT/LIGHT/WINDUP", additionalDeltaTime);
                    }
                }
            }
        }
    }
}
