using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ganarController : MonoBehaviour
{

    bool ganar = false;


    [SerializeField]
    GameObject canvas;


    void Start()
    {
        ganar = recordadorManager.Instance.ganar();

        if (ganar)
        {
            Time.timeScale = 0.0F;
            canvas.SetActive(true);
        }
    }

}
