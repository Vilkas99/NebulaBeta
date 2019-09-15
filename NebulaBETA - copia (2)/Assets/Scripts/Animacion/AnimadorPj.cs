using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimadorPj : MonoBehaviour {

    const float locomocionAnimacionSuavizadoTiempo = .1f;

    public AnimationClip animacionRemplazabaleAtaque;

    public AnimationClip[] animacionesAtacarDefault;
    protected AnimationClip[] animacionesAtacar;

    NavMeshAgent agente; //Variable que almacenará la propiedad NavMeshAgent de nuestro objeto. 
    protected Animator animador; //Variable que posee los componentes de la clase "Animator":
    protected CombatePersonaje combate; //Variable que nos permitirá acceder a las propiedades de la clase "CombatePersonaje"
    public AnimatorOverrideController controladorSobreescribir;
    public float velocidadPercentual;



    // Use this for initialization
    protected virtual void Start () {
        agente = GetComponent<NavMeshAgent>(); //Obtengo las propiedades de mi objeto.
        animador = GetComponentInChildren<Animator>(); //Obtengo las propiedades "Animator" del child de mi objeto. (Recordemos que el componente "Animator" se encuentra en el gráfico, y este script en el parent)
        combate = GetComponent<CombatePersonaje>();  //Obtiene la clase "CombatePersonaje" de los componentes del objeto.

        if (controladorSobreescribir == null)
        {
            controladorSobreescribir = new AnimatorOverrideController(animador.runtimeAnimatorController);
        }
        animador.runtimeAnimatorController = controladorSobreescribir;

        animacionesAtacar = animacionesAtacarDefault;

        combate.atacando += Atacando; //Agrego a mi delegado "atacando" mi método "Atacando" que establece las animaciones de combate.        
	}
	
	// Update is called once per frame
	 public virtual void Update () {
        velocidadPercentual = agente.velocity.magnitude / agente.speed; //Establezco un float que almacenará en cada frame, la velocidad actual de mi objeto, dividida entre su máxima velocidad
                                                                              // para asi, tener valores entre 0 y 1 (Que serán utilizados para la transición de las animaciones).


        animador.SetFloat("velocidadPercentual", velocidadPercentual, locomocionAnimacionSuavizadoTiempo, Time.deltaTime); //Utilizo el métosdo "SetFloat" que envía datos del tipo "float" al blendtree, llamado "velocidadPercentual"  //Este blendtree, de acuerdo a su valor entr 0 y 1, determina la animación que se ejecuta.
                                                                                                                           //El segundo parametro es el valor float que le envío a mi componente, en este caso, es la "velocidadPercentual", posteriormente le añado un suavizado (De valor -1f).
                                                                                                                           //EXPLICACIÓN:
                                                                                                                           //Mis tres animaciones (idle, caminar y correr) se ejecutan y transicionan, de acuerdo a un parámetro entre 0 y 1. 
                                                                                                                           //En cada frame (Update) obtenga la velocidad actual de mi pj, y la divido por la velocidad máxima. Esto significa que cuando mi pj alcance la velocidad máxima, será dividida por su velocidad máxima (Resultando en 1) 
                                                                                                                           //Cuando camine, tendrá un valor inferior a 1 (0.41567, por ejemplo). 
                                                                                                                           //Y cuando no haga nada, tendrá un valor de 0.
                                                                                                                           //Eso significa, que cuando mi personaje no tenga velocidad, el valor del animador será de 0, y por lo tanto, ejecutará la animación "idle",.










        animador.SetBool("enCombate", combate.enCombate); //Establece que la variable de animación "enCombate" tendrá un valor equivalente al de la variable bool "enCombate" de la clase "Combate"
        //Explicación: En mi clase "Combate" la variable "enCombate" se vuelve verdadera cuando se entra en combate, por lo tanto, al vincular mi variable de animación con mi variable de clase...
        //podemos ejecutar la animación de "CombateIdle" cuando el jugador entra en combate.   
    }

    protected virtual void Atacando() //Método que ejecuta la animacion de ataque.
    {
        animador.SetTrigger("atacar"); //Activa el "trigger" que permite las animaciones de ataque.
        int indiceAnimacionAtaque = Random.Range(0, animacionesAtacar.Length); //Crea un indice al azar para ejecutar distintas animaciones de ataque. 
        controladorSobreescribir[animacionRemplazabaleAtaque.name] = animacionesAtacar[indiceAnimacionAtaque]; //Sobreescribe la animación actual, por otra animación de ataque al azar (Por su índice)
    }
}
