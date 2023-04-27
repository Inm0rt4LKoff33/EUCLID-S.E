using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// PROXIMA SCENA
    /// </summary>
    public void NextScene()
    {
        //TRAER EL INDICE DE LA SCENA ACTUAL
        int currectSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currectSceneIndex + 1);
    }


    /// <summary>
    /// TREAR LA PRIMERA ESCENA
    /// </summary>
    public void FirstScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// REINICIAR DESDE LA ESCENA EN LA QUE ESTOY   
    /// </summary>
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    /// <summary>
    /// TREAR LA PRIMERA ESCENA
    /// </summary>
    public void NextScene(int numero)
    {
        SceneManager.LoadScene(numero);
    }



}
