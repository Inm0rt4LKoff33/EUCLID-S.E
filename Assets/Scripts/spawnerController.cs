using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerController : MonoBehaviour
{
    [SerializeField]
    LevelManager lv;

    [SerializeField]
    GameObject playerPrefab;

    void Awake()
    {
        int escenaActual = lv.escenaActual();
        if (escenaActual == 2)
        {
            Instantiate(playerPrefab, new Vector3(135.709F, 0.01999998F, -89.2726F), Quaternion.identity, gameObject.transform);
        }
    }
}
