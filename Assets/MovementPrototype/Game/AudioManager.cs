using UnityEngine;
using System.Collections.Generic;

public enum ClipType
{
    Attack,
    Hit,
    Block,
    Footsteps,
    Dash,
    Hurt,
    Dead,
    ArenaEnvironment,
    MenuBGM,
    ArenaBGM,
    PlayerJoinBGM,
    GUICursor,
    GUIPlayerJoin,
    GUIStartGame
}

public static class AudioManager
{
    static IList<AudioClip> swing = new List<AudioClip>();
    static IList<AudioClip> swingMetal = new List<AudioClip>();
    static IList<AudioClip> block = new List<AudioClip>();
    static IList<AudioClip> hit = new List<AudioClip>();
    static IList<AudioClip> footSteps = new List<AudioClip>();
    static IList<AudioClip> dash = new List<AudioClip>();
    static IList<AudioClip> femaleHurt = new List<AudioClip>();
    static IList<AudioClip> femaleDead = new List<AudioClip>();
    static IList<AudioClip> femaleAttacking = new List<AudioClip>();

    static AudioClip arenaEnvironment;

    static AudioClip arenaBGM;
    static AudioClip menuBGM;
    static AudioClip playerJoinBGM;

    static AudioClip UICursor;
    static AudioClip UIPlayerJoin;
    static AudioClip UIStartGame;

    static AudioManager()
    {
        swing.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing 1") as AudioClip);
        swing.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing 2") as AudioClip);
        swing.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing 3") as AudioClip);

        block.Add(Resources.Load("Audio/Sfx/Sword/Sword Block 1") as AudioClip);
        block.Add(Resources.Load("Audio/Sfx/Sword/Sword Block 2") as AudioClip);

        hit.Add(Resources.Load("Audio/Sfx/Sword/Sword Hit 1") as AudioClip);
        //hit.Add(Resources.Load("Audio/Sfx/Sword/Sword Hit 2") as AudioClip); ANTIGO!
        hit.Add(Resources.Load("Audio/Sfx/Sword/Sword Hit 3") as AudioClip);
        hit.Add(Resources.Load("Audio/Sfx/Sword/Sword Hit 4") as AudioClip);
        hit.Add(Resources.Load("Audio/Sfx/Sword/Sword Hit 5") as AudioClip);

        swingMetal.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing&Metal 1") as AudioClip);
        swingMetal.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing&Metal 2") as AudioClip);
        swingMetal.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing&Metal 3") as AudioClip);

        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 1") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 2") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 3") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 4") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 5") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 6") as AudioClip);

        dash.Add(Resources.Load("Audio/Sfx/Dash/Dash 1") as AudioClip);
        dash.Add(Resources.Load("Audio/Sfx/Dash/Dash 2") as AudioClip);
        dash.Add(Resources.Load("Audio/Sfx/Dash/Dash 3") as AudioClip);
        dash.Add(Resources.Load("Audio/Sfx/Dash/Dash 4") as AudioClip);

        femaleHurt.Add(Resources.Load("Audio/Sfx/Female/Female Hurt 1") as AudioClip);
        femaleHurt.Add(Resources.Load("Audio/Sfx/Female/Female Hurt 2") as AudioClip);
        femaleHurt.Add(Resources.Load("Audio/Sfx/Female/Female Hurt 3") as AudioClip);
        femaleHurt.Add(Resources.Load("Audio/Sfx/Female/Female Hurt 4") as AudioClip);

        femaleDead.Add(Resources.Load("Audio/Sfx/Female/Female Dead 1") as AudioClip);
        femaleDead.Add(Resources.Load("Audio/Sfx/Female/Female Dead 2") as AudioClip);

        femaleAttacking.Add(Resources.Load("Audio/Sfx/Female/Female Attacking 1") as AudioClip);
        femaleAttacking.Add(Resources.Load("Audio/Sfx/Female/Female Attacking 2") as AudioClip);
        femaleAttacking.Add(Resources.Load("Audio/Sfx/Female/Female Attacking 3") as AudioClip);
        femaleAttacking.Add(Resources.Load("Audio/Sfx/Female/Female Attacking 4") as AudioClip);


        arenaEnvironment = Resources.Load("Audio/Sfx/Arena Ambient Wind") as AudioClip;

        menuBGM = Resources.Load("Audio/Music/Menu Intro") as AudioClip;
        arenaBGM = Resources.Load("Audio/Music/Arena BGM") as AudioClip;
        playerJoinBGM = Resources.Load("Audio/Music/Menu ErHu") as AudioClip;

        UICursor = Resources.Load("Audio/Sfx/UI/GUI Cursor") as AudioClip;
        UIPlayerJoin = Resources.Load("Audio/Sfx/UI/GUI Player Join") as AudioClip;
        UIStartGame = Resources.Load("Audio/Sfx/UI/GUI Start Game") as AudioClip;
    }

    public static void Play(ClipType name, AudioSource source)
    {
        switch (name)
        {
            case ClipType.Attack:
                source.PlayOneShot(GetRandomClip(swingMetal));
                AudioClip clip = MaybeGetRandomClip(femaleAttacking);
                if (clip) source.PlayOneShot(clip);
                break;
            case ClipType.Hit:
                source.PlayOneShot(GetRandomClip(hit));
                break;
            case ClipType.Block:
                source.PlayOneShot(GetRandomClip(block));
                break;
            case ClipType.Footsteps:
                source.PlayOneShot(GetRandomClip(footSteps));
                break;
            case ClipType.Dash:
                source.PlayOneShot(GetRandomClip(dash));
                break;
            case ClipType.Hurt:
                source.PlayOneShot(GetRandomClip(femaleHurt));
                break;
            case ClipType.Dead:
                source.PlayOneShot(GetRandomClip(femaleDead));
                break;
            case ClipType.ArenaEnvironment:
                PlayLooping(arenaEnvironment, source);
                source.volume = 0.2f;
                break;
            case ClipType.MenuBGM:
                PlayLooping(menuBGM, source);
                source.volume = 0.5f;
                GetMenuVolume(source, "Music");
                break;
            case ClipType.ArenaBGM:
                PlayLooping(arenaBGM, source);
                source.volume = 0.8f;
                GetMenuVolume(source, "Music");
                break;
            case ClipType.GUICursor:
                source.PlayOneShot(UICursor);
                break;
            case ClipType.GUIPlayerJoin:
                source.PlayOneShot(UIPlayerJoin);
                break;
            case ClipType.GUIStartGame:
                source.PlayOneShot(UIStartGame);
                break;
        }
    }

    static void PlayLooping(AudioClip clip, AudioSource source)
    {
        source.loop = true;
        source.clip = clip;
        source.Play();
    }

    static AudioClip GetRandomClip(IList<AudioClip> clip)
    {
        return clip[Random.Range(0, clip.Count)];
    }

    static AudioClip MaybeGetRandomClip(IList<AudioClip> clip)
    {
        int rnd = Random.Range(0, 11);
        if (rnd > 6)
        {
            return clip[Random.Range(0, clip.Count)];
        }
        else
        {
            return null;
        }
    }

    static void GetMenuVolume(AudioSource source, string prefKey) {
        source.volume *= ((float)PlayerPrefs.GetInt(prefKey)) * 0.01f;
        UnityEngine.Debug.Log(source.volume + " " + ((float)PlayerPrefs.GetInt(prefKey)) * 0.01f);
    }
}
