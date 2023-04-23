using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    string gameScene;

    void Start()
    {
        ponerMusicaInicial();
    }

    void ponerMusicaInicial()
    {
        //PARAMOS TODOS LOS SONIDOS EN CASO DE HABERLOR
        AudioManager.Instance.stopAllSFX();
        AudioManager.Instance.stopAllMusic();

        //REPRODUCIMOS NUESTRA MUSICA
        AudioManager.Instance.playMusic("Ms_Inicio");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

}
