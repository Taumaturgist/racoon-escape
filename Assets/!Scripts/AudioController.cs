using UnityEngine;
using UniRx;

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

    private void Awake()
    {
        MessageBroker
            .Default
            .Receive<OnGameStartMessage>()
            .Subscribe(message =>
            {
                ChangeAmbientTrack(eBlockType.City);
            });

        MessageBroker
            .Default
            .Receive<OnAmbientThemeSwitchMessage>()
            .Subscribe(message =>
            {
                ChangeAmbientTrack(message.BlockType);
            });
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
