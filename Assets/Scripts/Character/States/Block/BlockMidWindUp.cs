namespace Assets.Scripts.Character.States.Block
{
    public class BlockMidWindUp : BlockWindUp
    {
        public BlockMidWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/MID/WINDUP";

            timer.TotalTime = 0.1f;
            timer.OnFinish = () => Fsm.ChangeState("BLOCK/MID/SWING");

            animation.TotalTime = 0.6f;
            animation.PlayTime = 0.1f;
            animation.Name = "BlockMid";
        }
    }
}
