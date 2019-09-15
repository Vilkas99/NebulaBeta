using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Libreria que nos permitirá usar el componente "Nav Mesh Agent" (Componente que le permite al jugador desplazarse a las áreas seleccionadas de movimiento)  

[RequireComponent(typeof (NavMeshAgent))] //RequiereComponent es un atributo que anexa el componente mencionado (En este caso, NavMeshAgent), al objeto de juego que contendrá este script.
                                          //Más info: https://docs.unity3d.com/ScriptReference/RequireComponent.html

public class MotorJugador : MonoBehaviour {

    Transform objetivo; //Variable que almacena la posicion del objeto al que se le ha dado click derecho (Porque es interactivo). Al inicio del juego, esta es "null".
    NavMeshAgent agente; //Creo una variable que almacenará los datos del "NavMeshAgent" de mi jugador. 
    
    float velocidad;

	// Use this for initialization
	void Start () {
        agente = GetComponent<NavMeshAgent>(); //Almacena las propiedades del componente "NavMeshAgent" del objeto de juego que tenga este script, a mi variable "agente".
    }

    void Update()
    {

        if (objetivo != null) //Verifica en cada frame si existe un objetivo (Es decir, si mi variable objetivo no es nula)
            //En la clase "ControladorJugador", si se hace click derecho en algún objeto que sea "Interactivo" (Que posea esa clase), se ejecuta el método "SeguirObjetivo" (Línea 38 - Esta clase) 
            // El cual establece un objetivo, y por ende, permite que este condicional se ejecute. 
        {
            agente.SetDestination(objetivo.position); //Desplaza al jugador (agente) a la posción de mi objetivo.
            EncararObjetivo();

        }        
    }



    public void MoverHaciaPos (Vector3 pos) //Método público que se encarga de mover el jugador hacia la posición que se le de como argumento "Vector 3 pos". 
    {

        
        agente.SetDestination(pos); //Accede a las propiedades de mi variable del tipo "NavMeshAgent". y ejecuta un método llamado "SetDestination()", que (en resumen) desplaza la posicion del objeto hacia el argumento tipo Vector3
                                    // que se le da (pos, en este caso). 
        velocidad = agente.velocity.magnitude;

               
    }

    public void SeguirObjetivo(Interactivo nuevoObjetivo) //Método que establece como "objetivo" a un objeto que contenga la clase "Interactivo".
    {
        agente.stoppingDistance = nuevoObjetivo.radio * .8f; //"stoppingDistance" es un método del componente "NavMeshPro" que almacena la distancia entre el jugador y su objetivo a la que el "agente" (jugador) 
                                                             //se detiene para no colisionar con su objetivo. 
                                                             //Establezco aquí entonces, que la distancia a la que se detiene mi jugador, es el radio de mi objetivo (Recordemos que los objetos "Interactivos"
                                                             // poseen un gizmo con un radio, que determina su espacio de interacción; por ende, la distancia a la que se detiene el jugador, será el radio de su
                                                             //objetivo, multiplicado por .8 (Para que sea lo más exacto posible)

        agente.updateRotation = false; //Desactivo la rotación automática, porque esta se manejará en el método llamado "MirarObjetivo". 
                                       //Esto por la sig razón: Cuando nuestro jugador se detiene en el la distancia radio de su objetivo, la rotación ya no se actualiza (Es por ello que debemos encargarnos de esta con un método)

        objetivo = nuevoObjetivo.posicionInteraccion; //Al asignarle a mi objetivo la posición de mi argumento "nuevoObjetivo", permite que el condiciona de la línea 23 (El cual se encarga de desplazar al jugador
        //hacia la pos de objetivo) se ejecute.
        
    }

    public void DejaDeSeguirAlObjetivo() //Método que interrumpe el seguimiento del jugador hacia su objetivo. 
    {
        agente.stoppingDistance = 0f; //Establezco su distancia de "detenderse" a 0, para que no tenga conflictos a la hora de desplazarse hacia objetos que no sean interactivos.
        agente.updateRotation = true;
        objetivo = null; //Al establecer objetivo como "null", impedimos que el condicional de la línea 23 (El cual se encarga de desplazar al jugador hacia la pos de su objetivo) se ejecute.
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
}
