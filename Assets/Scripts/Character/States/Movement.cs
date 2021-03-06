﻿namespace Assets.Scripts.Character.States
{
    using InputEvent = Input.InputEvent;

    public class Movement : CharacterState
    {
        SwordStance currentStance;
        public Movement(CharacterFsm fsm)
            : base(fsm)
        {
            Name = "MOVEMENT";
            canPlayerMove = true;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            currentStance = Character.Stance;
            PlayStanceAnimation(Character.Stance);
        }

        public override void PreUpdate()
        {
            if (Input.highStance)
            {
                Character.Stance = SwordStance.High;
            }
            else
            {
                Character.Stance = SwordStance.Right;
            }

            if (Input.buffer.NextEventIs<InputEvent.Attack>())
            {
                var evt = Input.buffer.Pop<InputEvent.Attack>();
                Fsm.ChangeState("ATTACK", 0f, evt); //"HATTACK/LIGHT/WINDUP"
            }
            else if (Input.buffer.NextEventIs<InputEvent.Lunge>())
            {
                var evt = Input.buffer.Pop<InputEvent.Lunge>();
                Fsm.ChangeState("LUNGE/LIGHT/WINDUP", 0f, evt);
            }
            else if (Input.buffer.NextEventIs<InputEvent.Block>())
            {
                if (Character.Stance == SwordStance.High)
                {
                    var evt = Input.buffer.Pop<InputEvent.Block>();
                    Fsm.ChangeState("BLOCK/HIGH/WINDUP", 0f, evt);
                }
                else
                {
                    var evt = Input.buffer.Pop<InputEvent.Block>();
                    Fsm.ChangeState("BLOCK/MID/WINDUP", 0f, evt);
                }
            }
            else if (Input.buffer.NextEventIs<InputEvent.Dash>())
            {
                var evt = Input.buffer.Pop<InputEvent.Dash>();
                Fsm.ChangeState("DASH", 0f, evt);
            }
            else
            {
                base.PreUpdate();
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            ChangeStanceAnimation();
        }

        void ChangeStanceAnimation()
        {
            if (currentStance != Character.Stance)
            {
                currentStance = Character.Stance;
                PlayStanceAnimation(currentStance);
            }
        }

        void PlayStanceAnimation(SwordStance stance)
        {
            switch (stance)
            {
                default:
                case SwordStance.Left:
                case SwordStance.Right:
                    Character.Animator.CrossFade("MoveLow", 0.3f, -1, 0);
                    break;
                case SwordStance.High:
                    Character.Animator.CrossFade("MoveHigh", 0.3f, -1, 0);
                    break;
            }
        }
    }
}