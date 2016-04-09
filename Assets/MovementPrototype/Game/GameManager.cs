using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState {
    Load,
    Intro,
    Play,
    End
}

public enum CharacterType {
    PunkSamuraiChick
}

public class GameManager : MonoBehaviour {

    public IList<CController> characterList = new List<CController>();
    public byte maxScore = 5;
    public float roundClock;
    //public Map currentMap;
    public GameState State { get; private set; }

    [SerializeField]
    GameObject characterPrefab;
    Timer timer = new Timer();

    void Awake() {
        EnterState(GameState.Load);
    }

    void Update() {
        StateMachine();
    }

    void StateMachine() {
        switch (State) {
            default:
            case GameState.Load:
                break;
            case GameState.Intro:
                break;
            case GameState.Play:
                RoundTimer();
                break;
            case GameState.End:
                break;
        }
    }

    void EnterState(GameState newState) {
        State = newState;
        switch (State) {
            default:
            case GameState.Load:
                LoadPlayers();
                break;
            case GameState.Intro:
                break;
            case GameState.Play:
                StartRound();
                break;
            case GameState.End:
                End();
                break;
        }
    }

    void LoadPlayers() {
        Vector3 position;
        foreach (Player pl in PlayerManager.GetPlayerList()) {
            position = Vector3.zero; //currentMap.GetSpawnPosition(type, i);
            SpawnCharacter(pl.Character, position);
            pl.Character.canControl = false;
        }
    }

    public GameObject SpawnCharacter(CController cController, Vector3 position) {
        //GameObject pl = (GameObject)Instantiate(GetCharacterPrefab(cController.type), position, transform.rotation);
        //characterList.Add(player.Character);
        return null;//pl;
    }

    public void RemoveCharacter(CController character) {

    }

    public void Score(PlayerIndex controller) {
        MatchData.PlayerScore[controller] += 1;
        if (MatchData.PlayerScore[controller] < maxScore) {
            StartNextRound();
        }
        else {
            EndMatch();
        }
    }

    void RoundTimer() {
        //if (timer.Run(currentMap.clockTime)) {
        //    StartNextRound();
        //}
        //roundClock = timer.GetTimeDecreasing();
    }

    void StartRound() {
        foreach (Player pl in PlayerManager.GetPlayerList()) {
            pl.Character.canControl = true;
        }
    }

    public void StartNextRound() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EndMatch() {
        EnterState(GameState.End);
    }

    void End() {
        SceneManager.LoadScene("ResultScreen");
    }

    GameObject GetCharacterPrefab(CharacterType type) {
        switch (type) {
            default:
            case CharacterType.PunkSamuraiChick:
                return null;
        }
    }
}
