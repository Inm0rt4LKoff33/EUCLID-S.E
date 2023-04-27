using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winPuzzleControllerMenu : MonoBehaviour
{

    //SOLO SE DEVUELVE PARA EFECTOS DE DEPOSTRACIÓN
    public void OnPressContinuar()
    {
        LevelManager scene = FindObjectOfType<LevelManager>();
        scene.NextScene(2);
    }

}
