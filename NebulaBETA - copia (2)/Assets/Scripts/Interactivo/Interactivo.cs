using UnityEngine;

public class Interactivo : MonoBehaviour {

    public float radio = 3f; //Distancia mínima en la que el jugador puede interactuar con el objeto. 
    public Transform posicionInteraccion; //Variable pública, que determina la posición en la que los objetos interactivos, tendrán su interacción.
                                           //En algunos, puede ser su posición actual (Transform.position), y en otros, puede ser enfrente, a un lado, 
                                           //Donde el objeto lo requiera (Con el fin de no condicionarnos a la pos del objeto)
                                           //Este se determina en el INSPECTOR, ya sea creando un objeto vacío y vinculandonlo a la variable, o
                                           //vinculando el "transform" de este objeto, si se quiere que la interacción tome lugar en su posición.


    bool estaEnfocado = false; //Bandera que me indica si este objeta está siendo "enfocado" por el jugador.
    bool yaInteractuo = false; //Bandera que me indica si este objeto ya ha interactuado con el jugador.
    

    Transform jugador; //Variable que almacenará las propiedades transform de nuestro jugador.

    

    public virtual void Interactuar() //Método virtual que genera los procesos de interacción.
                                      //Es virtual porque queremos sobreescribirlo con clases derivadas (Como "Enemigo", "Item", etc...) 
                                      // de la clase principal (en este caso, "Interactivo")
    {
        //Esté método, al ser VIRTUAL, está hecho para sobreescribirse.

    }

    public virtual void Update()
    {
        if (estaEnfocado && !yaInteractuo) //Si "estaEnfocado" es true, y este no ha interactuado (yaInteractuo = false o !yaInteractuo), entonces...
        {            
            float distancia = Vector3.Distance(jugador.position, posicionInteraccion.position); //Calcula la distancia entre el jugador y el objeto.
            if(distancia <= radio) //Si la distancia es menor o igual al radio de interacción de mi objeto, entonces...
            {
                Interactuar(); //Método VIRTUAL que realiza el proceso de interacción que cada objeto establezca.             
                yaInteractuo = true; //Una vez que ha interactuado, establece mi "bandera" como "true", con el fin de que sólo ejecute este condiconal 1 vez (Y no miles, por el update).
            }
        }
        
    }

    void OnDrawGizmosSelected() //Método que dibujará un "Gizmo" (Gráfico) que representa el campo de acción en donde el jugador puede interactuar con el objeto. 
    {

        if (posicionInteraccion == null) //Si no tenemos (O necesitamos) un objeto que se encargue de denotar la posición de interacción, entonces...
        {
            posicionInteraccion = transform; //La posicion de interacción, será la posición del objeto interactivo.
        }

        Gizmos.color = Color.yellow; //Establezco el color del gizmo
        Gizmos.DrawWireSphere(posicionInteraccion.position, radio); //Dibujo un gizmo de tipo esfera, en la posición del objeto que posee el script (transform.postion) y con el radio de acción establecido (Ln 4)
    }

    public void Enfocado(Transform jugadorT) //Método que determina si este objeto ha sido "enfocado" (O seleccionado) por el jugador, y con ello, permite que el "if" (Línea 14) se ejecute.
    {
        yaInteractuo = false; //Establecemos esta bandera como falso, para que el jugador pueda interactuar con el objeto interactivo, cada vez que lo "enfoque" (Presione click derecho)
        estaEnfocado = true; //Establece mi "bandera" como "true"
        jugador = jugadorT; //Vincula mi variable transform llamda "jugador" con el argumento "jugadorT", con el fin de generar un vector de distancia entre la pos...
                            //del jugador, y la pos de la interacción. (Línea 32)
    }

    public void Desenfocado() //Método que desactiva mi variable "estaEnfocado" denotando que el objetos se encuentra deseleccionado.
    {
        yaInteractuo = false;
        estaEnfocado = false;
        jugador = null;
    }
}
