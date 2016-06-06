namespace Assets.Scripts.Character
{
    public class CharacterFsm : BaseFsm
    {
        public CharacterController Character { get; protected set; }
        public CharacterFsm(CharacterController character) : base()
        {
            Character = character;
            StateLoader<CharacterFsm> loader = new StateLoader<CharacterFsm>();
            loader.LoadStates(this, "Assets.Scripts.Character.States");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Dash");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Attack");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Block");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Lock");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Stagger");
            Start("MOVEMENT");
        }
        public override void ChangeState(string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            //UnityEngine.Debug.Log(string.Format("{0} >> {1}", Current.Name, nextStateName));
            base.ChangeState(nextStateName, additionalDeltaTime, args);
        }
    }
}