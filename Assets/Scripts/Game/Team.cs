using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Minion;
using Assets.Scripts.Common;

namespace Assets.Scripts.Game
{

    using CharacterController = Character.CharacterController;

    public class Team
    {
        private const float spawnDistance = 2f;
        private GameManager manager;

        public bool Ended { get { return Leader.Ended && Minions.All(m => m.Ended); } }
        public List<Team> OtherTeams { get { return manager.Teams.FindAll(t => t != this); } }

        public Transform Spawn { get; private set; }
        public CharacterController Leader { get; private set; }
        public List<MinionController> Minions { get; private set; }
        public List<ITargetable> Targets { get; private set; }
        public Team(GameManager manager, Transform spawn)
        {
            this.manager = manager;
            Spawn = spawn;
            Minions = new List<MinionController>();
            Targets = new List<ITargetable>();
        }
        // Spawn
        public void SpawnCharacter(GameObject prefab, Player player)
        {

            Assert.IsFalse(prefab == null, "Missing character prefab");
            var obj = Object.Instantiate(prefab, Spawn.position, Spawn.rotation) as GameObject;
            Assert.IsFalse(obj == null, "Failed to instantiate character");
            var character = obj.GetComponent<CharacterController>();
            Assert.IsFalse(character == null, "New character missing CharController");
            player.Character = character;
            Leader = character;
            Targets.Add(character);
            character.Team = this;
            character.Id = player.Id;
        }
        public void SpawnMinion(GameObject prefab, Vector3 position)
        {
            Assert.IsFalse(prefab == null, "Missing prefab");
            var obj = Object.Instantiate(prefab, position, Spawn.rotation) as GameObject;
            Assert.IsFalse(obj == null, "Failed to instantiate prefab");
            var minion = obj.GetComponent<MinionController>();
            Assert.IsFalse(minion == null, "New object missing MinionController");
            Minions.Add(minion);
            Targets.Add(minion);
            minion.Team = this;
        }
        public void SpawnMinions(GameObject prefab, int count = 1)
        {
            Vector3 position = Spawn.position;
            position -= (Spawn.forward * spawnDistance);                     // behind spawn
            position -= (Spawn.right * spawnDistance * ((count - 1) / 2f));  // left from spawn 

            for (int i = 0; i < count; i++)
            {
                SpawnMinion(prefab, position);
                position += (Spawn.right * spawnDistance); // step right from last spawn
            }
        }
        // Win/Defeat
        public void Defeat()
        {
                foreach (var minion in Minions)
                {
                    minion.Die();
                }
        }
        public void Win()
        {
            Leader.Win();
            foreach (var minion in Minions.Where(minion => !minion.IsDead))
            {
                minion.Win();
            }
        }
        // Round
        public void PreRound()
        {
            Leader.PreRound();
            foreach (var minion in Minions)
            {
                minion.PreRound();
            }
        }
        public void Round()
        {
            Leader.Round();
            foreach (var minion in Minions)
            {
                minion.Round();
            }
        }
        public void PostRound()
        {
            Leader.PostRound();
            foreach (var minion in Minions)
            {
                minion.PostRound();
            }
        }
    }

}

