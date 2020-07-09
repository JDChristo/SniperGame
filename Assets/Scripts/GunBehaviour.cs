using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    
    public float damage = 100f;
    public float range = 100f;

    public Camera mainCamera;

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.tag);
            EnemyScript target = hit.transform.gameObject.GetComponentInParent<EnemyScript>();
            if(target != null)
            {
                target.RecieveDamage(100);
                
                if(hit.transform.tag == "Head")
                {
                    target.RecieveDamage(100);
                }
                else if(hit.transform.tag == "Body")
                {
                    target.RecieveDamage(80);
                }
            }
        }
    }
}
