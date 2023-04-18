using UnityEngine;

public class PlayerActiveCar : MonoBehaviour
{
	[SerializeField] private WheelCollider frontLeftW, frontRightW, rearLeftW, rearRightW;
	[SerializeField] private Transform frontLeftT, frontRightT, rearLeftT, rearRightT;
	[SerializeField] private GameObject stopLightLeft, stopLightRight;

	private Transform _carTransform;
	private Rigidbody _carRigidbody;

	private float _motorForce;
	private float _maxSteerAngle;
	private float _restoreDirectionSpeed;

	private float _horizontalInput;
	private float _steeringAngle;

	private float _spring;
	private float _damper;
	private float _targetPos;

	private bool _isActive = true;
	private bool _isAccelerating = true;
	[SerializeField] private bool _canUseBrakes;

	private float _speed;
	private float _speedLimit;
	private float _breakSpeedLimit;

	private float _currentRideDistance;
	private Vector3 _startPosition;

	public void Launch(PlayerAccountConfig playerAccountConfig)
	{
		_motorForce = playerAccountConfig.CarMotorForce;
		_maxSteerAngle = playerAccountConfig.MaxSteerAngle;
		_restoreDirectionSpeed = playerAccountConfig.RestoreDirectionSpeed;
		
		_carRigidbody = GetComponent<Rigidbody>();
		_carRigidbody.mass = playerAccountConfig.CarMass;

		SetSuspension(playerAccountConfig);

		_speedLimit = playerAccountConfig.CarMaxSpeed;
		_breakSpeedLimit = _speedLimit * (1 - playerAccountConfig.CarBreakPower / 100);
		_carTransform = GetComponent<Transform>();
		_startPosition = _carTransform.position;
	}

	public int GetSpeed()
    {
		return Mathf.RoundToInt(_speed);
    }

	public int GetCurrentRideDistance()
    {
		return Mathf.RoundToInt(_currentRideDistance);
    }
	private void FixedUpdate()
	{
		if (!_isActive)
        {
			return;
        }

		CheckBools();

		GetInput();
		Steer();
		RestoreCarOrientation();

		AccelerateAuto();
		UseBrakes(_canUseBrakes);

		UpdateWheelPoses();		

		_speed = Mathf.Abs(_carRigidbody.velocity.magnitude * 3.6f);
		_currentRideDistance = Vector3.Distance(_startPosition, _carTransform.position);
	}
	private void GetInput()
	{
		_horizontalInput = Input.GetAxis("Horizontal");
	}

	private void Steer()
	{
		_steeringAngle = _maxSteerAngle * _horizontalInput;
		frontLeftW.steerAngle = _steeringAngle;
		frontRightW.steerAngle = _steeringAngle;
	}
	private void RestoreCarOrientation()
	{
		if (_horizontalInput == 0)
		{
			var diffAngle = transform.rotation.y * _restoreDirectionSpeed;
			frontLeftW.steerAngle = -diffAngle;
			frontRightW.steerAngle = -diffAngle;
		}
	}

	private void AccelerateAuto()
	{
		var _currentMotorForce = _isAccelerating ? _motorForce : 0;

		frontLeftW.motorTorque = _currentMotorForce;
		frontRightW.motorTorque = _currentMotorForce;			
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontLeftW, frontLeftT);
		UpdateWheelPose(frontRightW, frontRightT);
		UpdateWheelPose(rearLeftW, rearLeftT);
		UpdateWheelPose(rearRightW, rearRightT);
	}

	private void UpdateWheelPose(WheelCollider collider, Transform transform)
	{
		var pos = transform.position;
		var quat = transform.rotation;

		collider.GetWorldPose(out pos, out quat);

		transform.position = pos;
		transform.rotation = quat;
	}

	private void SetSuspension(PlayerAccountConfig playerConfig)
	{
		_spring = playerConfig.Spring;
		_damper = playerConfig.Damper;
		_targetPos = playerConfig.TargetPosition;

		var wheelColliders = new[] { frontLeftW, frontRightW, rearLeftW, rearRightW };
		var suspensionSpring = new JointSpring();
		suspensionSpring.spring = _spring;
		suspensionSpring.damper = _damper;
		suspensionSpring.targetPosition = _targetPos;

		foreach (var wheelCollider in wheelColliders)
		{
			wheelCollider.wheelDampingRate = playerConfig.WheelDampingRate;
			wheelCollider.suspensionDistance = playerConfig.SuspensionDistance;
			wheelCollider.suspensionSpring = suspensionSpring;
		}
	}

	private void CheckBools()
    {
		if (_speed >= _speedLimit)
		{
			_isAccelerating = false;
		}
		else
		{
			_isAccelerating = true;
		}

		if (_speed >= _breakSpeedLimit)
		{
			_canUseBrakes = true;
		}
		else
		{
			_canUseBrakes = false;
		}	
	}	

	private void UseBrakes(bool canUseBrakes)
    {
		if (Input.GetKeyUp(KeyCode.Space))
		{
			stopLightLeft.SetActive(false);
			stopLightRight.SetActive(false);			
		}		

		if (Input.GetKey(KeyCode.Space) && canUseBrakes)
        {
			frontLeftW.brakeTorque = _motorForce * 10;
			frontRightW.brakeTorque = _motorForce * 10;

			stopLightLeft.SetActive(true);
			stopLightRight.SetActive(true);

			return;
		}

		frontLeftW.brakeTorque = 0;
		frontRightW.brakeTorque = 0;
	}
}
