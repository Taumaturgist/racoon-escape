using UnityEngine;

public class CameraSettings : MonoBehaviour
{
	[SerializeField] private Vector3 offsetShoulderView;
	[SerializeField] private Quaternion rotationShoulderView;

	[SerializeField] private float followSpeed;
	[SerializeField] private float lookSpeed;

	private Transform _objectToFollow;
	private Transform _cameraTransform;

    public void Launch(Transform objectToFollow, int carID)
    {
		_objectToFollow = objectToFollow;
		_cameraTransform = transform;

		switch (carID)
		{
			case 0:
				offsetShoulderView = new Vector3(0f, 1.3f, -3.35f);
				break;
			case 1:
				offsetShoulderView = new Vector3(0f, 1.4f, -3.35f);
				break;
			case 2:
				offsetShoulderView = new Vector3(0f, 1.1f, -3.35f);
				break;
			default:
				offsetShoulderView = new Vector3(0f, 2f, -5f);
				break;
		}
	}

    private void FixedUpdate()
	{
		MoveToTarget();
		LookAtTarget();
    }

	public void MoveToTarget()
	{
		Vector3 _targetPos = _objectToFollow.position +
							 _objectToFollow.forward * offsetShoulderView.z +
							 _objectToFollow.right * offsetShoulderView.x +
							 _objectToFollow.up * offsetShoulderView.y;
		_cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetPos, followSpeed * Time.fixedDeltaTime);
	}
	public void LookAtTarget()
	{
		Vector3 _lookDirection = _objectToFollow.position - _cameraTransform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		_cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, _rot, lookSpeed * Time.fixedDeltaTime);
	}
}
