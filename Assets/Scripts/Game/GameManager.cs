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
        Round,
        PostRound,
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
        List<GameObject> minionPrefabs;
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

        // FSM basics
        public void EnterState(GameState newState)
        {
            Debug.Log("Enter= " + newState + " Was in =" + State);
            ExitState(State);
            State = newState;
            switch (State)
            {
                case GameState.Load:
                    LoadEnter();
                    EnterState(GameState.Round);
                    break;
                case GameState.PreRound:
                    PreRoundEnter();
                    break;
                case GameState.Round:
                    RoundEnter();
                    break;
                case GameState.PostRound:
                    PostRoundEnter();
                    break;
                case GameState.EndGame:
                    EndGameEnter();
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
                    PreRoundUpdate();
                    break;
                case GameState.Round:
                    RoundUpdate();
                    break;
                case GameState.PostRound:
                    PostRoundUpdate();
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
                case GameState.Round:
                    break;
                case GameState.PostRound:
                    break;
                case GameState.EndGame:
                    break;
                default:
                    break;
            }
        }

        // Load
        void LoadEnter()
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
                team.SpawnMinions(minionPrefabs[i], minionPerTeam);
                player.Character.CanControl = false;
                i++;
            }
        }

        // Pre Round
        void PreRoundEnter()
        {
            foreach (var team in Teams)
            {
                team.PreRound();
            }
        }
        void PreRoundUpdate()
        {
            if (Teams.All(team => team.Ended))
            {
                EnterState(GameState.Round);
            }
        }

        // Round
        void RoundEnter()
        {
            foreach (var team in Teams)
            {
                team.Round();
            }
        }
        void RoundUpdate()
        {
            var playingTeams = Teams.Where(team => !team.Leader.Ended);
            if (playingTeams.Count() <= 1)
            {
                EnterState(GameState.PostRound);
            }
        }

        // Post Round
        void PostRoundEnter()
        {
            foreach (var team in Teams)
            {
                team.PostRound();
            }
        }
        void PostRoundUpdate()
        {
            var playingTeams = Teams.Where(team => !team.Leader.Ended);
            if (playingTeams.Count() == 0)
            {
                if (Teams.Where(team => team.Leader.Lives > 0).Count() <= 1)
                {
                    EnterState(GameState.EndGame);
                } else
                {
                    EnterState(GameState.PreRound);
                }
            }
       }

        // End Game
        void EndGameEnter()
        {
            Invoke("ShowResultScreen", 1.5f);
        }
        // Invoked functions
        void RestartRound()
        {
            EnterState(GameState.PreRound);
        }
        void ShowResultScreen()
        {
            resultOverlay.SetActive(true);
        }
    }
}
