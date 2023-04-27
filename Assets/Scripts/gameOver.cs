using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{

    public void OnPressRestart()
    {
        LevelManager scene = FindObjectOfType<LevelManager>();

        scene.Reiniciar();
    }

    public void OnPressSalir()
    {
        LevelManager scene = FindObjectOfType<LevelManager>();
        scene.FirstScene();
    }

}
