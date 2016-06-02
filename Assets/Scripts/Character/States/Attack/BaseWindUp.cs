namespace Assets.Scripts.Character.States.Attack
{

    public abstract class BaseWindUp : AnimatedState
    {
        const float speed = 1.5f;
        public BaseWindUp(CharacterFsm fsm) : base(fsm)
        {
            turnRate = 0f;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            //Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}

