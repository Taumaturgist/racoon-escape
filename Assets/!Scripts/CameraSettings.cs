using UnityEngine;

public class CameraSettings : MonoBehaviour
{
	[SerializeField] private Transform objectToFollow;
	[SerializeField] private Vector3 offsetShoulderView;
	//[SerializeField] private Quaternion rotationShoulderView;

	[SerializeField] private float followSpeed;
	[SerializeField] private float lookSpeed;

	private void FixedUpdate()
	{
		LookAtTarget();
		MoveToTarget();
	}
	public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{
		Vector3 _targetPos = objectToFollow.position +
							 objectToFollow.forward * offsetShoulderView.z +
							 objectToFollow.right * offsetShoulderView.x +
							 objectToFollow.up * offsetShoulderView.y;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}
}
