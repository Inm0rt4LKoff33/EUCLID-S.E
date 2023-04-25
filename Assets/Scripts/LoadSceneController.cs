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

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentScene.buildIndex == 1)
            {
                SceneManager.LoadScene(currentScene.buildIndex - 1);
            }
            else
            {
                SceneManager.LoadScene(currentScene.buildIndex + 1);
            }
        }
    }
}
