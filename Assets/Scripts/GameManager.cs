using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
     public static GameManager Instance
 {
     get {
         if (_instance == null)
         {
             _instance = GameObject.FindObjectOfType<GameManager>();
             
             if (_instance == null)
             {
                 GameObject container = new GameObject("Manager");
                 _instance = container.AddComponent<GameManager>();
             }
         }
     
         return _instance;
     }
 }
    public int EnemyCount = 0;
    public bool takeInput = true;
    void Start()
    {
        takeInput = true;
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyCount <= 0)
        {
            takeInput = false;
            StartCoroutine(ChangeScene(0.5f));
        }
    }
    IEnumerator ChangeScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(
            (scene.buildIndex != 4) ? scene.buildIndex + 1 : 0
        );
    }
}
