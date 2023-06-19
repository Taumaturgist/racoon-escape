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
            case 0:
                _offsetShoulderView = cameraSettingsConfig.CameraTransformDataSet[0].offsetShoulderView;
                break;
            case 1:
                _offsetShoulderView = cameraSettingsConfig.CameraTransformDataSet[1].offsetShoulderView;
                break;
            case 2:
                _offsetShoulderView = cameraSettingsConfig.CameraTransformDataSet[2].offsetShoulderView;
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
		Vector3 _lookDirection = _objectToFollow.position - _cameraTransform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		_cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, _rot, lookSpeed * Time.fixedDeltaTime);
	}
}