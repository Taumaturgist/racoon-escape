using UnityEngine;

public class CameraSettings : MonoBehaviour
{
	private GameObject _objectToFollow;
	[SerializeField] private Vector3 offsetShoulderView;
	[SerializeField] private Quaternion rotationShoulderView;

	[SerializeField] private float followSpeed;
	[SerializeField] private float lookSpeed;

    private void Awake()
    {
		_objectToFollow = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
	{
		MoveToTarget();
	}

	public void MoveToTarget()
	{
		Vector3 _targetPos = _objectToFollow.transform.position +
							 _objectToFollow.transform.forward * offsetShoulderView.z +
							 _objectToFollow.transform.right * offsetShoulderView.x +
							 _objectToFollow.transform.up * offsetShoulderView.y;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.fixedDeltaTime);
	}
	//public void LookAtTarget()
	//{
	//	Vector3 _lookDirection = _objectToFollow.transform.position - transform.position;
	//	Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
	//	transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.fixedDeltaTime);
	//}
}
