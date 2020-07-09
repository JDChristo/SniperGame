using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    [SerializeField]
    private Transform spine;
    [SerializeField]
    private Transform neckPivot;
    [SerializeField]
    private float mouseSensitivity;
    private float xAxisClamp, yAxisClamp;
    
    private void Start() 
    {
        
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
             CameraRotation();
        }
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;
        yAxisClamp += mouseX;

        xAxisClamp = ClampFloatNum(neckPivot, xAxisClamp, ref mouseY, minAngle: -30.0f, maxAngle: 40.0f ,minValue : 50, maxValue: 340.0f);
        yAxisClamp = ClampFloatNum(spine, yAxisClamp, ref mouseX, minAngle: -60.0f, maxAngle: 55.0f ,minValue : 300, maxValue: 55);

        neckPivot.Rotate(Vector3.left * mouseY);
        spine.Rotate(Vector3.left * mouseX);

    }

    private float ClampFloatNum(Transform body, float value, ref float mouseAxis, float maxAngle, float maxValue, float minAngle, float minValue)
    {
        if (value > maxAngle)
        {
            value = maxAngle;
            mouseAxis = 0.0f;
            ClampRotation(body: body, value: maxValue);
        }
        else if (value < minAngle)
        {
            value = minAngle;
            mouseAxis = 0.0f; 
            ClampRotation(body: body, value: minValue);
        }
        return value;
    }

    private void ClampRotation(Transform body,float value)
    {
        Vector3 eulerRotation = body.eulerAngles;
        if(body == neckPivot)
            eulerRotation.x = value;
        else
            eulerRotation.y = value;
        body.eulerAngles = eulerRotation;
    }

    private void DebugAngles(bool x)
    {
        if(x)
        {
            print(xAxisClamp);
            print(neckPivot.eulerAngles);
        }
        else
        {
            print(yAxisClamp);
            print(spine.eulerAngles);
        }
    }
}
