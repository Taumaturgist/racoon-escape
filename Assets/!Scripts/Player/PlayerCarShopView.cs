using UnityEngine;
using UniRx;

public class PlayerCarShopView : MonoBehaviour
{
	[SerializeField] private eCarModel carModel;
	[SerializeField] private eCarLevel carLevel;
	[SerializeField] private int carPurchasePrice;

	public int GetCarModelID()
	{
		return (int)carModel;
	}

	public eCarModel GetCarModel()
    {
		return carModel;
    }

	public eCarLevel GetCarLevel()
    {
		return carLevel;
    }

	public int GetCarPurchasePrice()
    {
		return carPurchasePrice;
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