namespace Assets.Scripts.Character.States.Block
{
    public class BlockMidRecover : BlockRecover
    {
        public BlockMidRecover(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/RECOVER";

            timer.TotalTime = 0.1f;
            timer.OnFinish = () => Fsm.ChangeState("MOVEMENT");
        }
    }
}
