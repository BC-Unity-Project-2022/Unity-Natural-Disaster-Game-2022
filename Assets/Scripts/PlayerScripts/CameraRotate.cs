using Cinemachine;
using UnityEngine;

struct MouseMovementData
{
    public float h;
    public float v;

    public override string ToString()
    {
        return $"h: {h}, v: {v}";
    }
}

public class CameraRotate : MonoBehaviour
{
    public Transform playerTransform;

    public CinemachineVirtualCamera virtCam;
    public int localVirtCamPriority = 10;
    
    public float rotationSpeedX = 10f;
    public float rotationSpeedY = 10f;

    public float maxLookUpRotation = 85;
    public float minLookDownRotation = -85;

    void FixedUpdate()
    {
        MouseMovementData mouseMovementData = new MouseMovementData()
        {
            h = Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime,
            v = -Input.GetAxis("Mouse Y") * rotationSpeedY * Time.deltaTime
        };
        
        if (Input.GetButton("Jump"))
        {
            // var cube = GameObject.CreatePrimitive(PrimitiveType.Cube); 
            // cube.transform.position = new Vector3(0, 0.5f, 0);
        }

        ApplyRotation(mouseMovementData);
    }

    
    private void ApplyRotation(MouseMovementData mouseMovementData)
    {
        Quaternion hQuaternion = Quaternion.AngleAxis(mouseMovementData.h, Vector3.up);
        
        playerTransform.rotation *= hQuaternion;
        
        Vector3 oldAimControlTransformRotation = transform.rotation.eulerAngles;

        float newRotX = oldAimControlTransformRotation.x + mouseMovementData.v;
        // deal with the angles resetting to 0<= x < 360
        if (newRotX > 180) newRotX = newRotX - 360;
        oldAimControlTransformRotation.x = Mathf.Clamp(newRotX, minLookDownRotation, maxLookUpRotation);
        
        transform.rotation = Quaternion.Euler(oldAimControlTransformRotation);
    }
}