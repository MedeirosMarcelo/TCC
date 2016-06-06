using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using Assets.Scripts.Minion;
using Assets.Scripts.Common;

namespace Assets.Scripts.Game
{
    using CharacterController = Character.CharacterController;

    public class Team
    {
        private const float spawnDistance = 2f;
        private GameManager manager;

        public List<Team> OtherTeams {
            get { return manager.Teams.FindAll(t => t != this); }
        }
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
        public void SpawnCharacter(GameObject prefab, Player player)
        {

            Assert.IsNotNull(prefab, "Missing character prefab");
            var obj = Object.Instantiate(prefab, Spawn.position, Spawn.rotation) as GameObject;
            Assert.IsNotNull(obj, "Failed to instantiate character");
            var character = obj.GetComponent<CharacterController>();
            Assert.IsNotNull(character, "New character missing CharController");
            player.Character = character;
            Leader = character;
            Targets.Add(character);
            character.Team = this;
            character.Id = player.Id;
        }
        public void SpawnMinion(GameObject prefab, Vector3 position)
        {
            Assert.IsNotNull(prefab, "Missing prefab");
            var obj = Object.Instantiate(prefab, position, Spawn.rotation) as GameObject;
            Assert.IsNotNull(obj, "Failed to instantiate prefab");
            var minion = obj.GetComponent<MinionController>();
            Assert.IsNotNull(minion, "New object missing MinionController");
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
    }
}

