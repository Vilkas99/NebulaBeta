using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof (MotorJugador))]

public class ControladorJugador : MonoBehaviour {

    [SerializeField] GameObject canvasUI; //Variable que en el inspector obtendrá el prefab del icono y texto que se presenta cuando se utiliza una habilidad que se disipa con el tiempo.

    #region Variables de Acceso
    public Interactivo objetivo; //Objeto interactivo que me permitirá acceder a los métodos de mi clase "Interactivo".
    public Enemigo enemigo; //Variable que almacena las propiedades de los enemigos a los que se identifica al ejercer click derecho sobre ellos.
    public LayerMask mascaraMovimiento; //Capa que determina el tipo de objetos en los que el jugador puede caminar. (Esta se vincula en el inspector con la capa "Suelo").
                                        //Si no tuviesemos esto, en nuestro método "Physics.Raycast" (Línea 27)  podríamos caminar sobre sillas, paredes...
    StatsPersonajes statsJugador; //Variable que almacena los stats del jugador.
    #endregion

    Camera camara; //Referencia para acceder a las propiedades de mi camara
    MotorJugador movimiento;  //Referencia para acceder a las propiedades de mi clase "MotorJugador".          
    [SerializeField] GameObject barraHabilidades;

    public bool autoAtaque; //Variable que determina si se está realizan el "autoAtaque".


    // Use this for initialization
    void Start()
    {
        camara = Camera.main; //Mi variable cámara tendrá las propiedades de mi camara principal.
        movimiento = GetComponent<MotorJugador>(); //Almaceno en mi variable "movimiento" las propiedades de mi clase "MotorJugador".

        statsJugador = GetComponent<StatsPersonajes>(); //Obtengo los stats del jugador.
        autoAtaque = false; //Establezco que al inicio, autoatque será falso.
        barraHabilidades.SetActive(false); //Y mi barra de habilidades estará desactivada.



    }



    // Update is called once per frame  
    void Update()
    {

        //Creamos este condicional, para impedir que el jugador se desplace al hacer click sobre algún elemento de su interfaz (Inventario, salud, etc...)
        #region "Clicks"


        if (autoAtaque) //Si mi bool de autoAtaque es verdadero...
        {
            Enfocar(objetivo); //Se enfoca en el objetivo (Que será un enemigo) e "interactua" (que en el caso del enemigo, interactuar es atacar) con el cada frame (update). 
        }

        if (EventSystem.current.IsPointerOverGameObject()) //Si la posicion del "mouse" está sobre un objeto de juego, entonces... (
                                                           //Cabe aclarar que esto no impide que nuestro jugador se desplace hacia la posicion de otros objetos de juego (Como un enemigo o un item)...
                                                           //¿Por qué? bueno, esto es debido a que el mouse posee propiedades 2D, y por ende, este condicional se activará SÓLO con objetos 2D (Debido a que son los únicos en donde se puede evaluar la pos del mouse)
                                                           //Y los objetos 2D son los elementos de interfaz.
        {
            return; //regrasamos al inicio del método "Update"
        }
        //En cada frame, verifica si el jugador hace click izquierdo o derecho.

        DesplazarseMouseIzq(); //Desplaza el jugador hacia la posicion deseada.             
        InteractuarMouseDer(); //Interactua con el objeto en cuestión.
        #endregion



    }
   
    #region "Botón Mouse"
    private void DesplazarseMouseIzq()
    {
        if (Input.GetMouseButtonDown(0)) //Si se presiona el botón izquierdo del mouse...
        {



            Ray rayo = camara.ScreenPointToRay(Input.mousePosition); //Método que establece los puntos en los que un rayo se generará (Raycast) - desde la posicion de mi camara, hasta la posición del mouse.
            RaycastHit colision; //Variable que almacenará de los valores del objeto con el que colisione el rayo. 
                                 //RayCastHit es una variable que obtiene información de un método "RayCast".
                                 // Más info, here: https://docs.unity3d.com/ScriptReference/RaycastHit.html

            if (Physics.Raycast(rayo, out colision, 100, mascaraMovimiento)) //Si el rayo que se genera (A través del método Raycast), desde y hasta las posiciones de mi variable "rayo", colisiona con un objeto a una máxima
                                                                             // distancia de 100, y este posee un "layer" del tipo "Suelo" (mascaraMovimiento), entonces...
                                                                             //Cabe aclarar que "out colision" es un argumento que almacena las propiedades del objeto con el que colisionó mi rayo, en la variable " RaycastHit colision"
            {

                movimiento.MoverHaciaPos(colision.point); //Accedo a mi variable de clase "MotorJugador" y ejecuto el método "MoverHaciaPos" que toma como argumento, la posicion del objeto que colisiona con el rayo que se lanza
                                                          // desde la cámara, hacia donde el mouse haga click.  
                                                          //RESUMEN: Mueve al jugador hacia el lugar donde le da click izquierdo.                 

                RemoverObjetivo(); //Cuando el jugador hace click izquierdo para desplazarse por el mapa, remueve al objetivo actual para impedir que el jugador continue siguiendolo.
                                   //Dejar de enfocarse en algún objeto.
            }
        }
    }
    private void InteractuarMouseDer()  //Si se presiona el botón derecho del mouse... (Condicional que acciona el modo "Objetivo" de nuestro jugador).
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray rayo = camara.ScreenPointToRay(Input.mousePosition); //Método que establece los puntos en los que un rayo se generará (Raycast) - desde la posicion de mi camara, hasta la posición del mouse.                                                            
            RaycastHit colision;

            if (Physics.Raycast(rayo, out colision, 100)) //Si el rayo que se genera (A través del método Raycast), desde y hasta las posiciones de mi variable "rayo", colisiona con un objeto a una máxima
            {                                             // distancia de 100, entonces...
                                                          //Cabe aclarar que "out colision" es un argumento que almacena las propiedades del objeto con el que colisionó mi rayo, en la variable " RaycastHit colision"                

                var Objetivointeractivo = colision.collider.GetComponent<Interactivo>(); //Almaceno en una variable de clase "Interactivo" (Uso var para evitar redundancia), llamada "Objetivointeractivo"
                                                                                         //Las propiedades del componente "Interactivo" del objeto seleccionado. (Es decir, el que colisiona con el raycast)
                                                                                         //Esta variable permite saber si el objeto que ha sido "seleccionado" es "interactivo" o no.

                if (Objetivointeractivo != null) //Si mi variable "Objetivointeractivo" no es nula - Es decir, si se le asignó a mi variable los componentes de Interactivo (Lo que significa que el obj es interactivo)
                {
                    Enfocar(Objetivointeractivo); //Entonces, ejecuto mi método "Enfocar" que toma como argumento mi "Objetivointeractivo"
                }

                //Si sí, entonces enfoquémoslo. 
            }
        }

    }
    #endregion

   
    #region "Enfoque"
    void Enfocar(Interactivo nuevoObjetivo) //Método que establece un objetivo (Tipo objeto, no mision), y que toma como argumento todo objeto que posea la clase "Interactivo".
    {
        if (nuevoObjetivo != objetivo) //Si mi nuevo objetivo no es el objetivo actual, entonces...
        {
            if (objetivo != null) //Y si este objetivo actual no es nulo...
            {
                objetivo.Desenfocado(); //Desenfocamos el objetivo anterior.
            }

            objetivo = nuevoObjetivo; //Establezco que mi objetivo, será el objeto seleccionado. (Solo existe para verlo en el inspector)

            if (objetivo.GetComponent<Enemigo>() != null) //Si mi objetivo es un enemigo (Es decir, si el objetivo tiene una clase de "Enemigo", entonces...
            {
                enemigo = objetivo.GetComponent<Enemigo>(); //Mi varialbe enemigo almacenará las propiedades y características del componente del objetivo.
                barraHabilidades.SetActive(true); //Si mi objetivo es un enemigo, entonces activo la interfaz de la barra de habilidades.

                if (!autoAtaque) //Si autoAtaque es falso (Es decir, que no esté realizando autoAtaque), entonces...
                {
                    autoAtaque = true; //Autoataque será verdadero.
                }                

            }

            else //Si mi objetivo no ntiene componente de enemigo...
            {
                enemigo = null; //entonces enemigo sera nulo.
            }
                

            movimiento.SeguirObjetivo(nuevoObjetivo); //Accedo a mi variable "movimiento" (Que posee las propiedades de la clase "MotorJugador" y ejecuto el método "SeguirObjetivo"
                                                      // el cuál tomará los valores de mi argumento "nuevoObjetivo".
        }


        if (objetivo != null) //Si mi objetivo no es nulo...
        {
            objetivo.Enfocado(transform); //Cuando interactue con mi objetivo, este correrá el método "Enfocado" que utiliza como argumento la posición de mi jugador (Transform).
        }

        else
        {
            autoAtaque = false;
        }
        
       
    }
    void RemoverObjetivo() //Método que remueve la "atención" del objetivo.
    {
        if (objetivo != null) //Si tenemos un objetivo...
        {

            objetivo.Desenfocado(); //Ejecuta el método de mi objetivo.
        }

        enemigo = null; //Enemigo será igual a nulo (Si volvemos a enfocar a un enemigo, entonces mi variable obtendrá su componente, pero al desenfocarlo, lo borrará)
        autoAtaque = false; //No habra autoataque
        barraHabilidades.SetActive(false); //Y mi barra de habilidades será escondida (Porque no hay objetivo)

        objetivo = null; //Establece mi objetivo como nulo. (Solo existe para saber quien es el objetivo, en el inspector.
        movimiento.DejaDeSeguirAlObjetivo();//Accedo a mi variable "movimiento" (Que posee las propiedades de la clase "MotorJugador" y ejecuto el método "DejarDeSeguirAlObjetivo").
                                           
    }
    #endregion
}
