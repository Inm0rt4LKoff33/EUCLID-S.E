using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    int numeroPuzzle = 1;

    [SerializeField]
    GameObject dialogueExclamation;

    [SerializeField]
    LevelManager lv;

    [SerializeField]
    GameObject[] spawnPointsPuzzles;

    //TextArea: Se usa unicamente para mejorar la visualizacion de los campos a rellenar desde la UI de desarrollo.

    [SerializeField, TextArea(4, 6)]
    string[] dialogueLines;

    [SerializeField]
    GameObject dialoguePanel;

    [SerializeField]
    GameObject imagen;

    [SerializeField]
    TMP_Text TextDialogue;

    [SerializeField]
    TMP_Text TextInstruccion;

    bool isPlayerInRange;
    bool didDialogueStart;
    int lineIndex;

    float waitTime = 0.05F;

    void Update()
    {
        //Detect player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {

            if (!didDialogueStart)
            {
                StartDialogue();

            }
            else if (TextDialogue.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }

        }
    }


    void StartDialogue()
    {

        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        imagen.SetActive(true);
        dialogueExclamation.SetActive(false);
        lineIndex = 0;

        //Stop game time when dialogue starts. All objects are going to stop.
        Time.timeScale = 0.0F;

        StartCoroutine(ShowDialogLine());
    }

    void NextDialogueLine()
    {

        lineIndex++;

        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowDialogLine());
        }
        else
        {
            //If it steps here is because the are not more dialogues
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            imagen.SetActive(false);
            dialogueExclamation.SetActive(true);

            //Restart game time when dialogue starts
            Time.timeScale = 1.0F;
            Vector3 posicion = Vector3.zero;
            //EJECUTAR SIGUIENTE PUZZLE
            switch (numeroPuzzle)
            {
                case 1:

                    lv.NextScene(3);

                    posicion = spawnPointsPuzzles[0].transform.position;
                    recordadorManager.Instance.SetUltimaPosicionJugador(posicion);
                    recordadorManager.Instance.AgregarPuzzle(1);
                    break;

                case 2:

                    posicion = spawnPointsPuzzles[1].transform.position;
                    recordadorManager.Instance.SetUltimaPosicionJugador(posicion);
                    lv.NextScene(4);
                    recordadorManager.Instance.AgregarPuzzle(2);
                    break;

                case 3:

                    posicion = spawnPointsPuzzles[2].transform.position;
                    recordadorManager.Instance.SetUltimaPosicionJugador(posicion);
                    lv.NextScene(5);
                    recordadorManager.Instance.AgregarPuzzle(3);
                    break;

                default:
                    return;
            }



        }
    }

    //Corrutine 
    IEnumerator ShowDialogLine()
    {
        TextDialogue.text = string.Empty;

        //We concatenate the text because we want the effect of progressice completation
        int count = 0;

        foreach (char x in dialogueLines[lineIndex])
        {
            TextDialogue.text += x;

            count++;
            if (count == dialogueLines[lineIndex].Length)
            {
                TextInstruccion.enabled = true;
            }
            else
            {
                TextInstruccion.enabled = false;
            }

            //THe method ignores game time.
            yield return new WaitForSecondsRealtime(waitTime);
        }
    }

    /// <summary>
    /// Start dialog
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true!;
            dialogueExclamation.SetActive(true);
        }
    }

    /// <summary>
    /// Do not start dialogue
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit(Collider collision)
    {
        isPlayerInRange = false;
        dialogueExclamation.SetActive(false);
    }
}
