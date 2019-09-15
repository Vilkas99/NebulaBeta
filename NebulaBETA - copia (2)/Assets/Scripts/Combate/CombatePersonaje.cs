using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsPersonajes))] //Este script requiere que el objeto posea la clase "StatsPersonajes" (Es decir, que tenga stats)

//Clase que se ubicará en el enemigo, jugador, y todo aquel objeto que pueda luchar.
public class CombatePersonaje : MonoBehaviour {


    [SerializeField] GameObject barraMana;

    StatsPersonajes misStats; //Creo una variable de clase "StatsPersonajes" que almacenará las propiedades del personaje que posee este script.
    public float velocidadAtaque; //Variable que almacenará la velocidad del pj (La cual influencía en el cooldown de ataque).
    private float cooldownAtaque = 0f; //Tiempo de espera para que el jugador ataque (Al inicio es 0, porque al comienzo del combate, queremos que logre atacar al objetivo).
    const float cooldownCombate = 20f; //Si el jugador no ataca en los prox 5 seg, entonces deja de estar en combate. 
    float tiempoUltimoAtaque; //Almacena el tiempo en el que se ejecutó el último ataque.

    public float tiempoAnimacionAtacar = .6f; //Tiempo que tarda la animación de ataque, en ejecutarse.

    public event System.Action atacando; //Esta línea es igual a crear un delegado de tipo "void" que no posee argumentos.

    public bool enCombate { get; private set; } 



    private void Start()
    {
        misStats = GetComponent<StatsPersonajes>(); //Establezco que "misStats" es igual al componente "StatsPersonajes" de mi objeto.
        velocidadAtaque = misStats.velocidad.ObtenerValor(); //Obtengo la velocidad del pj.
    }

    private void Update()
    {
        cooldownAtaque -= Time.deltaTime; //Cada frame, el valor del cooldown se reduce por el tiempo que tarda en ejecutarse cada frame.     

        if (Time.time - tiempoUltimoAtaque > cooldownCombate) //Si el tiempo en el que se realiza el último ataque es mayor al cooldown de Combate, entonces...
        {
            enCombate = false; //Ya no estamos en combate (Porque el jugador no ataca).
        }

        if (enCombate && barraMana != null)
        {
            barraMana.SetActive(true);
        }

        else if (barraMana != null && !enCombate)
        {
            barraMana.SetActive(false);
        }
    }

    public void Atacar(StatsPersonajes objetivo) //Creo un método llamado "Atacar" que utilzia como argumento los stats de su objetivo. 
    {
        if (cooldownAtaque <= 0f) //Si mi "cooldown" (Tiempo de espera entre ataques) es menor o igual a 0...
        {

            StartCoroutine(HaceDaño(objetivo, tiempoAnimacionAtacar)); //Iniciamos la corutina que genera el dañi, tomando como arg los stats del objetivo, y el tiempo que toma la animación de "Atacar" en realizarse.
            if (atacando != null)
            {
                atacando();
            }

            cooldownAtaque = 3f/velocidadAtaque; //Restablezco el cooldown, y lo divido entre la velocidad del jugador.  (A mayor velocidad del jugador, menor cooldown.)
            enCombate = true;
            tiempoUltimoAtaque = Time.time;
        }
    }

    IEnumerator HaceDaño (StatsPersonajes stats, float retraso) //Creamos una corutina para hacer el daño, debido a que queremos que este se realice hasta que finalice la animación de ataque.
    {

        ManejadorMusica.instancia.Reproducir("Ataque Espada");
        yield return new WaitForSeconds(retraso); //Esperemos unos segundos equivalente a la variable "retraso", y después, realizamos el proceso de daño.
        int miDaño = misStats.daño.ObtenerValor(); //Creo una variable que almacenará el daño de mi objeto.
        stats.RecibirDaño(miDaño); //Accede al método "RecibirDaño" del "Objetivo" que toma como argumento el daño de mi objeto. 

        if (stats.saludActual <= 0) //Si la salud del enemigo es menor o igual a 0...
        {
            enCombate = false; //Entonces ya no estamos en combate (false).
        }


    }
}
