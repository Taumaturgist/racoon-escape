using UnityEngine;

public class CameraSettings : MonoBehaviour
{
	[SerializeField] private CameraSettingsConfig cameraSettingsConfig;
	[SerializeField] private float followSpeed;
	[SerializeField] private float lookSpeed;

	private Vector3 _offsetShoulderView;
	private Quaternion _rotationShoulderView;

	private Transform _objectToFollow;
	private Transform _cameraTransform;

    public void Launch(Transform objectToFollow, int carID)
    {
		_objectToFollow = objectToFollow;
		_cameraTransform = transform;

		ChooseCameraTransform(ref carID);
    }

    private void FixedUpdate()
	{
		MoveToTarget();
		LookAtTarget();
    }

	private void ChooseCameraTransform(ref int carID)
	{
        switch (carID)
        {
            case (int)eCarModel.BasicPickUp:
                _offsetShoulderView = cameraSettingsConfig.CameraTransformDataSet[(int)eCarModel.BasicPickUp].offsetShoulderView;
                break;
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
		_cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, rot, lookSpeed * Time.fixedDeltaTime);
	}
}