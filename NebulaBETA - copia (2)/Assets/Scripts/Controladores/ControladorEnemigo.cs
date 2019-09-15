using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour {

    public float radioAlerta = 5f; //Variable que establece el radio en el que el enemigo atacará al jugador.
    //Si el jugador está en ese radio, entonces el enemigo empezará a perseguirlo.

    Transform objetivo; //Variable del tipo "Transform" que almacenará la posición del objetivo a interactuar (En este caso, el jugador)
    NavMeshAgent agente; //Variable que almacenará las propiedades del componente de movimiento "NavMeshAgent".
    CombatePersonaje combateEnemigo;

	// Use this for initialization
	void Start () {
        objetivo = ManejadorJugador.instancia.jugador.transform; //Establecemos que nuestro objetivo, será el jugador.
        agente = GetComponent<NavMeshAgent>(); //Accdemos al componente NavMesh de nuestro objeto.
        combateEnemigo = GetComponent<CombatePersonaje>(); //Obtiene la clase y métodos de "CombatePersonaje" presente en él.
		
	}
	
	// Update is called once per frame
	void Update () {
        float distancia = Vector3.Distance(objetivo.position, transform.position); //Creamos una variable que almacena la distancia entre el objetivo y el enemigo.

        if (distancia <= radioAlerta) //Si la distancia es menor o igual al radio de alerta...
        {
            agente.SetDestination(objetivo.position); //Entonces, establecemos que el enemigo se desplace a la posición del jugador. 

            if (distancia <= agente.stoppingDistance) //Si la distancia (entre el jugador y el enemigo) es menor o igual a la distancia a la que se detiene el enemigo...
            {
                //Atacarlo
                StatsPersonajes objetivoStats = objetivo.GetComponent<StatsPersonajes>(); //Obtiene los stats de su objetivo.
                if (objetivoStats != null)
                {
                    combateEnemigo.Atacar(objetivoStats); //Ejecuta el método "Atacar" tomando como argumento las propiedades de su objetivo (Primordialmente, el jugador).
                }
                
                EncararObjetivo(); //Método que se encarga de modificar la rotación cuando el objetivo se encuentre demasiado cerca.
                

            }
        }
	}

    void EncararObjetivo()
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized; //Establezco un vector 3 llamado "dirección" que almacenará el vector dirección de 
        Quaternion rotacionMirar = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z)); //Creo una variable del tipo "Quaternion" llamada "rotaciónMirar"...
                                                                                                      //Que almacenára el ángulo de rotación de un vector con la posición x y z de mi dirección...
                                                                                                      //No tomamos en cuenta y (vale 0 en el Vector), porque no queremos que el jugador
                                                                                                      //empiece a "flotar" por la modificacion de su rotación en y.


        //Establezco que la rotación de mi objeto, será igual a la interpolación entre su rotación actual, y la rotación para encarar al objetivo (rotaciónMirar) a una velocidad de "Time.deltaTime * 5f" .

        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionMirar, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioAlerta);

        
    }
}
