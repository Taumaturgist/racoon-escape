using UnityEngine;
using UniRx;

public class PlayerCarShopView : MonoBehaviour
{
	[SerializeField] eCarModel carModel;
	[SerializeField] eCarLevel carLevel;
	[SerializeField] int carPurchasePrice;

	public int GetCarModelID()
	{
		return (int)carModel;
	}

	public string GetCarModelName()
    {
		return carModel.ToString();
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
