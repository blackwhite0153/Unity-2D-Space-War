using System.Collections.Generic;
using UnityEngine;

// ���� ���ݿ��� ���Ǵ� BGM�� SFX�� �߾ӿ��� �����ϴ� AudioManager Ŭ����
public class AudioManager : Singleton<AudioManager>
{
    // �����(Background Music)�� �����ϴ� ��ųʸ�
    private Dictionary<string, AudioClip> _bgmDictionary;
    // ȿ����(Sound Effect)�� �����ϴ� ��ųʸ�
    private Dictionary<string, AudioClip> _sfxDictionary;

    [Header("Audio Sources")]
    // BGM ����� ���� AudioSource
    [SerializeField] private AudioSource _bgmSource;
    // SFX ����� ���� AudioSource
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    // �ν����Ϳ��� ���⸸ ������ BGM ����Ʈ
    [ShowOnly][SerializeField] private List<AudioClip> _bgmClips;
    // �ν����Ϳ��� ���⸸ ������ SFX ����Ʈ
    [ShowOnly][SerializeField] private List<AudioClip> _sfxClips;

    // ���� ������ BGM �� SFX ������ �����ֱ� ���� ����
    [ShowOnly][SerializeField] private float _currentBGMVolume;
    [ShowOnly][SerializeField] private float _currentSFXVolume;

    // �̱��� �ʱ�ȭ (��ӵ� Singleton���� �⺻ �ʱ�ȭ ����)
    protected override void Initialize()
    {
        base.Initialize();

        Setting();
    }

    // �ʱ�ȭ ����
    private void Setting()
    {
        _bgmDictionary = new Dictionary<string, AudioClip>();
        _sfxDictionary = new Dictionary<string, AudioClip>();

        _bgmClips = new List<AudioClip>();
        _sfxClips = new List<AudioClip>();

        // BGM ����� Audio Source�� ���ٸ� �ڵ����� ����
        if (_bgmSource == null)
        {
            _bgmSource = gameObject.AddComponent<AudioSource>();
            _bgmSource.playOnAwake = false;
            _bgmSource.loop = true; // �⺻������ BGM�� �ݺ� ���
        }

        // SFX ����� Audio Source�� ���ٸ� �ڵ����� ����
        if (_sfxSource == null)
        {
            _sfxSource = gameObject.AddComponent<AudioSource>();
            _sfxSource.playOnAwake = false; // ȿ������ ��� ����� ����
        }

        ResourceAllLoad();
    }

    // ��� ����� ���ҽ��� �ε�
    private void ResourceAllLoad()
    {
        // BGM Ŭ�� ���
        AudioClip arcadeGameClip = Resources.Load<AudioClip>(Define.ArcadeGamePath);
        AddBGM("Arcade Game", arcadeGameClip);

        // SFX Ŭ�� ���
        AudioClip laserClip = Resources.Load<AudioClip>(Define.LaserPath);
        AddSFX("Laser", laserClip);
    }

    // ������ �̸����� BGM�� ���
    public void AddBGM(string name, AudioClip clip)
    {
        // �ش� Ÿ���� ȿ������ ó�� �߰��� ��� (�ߺ� ����)
        if (!_bgmDictionary.ContainsKey(name))
        {
            _bgmDictionary.Add(name, clip);
            _bgmClips.Add(clip);
        }
    }

    // �̸��� �ش��ϴ� BGM ��� (�ɼ����� �ݺ� ���� ���� ����)
    public void PlayBGM(string name, bool isLoop = true)
    {
        if (_bgmDictionary.TryGetValue(name, out var clip))
        {
            // �̹� ������� BGM���� Ȯ��
            if (_bgmSource.clip == clip) return;

            _bgmSource.clip = clip;
            _bgmSource.loop = isLoop;
            _bgmSource.Play();
        }
    }

    // ���� ��� ���� BGM �Ͻ�����
    public void PauseOnBGM()
    {
        _bgmSource.Pause();
    }

    // ���� ��� ���� BGM �Ͻ����� ����
    public void PauseOffBGM()
    {
        _bgmSource.Play();
    }

    // ���� ��� ���� BGM ����
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    // BGM ���� ���� �� ���� ���� ����
    public void SetBGMVolume(float volume)
    {
        _bgmSource.volume = volume;
        _currentBGMVolume = volume;
    }

    // �̸��� �ش��ϴ� BGM ����
    public void RemoveBGM(string name)
    {
        if (_bgmDictionary.TryGetValue(name, out var clip))
        {
            _bgmDictionary.Remove(name);
            _bgmClips.Remove(clip);
        }
    }

    // ������ �̸����� SFX�� ���
    public void AddSFX(string name, AudioClip clip)
    {
        // �ش� Ÿ���� ȿ������ ó�� �߰��� ��� (�ߺ� ����)
        if (!_sfxDictionary.ContainsKey(name))
        {
            _sfxDictionary.Add(name, clip);
            _sfxClips.Add(clip);
        }
    }

    // �̸��� �ش��ϴ� SFX ��� (�� ���� �����)
    public void PlaySFX(string name)
    {
        if (_sfxDictionary.TryGetValue(name, out var clip))
        {
            _sfxSource.PlayOneShot(clip);
        }
    }

    // SFX ���� ���� �� ���� ���� ����
    public void SetSFXVolume(float volume)
    {
        _sfxSource.volume = volume;
        _currentSFXVolume = volume;
    }

    // �̸��� �ش��ϴ� SFX ����
    public void RemoveSFX(string name)
    {
        if (_sfxDictionary.TryGetValue(name, out var clip))
        {
            _sfxDictionary.Remove(name);
            _sfxClips.Remove(clip);
        }
    }

    // ��ü �������� ������Ʈ�� �����ϴ� �Լ�
    protected override void Clear()
    {
        base.Clear();

        _bgmDictionary.Clear(); // ��� BGM Ŭ�� ����
        _sfxDictionary.Clear(); // ��� SFX Ŭ�� ����

        _bgmClips.Clear();
        _sfxClips.Clear();
    }
}