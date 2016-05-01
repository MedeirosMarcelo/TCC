using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public IList<CharController> characterList = new List<CharController>();
    public byte maxScore = 5;
    public float roundClock;
    //public Map currentMap;
    public GameState State { get; private set; }
    public GameObject PlantedSword;

    [SerializeField]
    GameObject characterPrefab;
    Vector3[] spawnPosition = new Vector3[4];
    Vector3[] spawnRotation = new Vector3[4];
    Timer timer = new Timer();

    void Start()
    {
        spawnPosition[0] = new Vector3(0f, 0.393f, -3f);
        spawnPosition[1] = new Vector3(0f, 0.393f, 3f);
        spawnPosition[2] = new Vector3(-5f, 0.393f, 3f);
        spawnPosition[3] = new Vector3(-5f, 0.393f, -3f);

        spawnRotation[0] = Vector3.zero;
        spawnRotation[1] = new Vector3(0f, 180, 0f);
        spawnRotation[2] = new Vector3(0f, 180, 0f);
        spawnRotation[3] = Vector3.zero;

        //-- Provisório até criar sistema de entrada de jogadores.
        int i = 0;
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            pl.Character = characterList[i];
            pl.Character.CanControl = false;
            pl.Character.joystick = pl.PlayerId;
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
        int i = 3;
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            pl.Character.transform.position = spawnPosition[i];
            pl.Character.transform.eulerAngles = spawnRotation[i];
            pl.Character.health = 2;
            pl.Character.fsm.ChangeState("MOVEMENT");
            pl.Character.CanControl = true;
            i--;
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
        int healthyCount = 0;
        foreach (Player pl in PlayerManager.GetPlayerList())
        {
            if (pl.Character.health > 0)
            {
                healthyCount++;
            }
        }
        if (healthyCount <= 1)
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
