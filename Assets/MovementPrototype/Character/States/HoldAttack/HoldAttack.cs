using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

namespace Assets.MovementPrototype.Character.States.HoldAttackStates
{
    public class HoldAttack : ProxyState
    {
        public HoldAttack(CharFsm fsm) : base(fsm)
        {
            Name = "HATTACK";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            Assert.IsTrue(args.Length == 1);
            var evt = args[0] as InputEvent.Attack;
            Assert.IsNotNull(evt);

            Fsm.ChangeState("HATTACK/LIGHT/WINDUP", additionalDeltaTime);
        }
    }
}
