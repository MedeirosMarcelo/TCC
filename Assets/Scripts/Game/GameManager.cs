using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public enum GameState
    {
        Load,
        PreRound,
        PlayRound,
        RoundEnd,
        EndGame
    }
    public class GameManager : MonoBehaviour
    {
        [Header("Config")]
        public Transform[] spawns = new Transform[0];
        public int maxScore = 5;
        public int minionPerTeam = 5;

        [Header("Prefabs")]
        [SerializeField]
        GameObject characterPrefab;
        [SerializeField]
        GameObject minionPrefab;
        [SerializeField]
        GameObject resultOverlay;

        // Private state 
        public GameState state;
        public GameState State
        {
            get
            {
                return state;
            }
            private set
            {
                state = value;
            }
        }
        public List<Team> Teams { get; private set; }

        void Awake() {
            Teams = new List<Team>();
        }
        void Start()
        {
            EnterState(GameState.Load);
        }
        void Update()
        {
            UpdateState();
        }
        public void EnterState(GameState newState)
        {
            ExitState(State);
            State = newState;
            switch (State)
            {
                case GameState.Load:
                    LoadTeams();
                    EnterState(GameState.PreRound);
                    break;
                case GameState.PreRound:
                    EnterPreRound();
                    break;
                case GameState.PlayRound:
                    EnterPlayRound();
                    break;
                case GameState.RoundEnd:
                    EnterRoundEnd();
                    break;
                case GameState.EndGame:
                    EnterEndGame();
                    break;
                default:
                    break;
            }
        }
        void UpdateState()
        {
            switch (State)
            {
                case GameState.Load:
                    break;
                case GameState.PreRound:
                    break;
                case GameState.PlayRound:
                    break;
                case GameState.RoundEnd:
                    break;
                case GameState.EndGame:
                    break;
                default:
                    break;
            }
        }
        void ExitState(GameState newState)
        {
            switch (State)
            {
                case GameState.Load:
                    break;
                case GameState.PreRound:
                    break;
                case GameState.PlayRound:
                    ExitPlayRound();
                    break;
                case GameState.RoundEnd:
                    break;
                case GameState.EndGame:
                    break;
                default:
                    break;
            }
        }

        void LoadTeams()
        {
            var players = PlayerManager.GetPlayerList();
            Assert.IsTrue(spawns.Length >= players.Count, "More player than spawn points");

            var spawn = (spawns as IEnumerable<Transform>).GetEnumerator();
            foreach (var player in players)
            {
                spawn.MoveNext();
                var team = new Team(this, spawn.Current);
                Teams.Add(team);
                team.SpawnCharacter(characterPrefab, player);
                team.SpawnMinions(minionPrefab, minionPerTeam);
                player.Character.CanControl = false;
            }
        }

        void EnterPreRound()
        {
            var players = PlayerManager.GetPlayerList();
            Assert.IsTrue(spawns.Length >= players.Count, "More player than spawn points");
            var spawn = (spawns as IEnumerable<Transform>).GetEnumerator();
            foreach (var player in players)
            {
                spawn.MoveNext();
                player.Character.Reset(spawn.Current);
            }
            EnterState(GameState.PlayRound);
        }

        void EnterPlayRound()
        {
            foreach (Player pl in PlayerManager.GetPlayerList())
            {
                pl.Character.CanControl = true;
            }
        }
        void ExitPlayRound()
        {
            foreach (Player pl in PlayerManager.GetPlayerList())
            {
                pl.Character.CanControl = false;
            }
        }
        void EnterRoundEnd()
        {
            if (CheckGameEnd())
            {
                EnterState(GameState.EndGame);
                return;
            }
            StartCoroutine("WaitRestartRound");
        }
        IEnumerator WaitRestartRound()
        {
            yield return new WaitForSeconds(2f);
            EnterState(GameState.PreRound);
        }
        void EnterEndGame()
        {
            StartCoroutine("ShowResultScreen");
        }
        IEnumerator ShowResultScreen()
        {
            yield return new WaitForSeconds(1.5f);
            resultOverlay.SetActive(true);
        }
        public void CheckEndRound()
        {
            Debug.Log("Checking round end");
            int aliveCount = 0;
            foreach (Player pl in PlayerManager.GetPlayerList())
            {
                if (!pl.Character.IsDead)
                {
                    aliveCount++;
                }
            }
            if (aliveCount <= 1)
            {
                Debug.Log("ENDING ROUND");
                EnterState(GameState.RoundEnd);
            }
        }
        bool CheckGameEnd()
        {
            Debug.Log("Checking game end");
            int aliveCount = 0;
            foreach (Player pl in PlayerManager.GetPlayerList())
            {
                if (pl.Character.Lives > 0)
                {
                    aliveCount++;
                }
            }
            if (aliveCount <= 1)
            {
                return true;
            }
            return false;
        }
    }
}
