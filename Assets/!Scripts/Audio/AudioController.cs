using UnityEngine;
using UniRx;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public readonly struct OnAmbientThemeSwitchMessage
{
    public readonly eBlockType BlockType;

    public OnAmbientThemeSwitchMessage(eBlockType blockType)
    {
        BlockType = blockType;
    }
}

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip ambientCity;
    [SerializeField] private AudioClip ambientForest;
    [SerializeField] private AudioClip ambientDesert;
    [SerializeField] private AudioClip ambientHighway;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volumeScaleForSounds;

    private AudioConfig _audioConfig;
    private PlayerView _playerView;


    private void Awake()
    {
        _audioConfig = Resources.Load<AudioConfig>("AudioConfig");

        MessageBroker
            .Default
            .Receive<OnGameStartMessage>()
            .Subscribe(message => { ChangeAmbientTrack(eBlockType.City); });

        MessageBroker
            .Default
            .Receive<OnAmbientThemeSwitchMessage>()
            .Subscribe(message => { ChangeAmbientTrack(message.BlockType); });
    }

    private void Start()
    {
        // todo: figure out how to post a link to PlayerView
        _playerView = FindFirstObjectByType<PlayerView>();
        
        _playerView.OnWoodKick.AddListener(PlayWoodKick);
        _playerView.OnMetalKick.AddListener(PlayMetalKick);
        _playerView.OnHeavyMetalKick.AddListener(PlayHeavyMetalKick);
        _playerView.OnRockKick.AddListener(PlayRockKick);
        _playerView.OnCactusCrashed.AddListener(PlayCactusCrashed);
    }

    private void PlayWoodKick()
    {
        audioSource.PlayOneShot(_audioConfig.WoodKick[Random.Range(0, _audioConfig.WoodKick.Length)], volumeScaleForSounds);
    }

    private void PlayMetalKick()
    {
        audioSource.PlayOneShot(_audioConfig.MetalKick[Random.Range(0, _audioConfig.MetalKick.Length)], volumeScaleForSounds);
    }

    private void PlayHeavyMetalKick()
    {
        audioSource.PlayOneShot(_audioConfig.HeavyMetalKick[Random.Range(0, _audioConfig.HeavyMetalKick.Length)], volumeScaleForSounds);
    }

    private void PlayRockKick()
    {
        audioSource.PlayOneShot(_audioConfig.RockKick[Random.Range(0, _audioConfig.RockKick.Length)], volumeScaleForSounds);
    }

    private void PlayCactusCrashed()
    {
        audioSource.PlayOneShot(_audioConfig.CactusCrush[Random.Range(0, _audioConfig.CactusCrush.Length)], volumeScaleForSounds);
    }

    private void ChangeAmbientTrack(eBlockType blockType)
    {
        audioSource.Stop();

        switch (blockType)
        {
            case eBlockType.City:
                audioSource.clip = ambientCity;
                break;
            case eBlockType.Forest:
                audioSource.clip = ambientForest;
                break;
            case eBlockType.Desert:
                audioSource.clip = ambientDesert;
                break;
            case eBlockType.Highway:
                audioSource.clip = ambientHighway;
                break;
        }

        Debug.Log($"Play now: {blockType} ambient");
        audioSource.Play();
    }
}