using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public enum GameState
{
    Load,
    PreRound,
    PlayRound,
    RoundEnd,
    EndGame
}

[Obsolete] 
public class GameManager : MonoBehaviour
{

    public IList<CharController> characterList = new List<CharController>();
    public Transform[] spawn = new Transform[0];
    public byte maxScore = 5;
    public float roundClock;
    public GameState State { get; private set; }
    public GameObject PlantedSword;

    [SerializeField]
    GameObject characterPrefab;
   // Vector3[] spawnPosition = new Vector3[4];
    //Vector3[] spawnRotation = new Vector3[4];
    AudioSource[] audioSource;
    Timer timer = new Timer();

    void Start()
    {
        Debug.Log("STARTING WITH: " + PlayerManager.GetPlayerList().Count);
        audioSource = GetComponents<AudioSource>();
        if (audioSource.Length > 1)
        {
            AudioManager.Play(ClipType.ArenaEnvironment, audioSource[0]);
            AudioManager.Play(ClipType.ArenaBGM, audioSource[1]);
        }
        //spawnPosition[0] = new Vector3(0f, 0.393f, -3f);
        //spawnPosition[1] = new Vector3(0f, 0.393f, 3f);
        //spawnPosition[2] = new Vector3(-5f, 0.393f, 3f);
        //spawnPosition[3] = new Vector3(-5f, 0.393f, -3f);

        //spawnRotation[0] = Vector3.zero;
        //spawnRotation[1] = new Vector3(0f, 180, 0f);
        //spawnRotation[2] = new Vector3(0f, 180, 0f);
        //spawnRotation[3] = Vector3.zero;

        //-- Provisório até criar sistema de entrada de jogadores.
        if (PlayerManager.GetPlayerList().Count < 4) { //Adiciona players restantes caso tenha menos de quatro.
            int max = 4 - PlayerManager.GetPlayerList().Count;
            for (int x = 0; x < max; x++)
            {
                PlayerManager.AddPlayer((PlayerIndex)(4 - x));
            }
        }
        int i = 0;
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            pl.Character = characterList[i];
            pl.Character.CanControl = false;
            pl.Character.id = pl.PlayerId;
            i++;
        }
        //--

        EnterState(GameState.Load);
    }

    void Update()
    {
        StateMachine();
    }

    public void EnterState(GameState newState)
    {
        ExitState(State);
        State = newState;
        switch (State)
        {
            case GameState.Load:
                //LoadPlayers();
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

    void StateMachine()
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

    void LoadPlayers()
    {
        Debug.Log("LOADING: " + PlayerManager.GetPlayerList().Count);
        Vector3 position;
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            position = Vector3.zero; //currentMap.GetSpawnPosition(type, i);
            SpawnCharacter(pl.Character, position);
            pl.Character.CanControl = false;
        }
    }

    public GameObject SpawnCharacter(CharController cController, Vector3 position)
    {
        //GameObject pl = (GameObject)Instantiate(GetCharacterPrefab(cController.type), position, transform.rotation);
        //characterList.Add(player.Character);
        return null;//pl;
    }

    void EnterPlayRound()
    {
        int i = 0;
        int max = PlayerManager.GetPlayerList().Count - 1;
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            pl.Character.transform.position = spawn[i].position;
            pl.Character.transform.rotation = spawn[i].rotation;
            pl.Character.health = 2;
            pl.Character.fsm.ChangeState("MOVEMENT");
            pl.Character.CanControl = true;
            if (i < max) i++;
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

    public void CheckEndRound() {
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

    //public void Score(PlayerIndex controller) {
    //    MatchData.PlayerLives[controller] += 1;
    //    if (MatchData.PlayerLives[controller] < maxScore) {
    //        StartNextRound();
    //    }
    //    else {
    //        EndMatch();
    //    }
    //}

    //void RoundTimer()
    //{
    //    if (timer.Run(currentMap.clockTime))
    //    {
    //        StartNextRound();
    //    }
    //    roundClock = timer.GetTimeDecreasing();
    //}
}
