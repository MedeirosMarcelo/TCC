using System;
using UnityEngine;
using UnityEngine.Assertions;
using Assets.MovementPrototype.Character.States.HoldAttackStates;

namespace Assets.Scripts.Character
{
    public class CharacterOverride : MonoBehaviour
    {
#if UNITY_EDITOR
        public enum Action
        {
            BLOCKMID,
            BLOCKHIGH,
        };

        [Header("F9 Enable/Disable")]
        public bool Enable = false;

        [Header("F10 toggle Action")]
        public Action action = Action.BLOCKMID;

        CharacterController character;
        BaseFsm Fsm { get { return character.Fsm; } }
        IState Current { get { return Fsm.Current; } }

        void Start()
        {
            character = gameObject.GetComponent<CharacterController>();
            Assert.IsNotNull(character);
        }

        void NextAction()
        {
            action++;
            if ((int)action == Enum.GetValues(typeof(Action)).Length)
            {
                action = 0;
            }
        }

        void UpdateAction()
        {
            switch (action)
            {
                case Action.BLOCKMID:
                    Fsm.ChangeState("BLOCK/MID/SWING");
                    break;
                case Action.BLOCKHIGH:
                    Fsm.ChangeState("BLOCK/HIGH/SWING");
                    break;
            }

        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F9))
            {
                Enable = !Enable;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.F10))
            {
                NextAction();
            }
            if (Enable)
            {
                UpdateAction();
            }
        }
#endif
    }
}
