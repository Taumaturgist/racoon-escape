using UnityEngine;

public class PlayerActiveCar : MonoBehaviour
{
	[SerializeField] private WheelCollider frontLeftW, frontRightW, rearLeftW, rearRightW;
	[SerializeField] private Transform frontLeftT, frontRightT, rearLeftT, rearRightT;
	[SerializeField] private GameObject stopLightLeft, stopLightRight, nitroLight;
	
	private const float FullCircle = 360f;
	private const float HalfCircle = 180f;

	private Transform _carTransform;
	private Rigidbody _carRigidbody;

	private Vector3 _startPosition;	
	
	private float _motorForce;
	private float _currentMotorForce;
	private bool _isFrontWheelDriveOn;
	private bool _isRearWheelDriveOn;

	private float _maxSteerAngle;	

	private float _horizontalInput;
	private float _steeringAngle;
	private float _limitRotationAngleY;	

	private float _spring;
	private float _damper;
	private float _targetPos;

	private bool _isActive = true;
	private bool _isAccelerating = true;
	private bool _canUseBrakes;
	private bool _isNitroOn;

	private float _speed;
	private float _speedLimit;
	private float _breakSpeedLimit;
	private float _standardSpeedLimit;

	private float _currentRideDistance;

	private float _nitroCapacity;
	private float _currentNitroLevel;
	private float _nitroUsageSpeed;
	private float _nitroRestorationSpeed;
	private float _nitroSpeedBoost;
	private float _NitroTorqueBoost;

	public void Launch(PlayerAccountConfig playerAccountConfig)
	{		
		_maxSteerAngle = playerAccountConfig.MaxSteerAngle;		
		
		_carRigidbody = GetComponent<Rigidbody>();
		_carRigidbody.mass = playerAccountConfig.CarMass;
		_carRigidbody.centerOfMass = new Vector3(0, playerAccountConfig.CarMassCenterShiftY, 0);

		SetSuspension(playerAccountConfig);

		

		SetEngine(playerAccountConfig);		

		_standardSpeedLimit = playerAccountConfig.CarMaxSpeed;
		_speedLimit = _standardSpeedLimit;
		_breakSpeedLimit = _speedLimit * (1 - playerAccountConfig.CarBreakPower / 100);

		_carTransform = GetComponent<Transform>();
		_startPosition = _carTransform.position;

		_limitRotationAngleY = playerAccountConfig.LimitRotationY;
	}

	public int GetSpeed()
    {
		return Mathf.RoundToInt(_speed);
    }

	public int GetCurrentRideDistance()
    {
		return Mathf.RoundToInt(_currentRideDistance);
    }

	public int GetNitroCapacity()
    {
		return Mathf.RoundToInt(_currentNitroLevel);
	}

	private void FixedUpdate()
	{
		if (!_isActive)
        {
			return;
        }

		CheckBools();

		Steer();	
		AccelerateAuto();
		UseBrakes(_canUseBrakes);
		UseNitro();

		UpdateWheelPoses();		

		_speed = Mathf.Abs(_carRigidbody.velocity.magnitude * 3.6f);
		_currentRideDistance = Vector3.Distance(_startPosition, _carTransform.position);
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

	private void Steer()
	{
		_horizontalInput = Input.GetAxis("Horizontal");

		if (_horizontalInput == 0)
        {
			_steeringAngle = -transform.eulerAngles.y;
        }
		else
        {
			_steeringAngle = _maxSteerAngle * _horizontalInput;
		}		
		
		frontLeftW.steerAngle = _steeringAngle;
		frontRightW.steerAngle = _steeringAngle;

		if (_carTransform.eulerAngles.y > _limitRotationAngleY &&
			_carTransform.eulerAngles.y < HalfCircle)
		{
			_carTransform.eulerAngles = new Vector3(
				_carTransform.eulerAngles.x,
				_limitRotationAngleY,
				_carTransform.eulerAngles.z);

			_carRigidbody.angularVelocity = new Vector3(
				_carRigidbody.angularVelocity.x,
				_carRigidbody.angularVelocity.y,
				0);
		}
		else if (_carTransform.eulerAngles.y > HalfCircle &&
			_carTransform.eulerAngles.y < FullCircle - _limitRotationAngleY)
		{
			_carTransform.eulerAngles = new Vector3(
				_carTransform.eulerAngles.x,
				FullCircle - _limitRotationAngleY,
				_carTransform.eulerAngles.z);

			_carRigidbody.angularVelocity = new Vector3(
				_carRigidbody.angularVelocity.x,
				_carRigidbody.angularVelocity.y,
				0);
		}

		if (_carTransform.eulerAngles.y >= -1 && _carTransform.eulerAngles.y <= 1)
        {
			_carRigidbody.angularVelocity = new Vector3(
				_carRigidbody.angularVelocity.x,
				_carRigidbody.angularVelocity.y,
				0);
		}
	}	

	private void AccelerateAuto()
	{
		if (_isNitroOn)
        {
			_currentMotorForce = _isAccelerating ? _motorForce + _NitroTorqueBoost : 0;
		}
		else
        {
			_currentMotorForce = _isAccelerating ? _motorForce : 0;
		}

		if (_isFrontWheelDriveOn)
        {
			frontLeftW.motorTorque = _currentMotorForce;
			frontRightW.motorTorque = _currentMotorForce;
		}
		
		if (_isRearWheelDriveOn)
        {
			rearLeftW.motorTorque = _currentMotorForce;
			rearRightW.motorTorque = _currentMotorForce;
		}		
	}

	private void UseBrakes(bool canUseBrakes)
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			stopLightLeft.SetActive(false);
			stopLightRight.SetActive(false);
		}

		if (canUseBrakes)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				frontLeftW.brakeTorque = _motorForce * 10;
				frontRightW.brakeTorque = _motorForce * 10;

				stopLightLeft.SetActive(true);
				stopLightRight.SetActive(true);

				return;
			}
			else
			{
				stopLightLeft.SetActive(false);
				stopLightRight.SetActive(false);
			}
		}

		frontLeftW.brakeTorque = 0;
		frontRightW.brakeTorque = 0;
	}

	private void UseNitro()
    {
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			nitroLight.SetActive(false);
			_isNitroOn = false;
			_speedLimit = _standardSpeedLimit;
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (_currentNitroLevel <= 0)
            {
				nitroLight.SetActive(false);
				_isNitroOn = false;
				_speedLimit = _standardSpeedLimit;
				return;
            }

			nitroLight.SetActive(true);
			_isNitroOn = true;
			_speedLimit = _standardSpeedLimit + _nitroSpeedBoost;

			ConsumeNitro();

			return;
		}
		else
		{
			nitroLight.SetActive(false);
			_isNitroOn = false;
			_speedLimit = _standardSpeedLimit;
		}

		RestoreNitro();
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

	private void SetEngine(PlayerAccountConfig playerAccountConfig)
	{
		SetWheelsDrive(playerAccountConfig);
		SetNitro(playerAccountConfig);

		if (playerAccountConfig.FrontWheelDrive && playerAccountConfig.RearWheelDrive)
		{
			_motorForce = playerAccountConfig.CarMotorForce / 2;
			_NitroTorqueBoost = playerAccountConfig.NitroTorqueBoost / 2;
		}
		else
		{
			_motorForce = playerAccountConfig.CarMotorForce;
		}
	}
	private void SetWheelsDrive(PlayerAccountConfig playerAccountConfig)
	{
		_isFrontWheelDriveOn = playerAccountConfig.FrontWheelDrive;
		_isRearWheelDriveOn = playerAccountConfig.RearWheelDrive;
	}

	private void SetNitro(PlayerAccountConfig playerAccountConfig)
	{
		_nitroCapacity = playerAccountConfig.NitroCapacity;
		_currentNitroLevel = _nitroCapacity;
		_nitroUsageSpeed = playerAccountConfig.NitroUsageSpeed;
		_nitroRestorationSpeed = playerAccountConfig.NitroRestorationSpeed;
		_nitroSpeedBoost = playerAccountConfig.NitroSpeedBoost;
		_NitroTorqueBoost = playerAccountConfig.NitroTorqueBoost;
	}

	private void ConsumeNitro()
    {
		if (_isNitroOn)
        {
			_currentNitroLevel -= _nitroUsageSpeed * Time.deltaTime;

			if (_currentNitroLevel <= 0)
            {
				_currentNitroLevel = 0;
            }
        }
    }

	private void RestoreNitro()
    {
		if (!_isNitroOn)
        {
			_currentNitroLevel += _nitroRestorationSpeed * Time.deltaTime;

			if (_currentNitroLevel >= _nitroCapacity)
            {
				_currentNitroLevel = _nitroCapacity;
            }
        }
    }
}
