using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
    public UnityEvent OnWoodKick;
    public UnityEvent OnMetalKick;
    public UnityEvent OnHeavyMetalKick;
    public UnityEvent OnRockKick;
    public UnityEvent OnCactusCrashed;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.transform.gameObject.tag)
        {
            case "Wood":
                OnWoodKick.Invoke();
                break;
            case "Metal":
                OnMetalKick.Invoke();
                break;
            case "HeavyMetal":
                OnHeavyMetalKick.Invoke();
                break;
            case "Rock":
                OnRockKick.Invoke();
                break;
            case "Cactus":
                OnCactusCrashed.Invoke();
                break;
        }
    }
}