namespace Assets.Scripts.Character.States.Block
{
    public class BlockHighWindUp : BlockWindUp
    {
        public BlockHighWindUp(CharacterFsm fsm) : base(fsm)
        {
            Name = "BLOCK/HIGH/WINDUP";

            timer.TotalTime = 0.1f;
            timer.OnFinish = () => Fsm.ChangeState("BLOCK/HIGH/SWING");

            animation.TotalTime = 0.767f;
            animation.PlayTime = 0.1f;
            animation.Name = "BlockHigh";
        }
    }
}
