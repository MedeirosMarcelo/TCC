using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public int debugAddPlayers = 0;
        public Transform[] spawns = new Transform[0];
        public int maxScore = 5;
        public int minionPerTeam = 5;

        [Header("Prefabs")]
        [SerializeField]
        List<GameObject> characterPrefabs;
        [SerializeField]
        GameObject minionPrefab;
        [SerializeField]
        GameObject resultOverlay;
        AudioSource[] audioSource;

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

        void Awake()
        {
            Teams = new List<Team>();
        }
        void Start()
        {
            audioSource = GetComponents<AudioSource>();
            AudioManager.Play(ClipType.ArenaEnvironment, audioSource[0]);
            AudioManager.Play(ClipType.ArenaBGM, audioSource[1]);
            EnterState(GameState.Load);
        }
        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton6)
                || UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
            UpdateState();
        }
        public void EnterState(GameState newState)
        {
            Debug.Log("Enter= " + newState + " Was in =" + State);
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
            int i = 0;
            foreach (var player in players)
            {
                spawn.MoveNext();
                var team = new Team(this, spawn.Current);
                Teams.Add(team);
                team.SpawnCharacter(characterPrefabs[i], player);
                team.SpawnMinions(minionPrefab, minionPerTeam);
                player.Character.CanControl = false;
                i++;
            }
        }

        void EnterPreRound()
        {
            Assert.IsTrue(spawns.Length >= Teams.Count, "More player than spawn points");
            foreach (var team in Teams)
            {
                team.Reset();
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
            Invoke("RestartRound", 1.5f);
        }
        void RestartRound()
        {
            EnterState(GameState.PreRound);
        }
        void EnterEndGame()
        {
            Invoke("ShowResultScreen",1.5f);
        }
        void ShowResultScreen()
        {
            resultOverlay.SetActive(true);
        }
        public void CheckEndRound()
        {
            if (State != GameState.RoundEnd)
            {
            // If theres only one other team with leader alive, it wins
            var playingTeams = Teams.Where(team => !team.Leader.Ended);
            if (playingTeams.Count() <= 1)
            {
                if (playingTeams.Count() == 1) {
                    playingTeams.First().Win();
                }
                EnterState(GameState.RoundEnd);
            }
            }
        }
        bool CheckGameEnd()
        {
            return (Teams.Where(team => team.Leader.Lives > 0).Count() <= 1);
        }
    }
}
