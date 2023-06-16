using UnityEngine;

public class CameraSettings : MonoBehaviour
{
	[SerializeField] private Vector3 offsetShoulderView;
	[SerializeField] private Quaternion rotationShoulderView;

	[SerializeField] private float followSpeed;
	[SerializeField] private float lookSpeed;

	private Transform _objectToFollow;
	private Transform _cameraTransform;

    public void Launch(Transform objectToFollow)
    {
		_objectToFollow = objectToFollow;
		_cameraTransform = transform;
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
