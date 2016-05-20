//using UnityEngine;

//public enum ClipName
//{
//    Attack,
//    Hit,
//    Block
//}

//public static class AudioManager
//{
//    public static AudioClip attack1;
//    public static AudioClip attack2;
//    public static AudioClip attack3;
//    public static AudioClip hit;
//    public static AudioClip block1;
//    public static AudioClip block2;

//    static AudioManager() {
//        attack1 = Resources.Load("Audio/Sfx/Sword Swing 1") as AudioClip;
//        attack2 = Resources.Load("Audio/Sfx/Sword Swing 2") as AudioClip;
//        attack3 = Resources.Load("Audio/Sfx/Sword Swing 3") as AudioClip;
//        hit = Resources.Load("Audio/Sfx/Sword Hit") as AudioClip;
//        block1 = Resources.Load("Audio/Sfx/Sword Block 1") as AudioClip;
//        block2 = Resources.Load("Audio/Sfx/Sword Block 2") as AudioClip;
//    }

//    //public static void Play(ClipName name, AudioSource source)
//    //{
//    //    switch (name)
//    //    {
//    //        case ClipName.Attack:
//    //            PlayAttack(source);
//    //            break;
//    //        case ClipName.Hit:
//    //            PlayHit(source);
//    //            break;
//    //        case ClipName.Block:
//    //            PlayBlock(source);
//    //            break;
//    //    }
//    //}

//    static void PlayAttack(AudioSource source)
//    {
//        source.PlayOneShot(attack1);
//    }

//    static void PlayHit(AudioSource source)
//    {
//        source.PlayOneShot(hit);
//    }

//    static void PlayBlock(AudioSource source)
//    {
//        source.PlayOneShot(block1);
//    }
//}
