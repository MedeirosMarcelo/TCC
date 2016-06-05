using UnityEngine;
using System.Collections.Generic;

public enum ClipType
{
    Attack,
    Hit,
    Block,
    Footsteps,
    ArenaEnvironment,
    MenuBGM,
    ArenaBGM
}

public static class AudioManager
{
    static IList<AudioClip> attack = new List<AudioClip>();
    static IList<AudioClip> block = new List<AudioClip>();
    static AudioClip hit;
    static AudioClip hit2;
    static IList<AudioClip> footSteps = new List<AudioClip>();

    static AudioClip arenaEnvironment;

    static AudioClip arenaBGM;
    static AudioClip menuBGM;

    static AudioClip UICursorMove;
    static AudioClip UICursorSelection;
    static AudioClip UIMenuStart;
    static AudioClip UIPlayerJoined;
    static AudioClip UIGameStart;

    static AudioManager()
    {
        attack.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing 1") as AudioClip);
        attack.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing 2") as AudioClip);
        attack.Add(Resources.Load("Audio/Sfx/Sword/Sword Swing 3") as AudioClip);
        block.Add(Resources.Load("Audio/Sfx/Sword/Sword Block 2") as AudioClip);
        block.Add(Resources.Load("Audio/Sfx/Sword/Sword Block 3") as AudioClip);
        hit = Resources.Load("Audio/Sfx/Sword/Sword Hit") as AudioClip;
        hit2 = Resources.Load("Audio/Sfx/Sword/Sword Hit 2") as AudioClip;
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 1") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 2") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 3") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 4") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 5") as AudioClip);
        footSteps.Add(Resources.Load("Audio/Sfx/Footsteps/Footsteps 6") as AudioClip);

        arenaEnvironment = Resources.Load("Audio/Sfx/Environmental - Wind") as AudioClip;

        menuBGM = Resources.Load("Audio/Music/phat-Perc09-05-140") as AudioClip;
        arenaBGM = Resources.Load("Audio/Music/phat-Perc09-05-140") as AudioClip;

        UICursorMove = Resources.Load("Audio/Sfx/UI/Cursor Move") as AudioClip;
        UICursorSelection = Resources.Load("Audio/Sfx/UI/Cursor Selection") as AudioClip;
        UIMenuStart = Resources.Load("Audio/Sfx/UI/Menu Start") as AudioClip;
        UIPlayerJoined = Resources.Load("Audio/Sfx/UI/Player Joined") as AudioClip;
        UIGameStart = Resources.Load("Audio/Sfx/UI/Game Start") as AudioClip;
    }

    public static void Play(ClipType name, AudioSource source)
    {
        switch (name)
        {
            case ClipType.Attack:
                source.PlayOneShot(GetRandomClip(attack));
                break;
            case ClipType.Hit:
                source.PlayOneShot(hit2);
                break;
            case ClipType.Block:
                source.PlayOneShot(GetRandomClip(block));
                break;
            case ClipType.Footsteps:
                source.PlayOneShot(GetRandomClip(footSteps));
                break;
            case ClipType.ArenaEnvironment:
                source.volume = 0.2f;
                PlayLooping(arenaEnvironment, source);
                break;
            case ClipType.MenuBGM:
                source.volume = 0.5f;
                PlayLooping(menuBGM, source);
                break;
            case ClipType.ArenaBGM:
                source.volume = 0.5f;
                PlayLooping(arenaBGM, source);
                break;
        }
    }

    //static void PlayAttack(AudioSource source)
    //{
    //    source.PlayOneShot(GetRandomClip(attack));
    //}

    //static void PlayBlock(AudioSource source)
    //{
    //    source.PlayOneShot(GetRandomClip(block));
    //}

    //static void PlayHit(AudioSource source)
    //{
    //    source.PlayOneShot(hit2);
    //}

    //static void PlayArenaEnvironment(AudioSource source)
    //{
    //    source.loop = true;
    //    source.PlayOneShot(arenaEnviroment);
    //}

    static void PlayLooping(AudioClip clip, AudioSource source)
    {
        source.loop = true;
        source.volume = 0.2f;
        source.clip = clip;
        source.Play();
    }

    static AudioClip GetRandomClip(IList<AudioClip> clip) {
        return clip[Random.Range(0, clip.Count)];
    }
}
