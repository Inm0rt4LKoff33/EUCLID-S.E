using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screamerController : MonoBehaviour
{

    Image screamerImage;

    [SerializeField]
    float fadeDuration = 1.0f;

    CanvasGroup canvasGroup;

    private void Start()
    {
        // Busca el objeto Canvas con el nombre "PlayerUI"
        GameObject playerUICanvas = GameObject.Find("PlayerUI");

        // Busca el objeto Image en el objeto Canvas encontrado anteriormente
        screamerImage = playerUICanvas.GetComponentInChildren<Image>();


        canvasGroup = screamerImage.GetComponent<CanvasGroup>();

        // Si el objeto Image no tiene un componente CanvasGroup, lo añade
        if (canvasGroup == null)
        {
            canvasGroup = screamerImage.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alien"))
        {
            StartCoroutine(ShowScreamerCoroutine());
        }
    }

    IEnumerator ShowScreamerCoroutine()
    {
        // Muestra la imagen del screamer con un efecto de desvanecimiento
        AudioManager.Instance.playSFX("Sd_AlienScream");

        canvasGroup.alpha = 0.0f;

        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha = 1.0f;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        // Oculta la imagen del screamer con un efecto de desvanecimiento
        while (canvasGroup.alpha > 0.0f)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        AudioManager.Instance.stopAllSFXEnemies();
        canvasGroup.alpha = 0.0f;

        StartCoroutine(playerBreathing());
    }

    
    IEnumerator playerBreathing()
    {
        AudioManager.Instance.playSFX("Sd_Breathing");

        yield return new WaitForSeconds(10);

        AudioManager.Instance.stopAllSFXOtros();
    }
        
}
