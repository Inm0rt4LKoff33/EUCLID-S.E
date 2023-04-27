using Assets.Scripts.Modelos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordadorManager : Singleton<recordadorManager>
{
    Ganar playerGanar;

    bool primeraVezJugado = true;


    //CREAMOS UN PLAYER EN EL AWAKE HEREDADO
    public override void Awake()
    {
        base.Awake();

        if (playerGanar == null)
        {
            lock (_synLock)
            {
                if (playerGanar == null)
                {
                    playerGanar = new Ganar();
                }
            }
        }
    }

    //POSICION DEL JUGADOR

    public Vector3 GetUltimaPosicionJugador()
    {
        return playerGanar.ultimaPosicion;
    }

    public void SetUltimaPosicionJugador(Vector3 posicion)
    {
        playerGanar.ultimaPosicion = posicion;
    }


    //PUZZLES PARA GANAR
    public bool GetLlaveUno()
    {
        return playerGanar.LlaveUno;
    }

    public bool GetLlaveDos()
    {
        return playerGanar.LlaveUno;
    }


    public bool GetLlaveTres()
    {
        return playerGanar.LlaveUno;
    }


    public void AgregarPuzzle(int valor)
    {
        switch (valor)
        {
            case 1:
                primeraVezJugado = false;
                playerGanar.LlaveUno = true;
                break;

            case 2:
                primeraVezJugado = false;
                playerGanar.LlaveDos = true;
                break;

            case 3:
                primeraVezJugado = false;
                playerGanar.LlaveTres = true;
                break;

            default:
                break;
        }
    }

    public bool getPrimeraVezJugando()
    {

        return primeraVezJugado;

    }

    //VALORAR GANAR
    public bool ganar()
    {

        bool llave1 = GetLlaveUno();
        bool llave2 = GetLlaveDos();
        bool llave3 = GetLlaveTres();

        if (llave1 && llave2 && llave3)
        {
            return true;
        }

        return false;
    }
}
