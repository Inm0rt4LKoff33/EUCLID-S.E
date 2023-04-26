using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    //[SerializeField]
    //GameObject spawnPoint;

    Scene currentScene;

    //List<GameObject> spawnPoints;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentScene.buildIndex == 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
