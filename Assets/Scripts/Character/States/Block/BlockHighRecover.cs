namespace Assets.Scripts.Character.States.Block
{
    public class BlockHighRecover : BlockRecover
    {
        public BlockHighRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/RECOVER";

            timer.TotalTime = 0.1f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");
        }
    }
}
