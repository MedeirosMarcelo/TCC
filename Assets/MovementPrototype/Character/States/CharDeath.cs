using UnityEngine;

namespace Assets.MovementPrototype.Character.States
{
    public class CharDeath : CharState
    {
        public CharDeath(CharFsm fsm)
            : base(fsm)
        {
            Name = "DEATH";
            turnRate = 0f;
        }
        public override void Enter(string lastName, string nextName, float additionalDeltaTime, params object[] args)
        {
            Character.animator.Play("Death");
            Character.lives--;
            if (Character.lives > 0)
            {
               PlantSword();
            }
            Game.CheckEndRound();
        }

        public override void OnTriggerEnter(Collider collider) { }

        void PlantSword()
        {
            Vector3 pos = Character.transform.position;
            pos.y += 0.5f;
            GameObject.Instantiate(Game.PlantedSword, pos, Game.PlantedSword.transform.rotation);
            Character.transform.Find("Model").Find("Swords").Find("Sword " + Character.lives).gameObject.SetActive(false);
        }
    }
}