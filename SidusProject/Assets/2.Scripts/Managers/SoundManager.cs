using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�� ���ӿ����� �Ÿ��� ����� ������ ũ�⸦ ������ �ʿ䰡 ���⿡ �ϳ��� AudioSource�� AudioClip���� �������� �����ų ���̴�.
//��������� ������ AudioSource�� ȿ������ ������ AudioSource�� SoundManager�� �ڽ� ������Ʈ�� ����
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SoundManager>();
            return instance;
        }
    } // Sound�� �������ִ� ��ũ��Ʈ�� �ϳ��� �����ؾ��ϰ� instance������Ƽ�� ���� ��𿡼��� �ҷ��������� �̱��� ���

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;
    [SerializeField]
    private AudioClip LobbyBgmAudioClip; // �κ񿡼� ����� BGM
    [SerializeField]
    private AudioClip TalkBgmAudioClip; // �ΰ��ӿ��� ����� BGM
    //[SerializeField]
    //private AudioClip loadingBgmAudioClip; // �ε������� ����� BGM
    //[SerializeField]
    //private AudioClip nightBgmAudioClip; // ����ʿ��� ����� BGM
    //[SerializeField]
    //private AudioClip chaosBgmAudioClip; // ī�����ʿ��� ����� BGM
    //[SerializeField]
    //private AudioClip BossBgmAudioClip; // ī�����ʿ��� ����� BGM
    //[SerializeField]
    //private AudioClip CreditAudioClip; // ũ����â���� ����� BGM

    [SerializeField]
    private AudioClip[] sfxAudioClips; //ȿ������ ����

    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>(); //ȿ���� ��ųʸ�
    // AudioClip�� Key,Value ���·� �����ϱ� ���� ��ųʸ� ���

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(this.gameObject); //���� ������ ����� ��.

        SceneManager.sceneLoaded += OnSceneLoaded;

        bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();

        foreach (AudioClip audioclip in sfxAudioClips)
        {
            audioClipsDic.Add(audioclip.name, audioclip);
        }

        if (!PlayerPrefs.HasKey("BGMVolume"))
            PlayerPrefs.SetFloat("BGMVolume", 1f);

        if (!PlayerPrefs.HasKey("SFXVolume"))
            PlayerPrefs.SetFloat("SFXVolume", 1f);

        if (!PlayerPrefs.HasKey("BGMMute"))
            PlayerPrefs.SetInt("BGMMute", System.Convert.ToInt16(false));

        if (!PlayerPrefs.HasKey("SFXMute"))
            PlayerPrefs.SetInt("SFXMute", System.Convert.ToInt16(false));

        SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        bgmPlayer.mute = System.Convert.ToBoolean(PlayerPrefs.GetInt("BGMMute"));
        sfxPlayer.mute = System.Convert.ToBoolean(PlayerPrefs.GetInt("SFXMute"));
        masterVolumeBGM = PlayerPrefs.GetFloat("BGMVolume");
        masterVolumeSFX = PlayerPrefs.GetFloat("SFXVolume");
    }

    // ȿ�� ���� ��� : �̸��� �ʼ� �Ű�����, ������ ������ �Ű������� ����
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    public void PlaySFXSound(AudioSource parent, string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
            return;
        parent.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }

    public void PlaySFXSoundLoop(string name, Transform parent, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
            return;
        
        AudioSource audioSource = Instantiate(Resources.Load("SFXSoundPlayer") as GameObject, parent).GetComponent<AudioSource>();
        audioSource.loop = true; 
        audioSource.clip = audioClipsDic[name];
        audioSource.volume = volume * masterVolumeSFX;
        audioSource.Play();
    }

    //BGM ���� ��� : ������ ������ �Ű������� ����
    public void PlayBGMSound(Scene scene ,float volume = 1f)
    {
        bgmPlayer.loop = true; //BGM �����̹Ƿ� ��������
        //bgmPlayer.volume = volume * masterVolumeBGM;
        if (scene.name == "2.Lobby" || scene.name == "1.Start")
        {
            bgmPlayer.clip = LobbyBgmAudioClip;
            bgmPlayer.Play();
        }
        else if (scene.name == "4.Talk")
        {
            bgmPlayer.clip = TalkBgmAudioClip;
            bgmPlayer.Play();
        }
        //else if (SceneManager.GetActiveScene().name == "LoadingScene")
        //{
        //    bgmPlayer.clip = loadingBgmAudioClip;
        //    bgmPlayer.Play();
        //}
        //���� ���� �´� BGM ���
    }
    public void PlayCoustomBGM(string name)
    {
        AudioClip playClip = null;
        //switch (name)
        //{
        //    case "Boss":
        //        playClip = BossBgmAudioClip;
        //        break;
        //    case "NightStage":
        //        playClip = nightBgmAudioClip;
        //        break;
        //    case "ChaosStage":
        //        playClip = chaosBgmAudioClip;
        //        break;
        //    case "Credit":
        //        playClip = CreditAudioClip;
        //        break;
        //    case "Menu":
        //        playClip = menuBgmAudioClip;
        //        break;
        //}
        bgmPlayer.clip = playClip;
        bgmPlayer.Play();
    }
    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfxPlayer.volume = volume;
    }
    public void SetBGMMute(bool mute)
    {
        bgmPlayer.mute = mute;
    }
    public void SetSFXMute(bool mute)
    {
        sfxPlayer.mute = mute;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ���� �ε��ɶ� ����
    {
        PlayBGMSound(scene);
    }

    public void PlayUISound()
    {
        if (audioClipsDic.ContainsKey("ClickSound") == false)
        {
            Debug.Log("ClickSound" + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic["ClickSound"], masterVolumeSFX);
    }
}