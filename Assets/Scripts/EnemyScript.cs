using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;
    public void RecieveDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Dead();
        }
    }
    void Dead()
    {
        Destroy(gameObject);
        GameManager.Instance.EnemyCount -= 1;
    }
}
