﻿using UnityEngine;

namespace Assets.MovementPrototype.Character.States.AttackStates.Left
{
    public class LeftHeavyWindUp : AttackState
    {
        const float speed = 1f;
        public LeftHeavyWindUp(AttackFsm fsm) : base(fsm)
        {
            Name = "LEFT/HEAVY/WINDUP";
            nextState = "LEFT/HEAVY/SWING";
            totalTime = 0.3f;
        }

        public override void Enter(string lastStateName, string nextStateName, float additionalDeltaTime, params object[] args)
        {
            base.Enter(lastStateName, nextStateName, additionalDeltaTime, args);
            Character.animator.SetFloat("Speed", 1f / totalTime);
            Character.animator.Play("Windup");
        }

        public override void FixedUpdate() 
        {
            base.FixedUpdate();
            Character.Move(Transform.position + ((Transform.forward * speed) * Time.fixedDeltaTime));
        }
    }
}
