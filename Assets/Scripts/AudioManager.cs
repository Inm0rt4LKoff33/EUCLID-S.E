using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    AudioMixer audioMixerMusica;

    [SerializeField]
    AudioMixer audioMixerEfectos;

    [SerializeField]
    Sound[] musicSounds, sfxSounds, sfxEnemigosSounds, sfxSourceOtros;

    [SerializeField]
    AudioSource musicSource, sfxSource, sfxHitsEnemigosSource, sfxRecoleccionSource;


    public void playMusic(string name)
    {

        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void playSFX(string name)
    {
        //BUSCAMOS EN AMBAS PISTAS PARA SIEMPRE TENER LOS SONIDOS SEPARADOS Y QUE NO SE ESCUCHE UNO ENCIMA DEL OTRO

        Sound s = Array.Find(sfxSounds, x => x.name == name);
        Sound sE = Array.Find(sfxEnemigosSounds, x => x.name == name);
        Sound sR = Array.Find(sfxSourceOtros, x => x.name == name);

        if (s == null && sE == null && sR == null)
        {
            Debug.Log("SFX not found");
        }
        else
        {
            if (s != null)
            {
                sfxSource.clip = s.clip;
                sfxSource.Play();
            }
            else if (sE != null)
            {
                sfxHitsEnemigosSource.clip = sE.clip;
                sfxHitsEnemigosSource.Play();
            }
            else
            {
                sfxRecoleccionSource.clip = sR.clip;
                sfxRecoleccionSource.Play();
            }
        }
    }

    public void stopMusic(string name)
    {

        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music not found");
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void stopAllMusic()
    {
        musicSource.Stop();
    }

    public void stopAllSFX()
    {
        sfxSource.Stop();
    }


}
