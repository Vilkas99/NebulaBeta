using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sonido {

    public AudioClip clip;

    public string nombre;

    [Range(0f, 1f)]
    public float volumen;

    [Range(.1f, 3f)]
    public float pitch;

    public bool ciclo;


    [HideInInspector]
    public AudioSource fuente;


}
