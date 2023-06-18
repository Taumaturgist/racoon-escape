using UnityEngine;

public class CameraChoiceOffsetAndRotation : MonoBehaviour
{
    public void GetCameraOffsetAndRotation(int carID, ref Vector3 offsetShoulderView, ref Quaternion rotationShoulderView)
    {
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
}
