using UnityEngine.Assertions;

namespace Assets.MovementPrototype.Character.States.BlockStates
{
    public class BlockState : ProxyState
    {
        public BlockState(CharFsm fsm) : base(fsm)
        {
            Name = "BLOCK";
        }
        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Assert.IsTrue(args.Length == 1);
            var evt = args[0] as InputEvent.Block;
            Assert.IsNotNull(evt);

            Fsm.ChangeState((evt.IsHigh) ? "BLOCK/HIGH/WINDUP" : "BLOCK/MID/WINDUP", additionalDeltaTime);
        }
    }
}