using UnityEngine;
using UniRx;

public class PlayerCarShopView : MonoBehaviour
{
	[SerializeField] eCarModel carModel;
	[SerializeField] eCarLevel carLevel;	

	public int GetCarModelID()
	{
		return (int)carModel;
	}

	private void Awake()
    {
		MessageBroker
			.Default
			.Receive<OnEraseCarMessage>()
			.Subscribe(message => {
				Destroy(gameObject);
			})
			.AddTo(this);
	}
}
