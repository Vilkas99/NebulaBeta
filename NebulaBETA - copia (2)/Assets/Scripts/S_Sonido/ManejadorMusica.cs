using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManejadorMusica : MonoBehaviour {

    public Sonido[] sonidos; //Arreglo de objetos clase "Sonido" que almacenará los sonidos que quiera modificar con este script.

    public static ManejadorMusica instancia; //Singleton



    // Use this for initialization
    void Awake() { //Al momento de crear el objeto de juego (Es decir, cuando inicia el juego) 

        //DontDestroyOnLoad(gameObject); Eliminé porque erradicó el archivo de audio de los componentes.,

        if (instancia != null) //Si ya existe otro objeto que contenga "ManejadorMusica", entonces...
        {
            Destroy(gameObject); //Destruye el objeto de juego.
        }

        instancia = this; //Si instancia es nulo, entonces instancia será este objeto.


		foreach(Sonido s in sonidos) //Por cada objeto de clase "Sonido" que haya en mi arreglo "sonidos"...
        {
            s.fuente = gameObject.AddComponent<AudioSource>(); //Creo un componente en mi objeto de juego (MusicManager) de tipo "AudioSource" y lo vinculo con mi variable de tipo "AudioSource" "fuente".
            s.fuente.clip = s.clip; //Añado al clip de la fuente, el clip del sonido.
            
            //Hago lo mismo con los demás componentes, para conformar la fuente (AudioSource) de mi audio.
            s.fuente.loop = s.ciclo;            
            s.fuente.volume = s.volumen;            
            s.fuente.pitch = s.pitch;
            
        }

        Reproducir("Musica Fondo");
	}
	
	public void Reproducir(string nombreAudio) //Método que reproduce el audio del nombre que coloquemos (argumento) al ejectuarlo.
    {
        if (nombreAudio == null) //Si el argumento no existe...
        {
            Debug.LogWarning("El audio: " + nombreAudio + ". No ha sido encontrado!"); //Me mando una advertencia a consola.
            return; //Y regreso el método para que no haya errores.
        }
        
        //Método de "System"
        Sonido sonidoReproducir = Array.Find(sonidos, Sonido => Sonido.nombre == nombreAudio); //En el arreglo "sonidos", necesito que encuentre un objeto de clase "Sonido"; cuyo nombre sea igual al del argumento "nombreAudio").
        sonidoReproducir.fuente.Play(); //Accedemos al "AudioSource" (Fuente), y lo reproducimos.
    }
    

}
