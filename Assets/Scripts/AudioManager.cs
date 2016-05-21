using UnityEngine;
using System.Collections.Generic;

public enum ClipType
{
    Attack,
    Hit,
    Block
}

public static class AudioManager
{
    static IList<AudioClip> attack = new List<AudioClip>();
    static IList<AudioClip> block = new List<AudioClip>();
    static AudioClip hit;
    static AudioClip hit2;

    static AudioManager()
    {
        attack.Add(Resources.Load("Audio/Sfx/Sword Swing 1") as AudioClip);
        attack.Add(Resources.Load("Audio/Sfx/Sword Swing 2") as AudioClip);
        attack.Add(Resources.Load("Audio/Sfx/Sword Swing 3") as AudioClip);
        block.Add(Resources.Load("Audio/Sfx/Sword Block 2") as AudioClip);
        block.Add(Resources.Load("Audio/Sfx/Sword Block 3") as AudioClip);
        hit = Resources.Load("Audio/Sfx/Sword Hit") as AudioClip;
        hit2 = Resources.Load("Audio/Sfx/Sword Hit 2") as AudioClip;
    }

    public static void Play(ClipType name, AudioSource source)
    {
        switch (name)
        {
            case ClipType.Attack:
                PlayAttack(source);
                break;
            case ClipType.Hit:
                PlayHit(source);
                break;
            case ClipType.Block:
                PlayBlock(source);
                break;
        }
    }

    static void PlayAttack(AudioSource source)
    {
        source.PlayOneShot(GetRandomClip(attack));
    }

    static void PlayBlock(AudioSource source)
    {
        source.PlayOneShot(GetRandomClip(block));
    }

    static void PlayHit(AudioSource source)
    {
        source.PlayOneShot(hit2);
    }

    static AudioClip GetRandomClip(IList<AudioClip> clip) {
        return clip[Random.Range(0, clip.Count)];
    }
}
