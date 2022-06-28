using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ClipInfo
{
    public string ClipName;
    public string ClipPath;

}

[System.Serializable]
public class AudioConfig
{
    public ClipInfo[] ClipArr;
}

public class AudioManager : Singleton<AudioManager>
{
    private readonly AudioConfig config;
    private readonly AudioClip[] clipArr;
    private readonly AudioSource backgroundMusic;
    private readonly List<AudioSource> audioSourceList;  // AudioSource对象池
    private int times = 0;

    private AudioManager()
    {
        audioSourceList = new();
        backgroundMusic = Main.Instance.gameObject.AddComponent<AudioSource>();
        backgroundMusic.loop = true;
        // 读取配置文件
        config = ResourceManager.Instance.FindResourceFromJson<AudioConfig>(StringConfigPath.Audio_Config);
        clipArr = new AudioClip[config.ClipArr.Length];
        for (int i = 0; i < config.ClipArr.Length; i++)
        {
            clipArr[i] = ResourceManager.Instance.FindResource<AudioClip>(config.ClipArr[i].ClipPath);
        }
    }

    public void PlayBackgroundMusic(string clipName)
    {
        Debug.Log("播放背景音乐===" + clipName);
        backgroundMusic.clip = GetClipByName(clipName);
        backgroundMusic.Play();
    }

    public void PauseBackgroundMusic()
    {
        Debug.Log("暂停背景音乐===");
        backgroundMusic.Pause();
    }

    public void StopBackgroundMusic()
    {
        Debug.Log("停止背景音乐===");
        backgroundMusic.Stop();
    }

    public void PlayOneShot(string clipName)
    {
        //Debug.Log("播放音效===" + clipName);
        AudioSource audioSource = GetFreeAudioSource();
        AudioClip audioClip = GetClipByName(clipName);
        audioSource.PlayOneShot(audioClip);
    }

    public void Play(string clipName)
    {
        Debug.Log("播放声音===" + clipName);
        AudioSource audioSource = GetFreeAudioSource();
        AudioClip audioClip = GetClipByName(clipName);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void Stop(string clipName)
    {
        Debug.Log("停止播放声音===" + clipName);
        foreach (AudioSource audioSource in audioSourceList)
        {
            if (audioSource.isPlaying && audioSource.clip.name.Equals(clipName))
            {
                audioSource.Stop();
            }
        }
    }

    /// <summary>
    /// 按名称获取AudioClip对象
    /// </summary>
    /// <param name="clipName">音频在配置文件中的名称</param>
    /// <returns>AudioClip对象</returns>
    public AudioClip GetClipByName(string clipName)
    {
        for (int i = 0; i < config.ClipArr.Length; i++)
        {
            if (config.ClipArr[i].ClipName.Equals(clipName))
            {
                return clipArr[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 获取一个空闲的AudioSource对象
    /// </summary>
    /// <returns>AudioSource对象</returns>
    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource audioSource in audioSourceList)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        AudioSource newSource = Main.Instance.gameObject.AddComponent<AudioSource>();
        audioSourceList.Add(newSource);
        times++;
        if (times > 50)  // 固定次数获取之后释放一次多余对象
        {
            ReleaseFreeAudioSource();
            times = 0;
        }
        return newSource;
    }

    /// <summary>
    /// 释放多余AudioSource对象
    /// </summary>
    private void ReleaseFreeAudioSource()
    {
        int count = 0;
        List<AudioSource> toRemoveList = new();
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            if (!audioSourceList[i].isPlaying)
            {
                count++;
                if (count > 3)  // 超过三个空闲对象时释放多余空闲对象
                {
                    toRemoveList.Add(audioSourceList[i]);
                }
            }
        }
        if (toRemoveList.Count > 0)
        {
            foreach (AudioSource audioSource in toRemoveList)
            {
                audioSourceList.Remove(audioSource);
                Object.Destroy(audioSource);
            }
        }
        toRemoveList.Clear();
    }
}
