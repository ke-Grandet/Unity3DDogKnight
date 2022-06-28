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
    private readonly List<AudioSource> audioSourceList;  // AudioSource�����
    private int times = 0;

    private AudioManager()
    {
        audioSourceList = new();
        backgroundMusic = Main.Instance.gameObject.AddComponent<AudioSource>();
        backgroundMusic.loop = true;
        // ��ȡ�����ļ�
        config = ResourceManager.Instance.FindResourceFromJson<AudioConfig>(StringConfigPath.Audio_Config);
        clipArr = new AudioClip[config.ClipArr.Length];
        for (int i = 0; i < config.ClipArr.Length; i++)
        {
            clipArr[i] = ResourceManager.Instance.FindResource<AudioClip>(config.ClipArr[i].ClipPath);
        }
    }

    public void PlayBackgroundMusic(string clipName)
    {
        Debug.Log("���ű�������===" + clipName);
        backgroundMusic.clip = GetClipByName(clipName);
        backgroundMusic.Play();
    }

    public void PauseBackgroundMusic()
    {
        Debug.Log("��ͣ��������===");
        backgroundMusic.Pause();
    }

    public void StopBackgroundMusic()
    {
        Debug.Log("ֹͣ��������===");
        backgroundMusic.Stop();
    }

    public void PlayOneShot(string clipName)
    {
        //Debug.Log("������Ч===" + clipName);
        AudioSource audioSource = GetFreeAudioSource();
        AudioClip audioClip = GetClipByName(clipName);
        audioSource.PlayOneShot(audioClip);
    }

    public void Play(string clipName)
    {
        Debug.Log("��������===" + clipName);
        AudioSource audioSource = GetFreeAudioSource();
        AudioClip audioClip = GetClipByName(clipName);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void Stop(string clipName)
    {
        Debug.Log("ֹͣ��������===" + clipName);
        foreach (AudioSource audioSource in audioSourceList)
        {
            if (audioSource.isPlaying && audioSource.clip.name.Equals(clipName))
            {
                audioSource.Stop();
            }
        }
    }

    /// <summary>
    /// �����ƻ�ȡAudioClip����
    /// </summary>
    /// <param name="clipName">��Ƶ�������ļ��е�����</param>
    /// <returns>AudioClip����</returns>
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
    /// ��ȡһ�����е�AudioSource����
    /// </summary>
    /// <returns>AudioSource����</returns>
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
        if (times > 50)  // �̶�������ȡ֮���ͷ�һ�ζ������
        {
            ReleaseFreeAudioSource();
            times = 0;
        }
        return newSource;
    }

    /// <summary>
    /// �ͷŶ���AudioSource����
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
                if (count > 3)  // �����������ж���ʱ�ͷŶ�����ж���
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
