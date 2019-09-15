using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsPersonajes))]
public class Enemigo : Interactivo
{

    ManejadorJugador jugadorAcceso; //Variable que me permitirá acceder a TODOS los datos de mi jugador. 
    public StatsPersonajes misStats; //Variable que almacena los "Stats" de mi enemigo.

    private void Start()
    {
        jugadorAcceso = ManejadorJugador.instancia; //Almaceno los datos de mi jugador al igualar mi variable con la "instancia" de mi clase "ManejadorJugador".
        misStats = GetComponent<StatsPersonajes>(); //Obtengo los stats de mi enemigo, al obtener su componente ("StatsPersonajes").
    }

    public override void Interactuar()
    {        

        //Creo una variable de clase "CombatePersonaje" que accederá al componente "CombatePersonaje" de mi JUGADOR. (Para que a la hora de ejecutar el método "Atacar"...
        //Obtenga el daño del JUGADOR (Debido a que este se ejecuta desde el jugador) y obtenga la salud del ENEMIGO (Al proveerle como argumento sus "stats).
        CombatePersonaje combateJugador = jugadorAcceso.jugador.GetComponent<CombatePersonaje>();
        if (combateJugador != null)
        {
            combateJugador.Atacar(misStats); //Cuando el jugador interactue con el enemigo, queremos que lo ataque (Ejecutando el método "Atacar" de mi clase "CombatePersonaje" de mi JUGADOR).
        }
    }

}

