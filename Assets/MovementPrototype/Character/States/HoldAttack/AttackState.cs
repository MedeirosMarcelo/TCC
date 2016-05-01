using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class AttackState : ProxyState
    {
        public AttackState(CharFsm fsm)
            : base(fsm)
        {
            Name = "ATTACK";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = args[0] as InputEvent.Attack;
            Assert.IsNotNull(evt);

            if (Input.run >= 0.25f) //TODO: Usar LOCKED do BlockBase?
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
            else
            {
                if (Character.Stance == SwordStance.High)
                {
                    Fsm.ChangeState("DOWN/LIGHT/WINDUP", additionalDeltaTime);
                }
                else
                {
                    Vector3 moveDirection = Character.transform.InverseTransformDirection(evt.Move.vector.normalized);
                    if (moveDirection.x < -0.2f)
                    {
                        Fsm.ChangeState("RIGHT/LIGHT/WINDUP", additionalDeltaTime);
                    }
                    else
                    {
                        Fsm.ChangeState("LEFT/LIGHT/WINDUP", additionalDeltaTime);
                    }
                }
            }
        }
    }
}
