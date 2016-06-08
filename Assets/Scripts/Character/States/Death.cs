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
            foreach (var collider in Character.gameObject.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            Character.animator.Play("Defeat");
            Character.Die();
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