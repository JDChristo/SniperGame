using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControls : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform neckPivot, spinePivot;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private GameObject scopeOverlay, scopeButton;

    private Touch touchin = new Touch();

    public GunBehaviour gun;

    private float xAxis = 0f;
    private float yAxis = 0f;
    private float initialFOV;
    private float finalFOV;

    void Start()
    {
        xAxis = spinePivot.eulerAngles.x;
        yAxis = neckPivot.eulerAngles.y;
        initialFOV = 60f;
        //scopeButton.GetComponent<Button>().onClick.AddListener(() => ScopeInButton());
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && GameManager.Instance.takeInput)
        {
            Touch t = Input.touches[0];
            ViewRotation(t);
        }
    }

    private void ViewRotation(Touch t)
    {
        if (t.phase == TouchPhase.Began)
        {
            touchin = t;
        }
        else if (t.phase == TouchPhase.Moved)
        {
            float mouseX = (touchin.position.x - t.position.x) * Time.deltaTime * sensitivity;
            float mouseY = (touchin.position.y - t.position.y) * Time.deltaTime * sensitivity;

            xAxis += mouseY;
            yAxis += mouseX;

            xAxis = ClampFloatNum(neckPivot, xAxis, ref mouseY, minAngle: -15.0f, maxAngle: 15.0f, minValue: 360, maxValue: 29.5f);
            yAxis = ClampFloatNum(spinePivot, yAxis, ref mouseX, minAngle: 297, maxAngle: 414f, minValue: 63, maxValue: 305.8f);
            
            neckPivot.Rotate(Vector3.right * mouseY);
            spinePivot.Rotate(Vector3.right * mouseX);
        }
        else if (t.phase == TouchPhase.Stationary)
        {
            //touchin = t;
        }
        else if(t.phase == TouchPhase.Ended)
        {
            
        }
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

    private void ClampRotation(Transform body, float value)
    {
        Vector3 eulerRotation = body.eulerAngles;
        if (body == neckPivot)
            eulerRotation.x = value;
        else
            eulerRotation.y = value;
        body.eulerAngles = eulerRotation;
    }

    public void ShootBullet()
    {
        gun.Shoot();
    }

    public void ScopeInButton()
    {
        StartCoroutine(ScopeAnimation(60f,15f,3f,true,0f));
    }
    public void ScopeOutButton()
    {
        StartCoroutine(ScopeAnimation(15f,60f,3f,false, 0.5f));
    }

    IEnumerator ScopeAnimation(float initialPos, float finalPos,float timeScale, bool value, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        float progress = 0;

        while (progress <= 1)
        {
            mainCamera.fieldOfView = Mathf.Lerp(initialPos, finalPos, progress);
            progress += Time.deltaTime * timeScale;
            yield return null;
        }
        scopeOverlay.SetActive(value);
        mainCamera.fieldOfView = finalPos;
    }
    private void DebugAngles(bool x)
    {
        if (x)
        {
            print(xAxis);
            print(neckPivot.eulerAngles);
        }
        else
        {
            print(yAxis);
            print(spinePivot.eulerAngles);
        }
    }
}
