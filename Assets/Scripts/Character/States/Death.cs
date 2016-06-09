using UnityEngine;

namespace Assets.Scripts.Character.States
{
    public class Death : CharacterState
    {
        public Death(CharacterFsm fsm)
            : base(fsm)
        {
            Name = "DEATH";
            turnRate = 0f;
        }
        public override void Enter(string lastName, string nextName, float additionalDeltaTime, params object[] args)
        {
            Character.rbody.isKinematic = true;
            Character.transform.Find("Hitbox").gameObject.SetActive(false);
            Character.transform.Find("PushCollider").gameObject.SetActive(false);
            Character.AttackCollider.enabled = false;
            Character.animator.Play("Defeat");
            AudioManager.Play(ClipType.Dead, Character.audioSource);
            Character.Die();
        }

        public override void Exit(string lastStateName, string nextStateName, float additionalDeltaTime = 0, params object[] args)
        {
            Character.rbody.isKinematic = false;
            Character.transform.Find("Hitbox").gameObject.SetActive(true);
            Character.transform.Find("PushCollider").gameObject.SetActive(true);
            Character.AttackCollider.enabled = true;
            base.Exit(lastStateName, nextStateName, additionalDeltaTime, args);
        }

        public override void OnTriggerEnter(Collider collider) { }

        void PlantSword()
        {
            Vector3 pos = Character.transform.position;
            pos.y += 0.5f;
            GameObject.Instantiate(Character.swordPrefab, pos, Character.swordPrefab.transform.rotation);
            Character.transform.Find("Model").Find("Swords").Find("Sword " + Character.Lives).gameObject.SetActive(false);
        }
    }
}