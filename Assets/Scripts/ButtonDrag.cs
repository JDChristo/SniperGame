using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDrag : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public TouchControls touchControls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        StopAllCoroutines();
        if(GameManager.Instance.takeInput)
        {
            touchControls.ScopeInButton();
        }
        else{
            touchControls.ScopeOutButton();
        }
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        touchControls.ShootBullet();
        touchControls.ScopeOutButton();
    }

}
