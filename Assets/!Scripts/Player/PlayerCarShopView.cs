using UnityEngine;
using UniRx;

public class PlayerCarShopView : MonoBehaviour
{
	[SerializeField] private int carModelID;

	public int GetCarModelID()
	{
		return carModelID;
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
