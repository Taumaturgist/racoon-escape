using UnityEngine;
using UniRx;

public class PlayerActiveCar : MonoBehaviour
{
	[SerializeField] private WheelCollider frontLeftW, frontRightW, rearLeftW, rearRightW;
	[SerializeField] private Transform frontLeftT, frontRightT, rearLeftT, rearRightT;
	[SerializeField] private GameObject stopLightLeft, stopLightRight, nitroLight;
	[SerializeField] private PlayerActiveCarConfig carConfig;
	
	private const float FullCircle = 360f;
	private const float HalfCircle = 180f;

	private Game _game;

	private Transform _carTransform;
	private Rigidbody _carRigidbody;
	private Collider _carCollider;

	private Vector3 _startPosition;
	
	private float _motorForce;
	private float _currentMotorForce;
	private bool _isFrontWheelDriveOn;
	private bool _isRearWheelDriveOn;

	private float _maxSteerAngle;	

	private float _horizontalInput;
	private float _steeringAngle;
	private float _limitRotationAngleY;	

	private bool _isActive;
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

	private float _defeatActivationSpeed;
	private float _defeatConditionSpeed;
	private bool _canLose;
	private bool _isLossConditionActivated;
	private bool _hasLost;

	public void Launch(Game game)
	{
		_carCollider = GetComponent<Collider>();
		
		_game = game;

		_maxSteerAngle = carConfig.MaxSteerAngle;
		
		_carRigidbody = GetComponent<Rigidbody>();
		_carRigidbody.mass = carConfig.CarMass;
		_carRigidbody.angularDrag = carConfig.CarAngularDrug;
		_carRigidbody.centerOfMass = new Vector3(0, carConfig.CarMassCenterShiftY, 0);

		SetSuspension();
		SetEngine();

		_standardSpeedLimit = carConfig.CarMaxSpeed;
		_speedLimit = _standardSpeedLimit;
		_breakSpeedLimit = _speedLimit * (1 - carConfig.CarBreakPower / 100);

		_carTransform = GetComponent<Transform>();
		_startPosition = _carTransform.position;

		_limitRotationAngleY = carConfig.LimitRotationY;

		_defeatActivationSpeed = carConfig.DefeatActivationSpeed;
		_defeatConditionSpeed = carConfig.DefeatConditionSpeed;

		SetPhysics(true);

		_isActive = true;
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

	public void SetPhysics(bool value)
    {
		_carCollider.isTrigger = !value;
		_carRigidbody.useGravity = value;
	}

	public void FreezeRotation(bool value)
    {
		_carRigidbody.freezeRotation = value;
    }
	
	private void FixedUpdate()
	{
		CheckConditions();

		if (!_isActive)
		{
			return;
		}

		Steer();
		AccelerateAuto();
		UseBrakes(_canUseBrakes);
		UseNitro();

		UpdateWheelPoses();

		_speed = Mathf.Abs(_carRigidbody.velocity.magnitude * 3.6f);
		_currentRideDistance = Vector3.Distance(_startPosition, _carTransform.position);
	}

	private void CheckConditions()
	{
		switch(_game.GetGameState())
        {
			case GameState.Pause:
			case GameState.Defeat:
				_isActive = false;
				_canLose = false;
				_isLossConditionActivated = false;
				//SetPhysics(false);
				break;
			case GameState.Action:
				_isActive = true;
				_hasLost = false;
				SetPhysics(true);
				break;
        }

		if (_hasLost)
        {
			_carRigidbody.velocity = Vector3.zero;
		}

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

		if (!_isLossConditionActivated)
        {
			if (_speed >= _defeatActivationSpeed)
            {
				_canLose = true;
				_isLossConditionActivated = true;
			}
        }

		if (_canLose)
        {
			if (_speed <= _defeatConditionSpeed)
            {
				MessageBroker
					.Default
					.Publish(new OnPlayerDefeatedMessage());
				_hasLost = true;
			}
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
		if (!_isActive)
		{
			return;
		}

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

	private void SetSuspension()
	{
		var wheelColliders = new[] { frontLeftW, frontRightW, rearLeftW, rearRightW };
		var suspensionSpring = new JointSpring();
		suspensionSpring.spring = carConfig.Spring;
		suspensionSpring.damper = carConfig.Damper;
		suspensionSpring.targetPosition = carConfig.TargetPosition;

		foreach (var wheelCollider in wheelColliders)
		{
			wheelCollider.wheelDampingRate = carConfig.WheelDampingRate;
			wheelCollider.suspensionDistance = carConfig.SuspensionDistance;
			wheelCollider.suspensionSpring = suspensionSpring;
		}
	}		

	private void SetEngine()
	{
		SetWheelsDrive();
		SetNitro();

		if (carConfig.FrontWheelDrive && carConfig.RearWheelDrive)
		{
			_motorForce = carConfig.CarMotorForce / 2;
			_NitroTorqueBoost = carConfig.NitroTorqueBoost / 2;
		}
		else
		{
			_motorForce = carConfig.CarMotorForce;
		}
	}
	private void SetWheelsDrive()
	{
		_isFrontWheelDriveOn = carConfig.FrontWheelDrive;
		_isRearWheelDriveOn = carConfig.RearWheelDrive;
	}

	private void SetNitro()
	{
		_nitroCapacity = carConfig.NitroCapacity;
		_currentNitroLevel = _nitroCapacity;
		_nitroUsageSpeed = carConfig.NitroUsageSpeed;
		_nitroRestorationSpeed = carConfig.NitroRestorationSpeed;
		_nitroSpeedBoost = carConfig.NitroSpeedBoost;
		_NitroTorqueBoost = carConfig.NitroTorqueBoost;
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