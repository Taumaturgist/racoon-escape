using UnityEngine;
using UniRx;

public readonly struct OnShopEnterCamSwitchMessage
{
	public readonly bool IsShopping;

	public OnShopEnterCamSwitchMessage(bool value)
    {
		IsShopping = value;
    }
}

public class CameraSettings : MonoBehaviour
{
	[SerializeField] private CameraSettingsConfig cameraSettingsConfig;
	[SerializeField] private float followSpeed;
	[SerializeField] private float lookSpeed;

	private Vector3 _offsetShoulderView;
	
	private Quaternion _camShopRotation;

	private Transform _objectToFollow;
	private Transform _cameraTransform;

	private bool _isShopMode;
	private bool _isTransitionComplete;

    public void Launch(Transform objectToFollow, int carID)
    {
		_objectToFollow = objectToFollow;
		_cameraTransform = transform;

		ChooseCameraTransform(ref carID);

		_camShopRotation = Quaternion.Euler(
			cameraSettingsConfig.CamShopData.rotation.x,
			cameraSettingsConfig.CamShopData.rotation.y,
			cameraSettingsConfig.CamShopData.rotation.z);

		MessageBroker
			.Default
			.Receive<OnShopEnterCamSwitchMessage>()
			.Subscribe(message =>
			{
				SetCameraShopMode(message.IsShopping);
				SetCameraMovementMode(!message.IsShopping);
			});
    }

    private void FixedUpdate()
	{
		if (!_isShopMode)
        {
			MoveToTarget();
			LookAtTarget();
		}
		else if (!_isTransitionComplete)
        {
			MoveToShopView();
        }
    }

	private void ChooseCameraTransform(ref int carID)
	{
        switch (carID)
        {
			default:
			case (int)eCarModel.ToyotaTundra:
                _offsetShoulderView = cameraSettingsConfig.CameraTransformDataSet[(int)eCarModel.ToyotaTundra].offsetShoulderView;
                break;
            case (int)eCarModel.LamborghiniHuracanLP700:
                _offsetShoulderView = cameraSettingsConfig.CameraTransformDataSet[(int)eCarModel.LamborghiniHuracanLP700].offsetShoulderView;
                break;
        }
    }

	private void MoveToTarget()
	{
		Vector3 _targetPos = _objectToFollow.position +
							 _objectToFollow.forward * _offsetShoulderView.z +
							 _objectToFollow.right * _offsetShoulderView.x +
							 _objectToFollow.up * _offsetShoulderView.y;
		_cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetPos, followSpeed * Time.fixedDeltaTime);
	}

	private void LookAtTarget()
	{
		var lookDirection = _objectToFollow.position - _cameraTransform.position;
		var rot = Quaternion.LookRotation(lookDirection, Vector3.up);
		rot.eulerAngles -= new Vector3(0f, rot.eulerAngles.y, 0f);
		_cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, rot, lookSpeed * Time.fixedDeltaTime);
	}

	private void MoveToShopView()
    {
		_cameraTransform.position = Vector3.MoveTowards(
			_cameraTransform.position, 
			cameraSettingsConfig.CamShopData.position, 
			cameraSettingsConfig.CamShopSwitchSpeed * Time.fixedDeltaTime);

		_cameraTransform.rotation = Quaternion.Slerp(
			_cameraTransform.rotation, 
			_camShopRotation,
			cameraSettingsConfig.CamShopSwitchSpeed * Time.fixedDeltaTime);

		if (_cameraTransform.position == cameraSettingsConfig.CamShopData.position)
        {
			_isTransitionComplete = true;
        }
	}

	private void SetCameraShopMode(bool value)
    {
		_isShopMode = value;		
    }

	private void SetCameraMovementMode(bool value)
    {
		_isTransitionComplete = value;
	}
}