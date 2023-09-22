using UnityEngine;

[CreateAssetMenu(menuName = "Configs/AudioConfig", fileName = "AudioConfig", order = 0)]
public class AudioConfig : ScriptableObject
{
    public AudioClip[] WoodKick;
    public AudioClip[] MetalKick;
    public AudioClip[] HeavyMetalKick;
    public AudioClip[] RockKick;
    public AudioClip[] CactusCrush;
}