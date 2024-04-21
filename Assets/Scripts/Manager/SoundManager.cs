using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    public int bgmChannels;
    AudioSource[] bgmPlayers;
    int bgmChannelIndex;

    //[Header("#BGM")]
    //public AudioClip bgmClip;
    //public float bgmVolume;
    //public int bgmChannel;
    //AudioSource bgmPlayer;
    //int bgmChannelIndex;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int sfxChannels;
    AudioSource[] sfxPlayers;
    int sfxChannelIndex;
    public enum BGM
    {
        InGame, Lobby
    }
    public enum SFX
    {
        EnemyDeath, EnemyIdle, EnemyLockOn, EnemyShoot,
        PlayerColorSwap, PlayerDeath, PlayerGuard, PlayerGunOn,
        PlayerJam, PlayerReload, PlayerRun, PlayerShoot, PlayerswordOn, PlayerWalk
    }
    private void Start()
    {
        Init();
        //AudioManager.Instance.PlayBgm(true);
        //AudioManager.Instance.PlayBgm(AudioManager.BGM.InGame);
    }
    private void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayers = new AudioSource[bgmChannels];

        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            bgmPlayers[index] = bgmObject.AddComponent<AudioSource>();
            bgmPlayers[index].playOnAwake = false;
            bgmPlayers[index].loop = true;
            bgmPlayers[index].volume = bgmVolume;
            bgmPlayers[index].clip = bgmClips[index];
        }

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }
    public void PlayBGM(BGM bgm)
    {

        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            int loopIndex = (index + bgmChannelIndex) % bgmPlayers.Length;

            if (bgmPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            int ranIndex = 0;
            //if (bgm == BGM.Lobby)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            bgmChannelIndex = loopIndex;
            bgmPlayers[0].clip = bgmClips[(int)bgm + ranIndex];
            bgmPlayers[0].Play();
            Debug.Log("bgm");
            break;
        }
        //if (isPlay)
        //{
        //    bgmPlayer.Play();
        //}
        //else
        //{
        //    bgmPlayer.Stop();
        //}
    }
    public void StopBGM(BGM bgm)
    {

        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            int loopIndex = (index + bgmChannelIndex) % bgmPlayers.Length;

            if (bgmPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            int ranIndex = 0;
            //if (bgm == BGM.Lobby)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            bgmChannelIndex = loopIndex;
            bgmPlayers[0].clip = bgmClips[(int)bgm + ranIndex];
            bgmPlayers[0].Stop();
            Debug.Log("bgm");
            break;
        }

    }
    public void StopAllBGMs()
    {
        foreach (AudioSource audioSource in bgmPlayers)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }


    public void PlaySFX(SFX sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + sfxChannelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            int ranIndex = 0;
            //if (sfx == SFX.PlayerColorSwap)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            sfxChannelIndex = loopIndex;
            sfxPlayers[0].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[0].Play();
            break;
        }
    }
    public void StopSFX()
    {

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + sfxChannelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            //int ranIndex = 0;
            //if (bgm == BGM.Lobby)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            sfxChannelIndex = loopIndex;
            sfxPlayers[0].clip = sfxClips[(int)index];
            sfxPlayers[0].Stop();
            Debug.Log("bgm");
            break;
        }
    }
    public void StopAllSFXs()
    {
        foreach (AudioSource audioSource in sfxPlayers)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}





//public class SoundManager : Singleton<SoundManager>
//{
//    [SerializeField] AudioSource bgmSource;
//    [SerializeField] AudioSource sfxSource;

//    public float BGMVolme { get { return bgmSource.volume; } set { bgmSource.volume = value; } }
//    public float SFXVolme { get { return sfxSource.volume; } set { sfxSource.volume = value; } }

//    public void PlayBGM(AudioClip clip)
//    {
//        if (bgmSource.isPlaying)
//        {
//            bgmSource.Stop();
//        }
//        bgmSource.clip = clip;
//        bgmSource.Play();
//    }

//    public void StopBGM()
//    {
//        if (bgmSource.isPlaying == false)
//            return;

//        bgmSource.Stop();
//    }

//    public void PlaySFX(AudioClip clip)
//    {
//        sfxSource.PlayOneShot(clip);
//    }

//    public void StopSFX()
//    {
//        if (sfxSource.isPlaying == false)
//            return;

//        sfxSource.Stop();
//    }
//}
