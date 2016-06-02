using UnityEngine;
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
        [Header("Game Config")]
        public Transform[] spawns = new Transform[0];
        public int maxScore = 5;
        public int minionPerTeam = 5;

        [Header("Prefabs")]
        [SerializeField]
        GameObject characterPrefab;
        [SerializeField]
        GameObject minionPrefab;

        // Private state 
        public GameState State { get; private set; }
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
                    EnterState(GameState.PlayRound);
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
            foreach (var spawn in spawns)
            {
                var team = new Team(this, spawn);
                Teams.Add(team);
                //team.SpawnCharacter(characterPrefab, player);
                team.SpawnMinions(minionPrefab, minionPerTeam);
                //player.Character.CanControl = false;
            }
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
            SceneManager.LoadScene("Result");
        }
        public void CheckEndRound()
        {
            int aliveCount = 0;
            foreach (Player pl in PlayerManager.GetPlayerList())
            {
                if (pl.Character.fsm.Current.Name != "DEATH")
                {
                    aliveCount++;
                }
            }
            if (aliveCount <= 1)
            {
                EnterState(GameState.RoundEnd);
            }
        }
        bool CheckGameEnd()
        {
            int aliveCount = 0;
            foreach (Player pl in PlayerManager.GetPlayerList())
            {
                if (pl.Character.lives > 0)
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
