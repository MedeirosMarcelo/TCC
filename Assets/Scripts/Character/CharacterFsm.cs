using UnityEngine;

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
            loader.LoadStates(this, "Assets.Scripts.Character.States.Attack");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Block");
            loader.LoadStates(this, "Assets.Scripts.Character.States.Lock");
            Start("MOVEMENT");
        }
        public override void ChangeState(string nextStateName, float additionalDeltaTime = 0f, params object[] args)
        {
            //UnityEngine.Debug.Log(string.Format("{0} >> {1}", Current.Name, nextStateName));
            
            if (Current.Name == "DEATH")
            {
                bool reset = false;
                if (args.Length > 0 && args[0].GetType() == typeof(bool))
                {
                    reset = (bool)args[0];
                }
                if (!reset)
                {
                    return;
                }
            }
            base.ChangeState(nextStateName, additionalDeltaTime, args);
        }
    }
}