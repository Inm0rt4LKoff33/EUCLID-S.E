using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
     where T : MonoBehaviour
{
    //CREAMOS INSTANCIA DE SÍ MISMO
    static Singleton<T> _instance;

    protected static readonly object _synLock = new object();

    //Virtual para sobreescribirlo
    public virtual void Awake()
    {
        bool destroyInstance = true;

        if (_instance == null)
        {

            //PARA QUE HAYA COLA DE ESPERA PARA LLAMARLO
            lock (_synLock)
            {
                if (_instance == null)
                {
                    _instance = this;
                    //INDICAR QUE NUNCA SE DESTRUYA
                    DontDestroyOnLoad(gameObject);
                    destroyInstance = false;
                }
            }
        }

        //PARA MANTENER SOLO 1 INSTANCIA SIEMPRE
        if (destroyInstance)
        {
            Destroy(gameObject);
        }

    }


    static Singleton<T> GetInstance()
    {
        return _instance;
    }


    public static T Instance
    {
        get { return GetInstance() as T; }
    }

}