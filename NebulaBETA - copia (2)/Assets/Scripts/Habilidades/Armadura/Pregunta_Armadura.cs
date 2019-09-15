using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pregunta_Armadura : MonoBehaviour {

    //En las habilidades de armadura, se le harán preguntas al jugador de acuerdo a los siguientes temas de física: 

    //Lvl 1:
    // 1) Sistema de Unidades (Masa)
    // 2) Movimiento (Resistencia de Fluidos) 
    // 3) Suma y resta


    public bool esCorrecta; //Booleano que almacena si la respuesta es correcta.
    public bool respondida;  //Booleano que almacena si la pregunta ha sido respondida.

    string pregunta; //Variable que almacena la respuesta del jugador.
    int resultado; //Variable que almacena el resultado.

    int indicePregunta; //Variable que determina que pregunta se realizará. (Su índice)



    public void ObtenerInput (string respuesta) //Método que se agrega como "Listener" en "HabilidadesJugador".
        //Este se ejecuta cuando el jugador presiona "Enter" en el campo "Input".
    {
        respondida = true; //Al presionar enter, significa que el jugador ha respondido la pregunta.
        EvaluarRespuesta(respuesta); //Posteriormente, ejecuta el método que la evalua.         
        
                
    }
     
    
    public bool EvaluarRespuesta (string respuesta) //Método que evalua la respuesta, y regre un booleano.
    {        

        if (respuesta == resultado.ToString()) //Si la respuesta es igual al resultado (convertido a texto)
        {
            esCorrecta = true; //Entonces es correcta.
            return esCorrecta;
        }

        //Si no es igual al resultado...
        esCorrecta = false; //Entonces es falsa (incorrecta)
        return esCorrecta;
    }


    private void Suma()  //Método que establece la pregunta como suma.
    {
        int indiceTipoDeSuma = Random.Range(1,3);
        //Establezco las dos variables (con un valor al azar entre 2 y 200) que serán los factores de mi suma.

        if (indiceTipoDeSuma == 1) //Suma de 2 digitos
        {
            int x = Random.Range(1, 200);
            int y = Random.Range(1, 200);

            pregunta = "Cual es el resultado de la siguiente operación: " + "\n" + x.ToString() + " + " + y.ToString(); //Establezco el texto de mi pregunta.

            resultado = x + y; //Establezco el resultado.
        }

        else  if (indiceTipoDeSuma == 2) //Suma de 3 digitos
        {
            int x = Random.Range(1, 50);
            int y = Random.Range(1, 50);
            int z = Random.Range(1, 50);

            pregunta = "Cual es el resultado de la siguiente operación: " + "\n" + x.ToString() + " + " + y.ToString() + " + " + z.ToString(); //Establezco el texto de mi pregunta.

            resultado = x + y + z; //Establezco el resultado.
        }

        else if (indiceTipoDeSuma == 3) //suma de 4 digitos
        {
            int x = Random.Range(1, 30);
            int y = Random.Range(1, 30);
            int z = Random.Range(1, 30);
            int j = Random.Range(1, 30);

            pregunta = "Cual es el resultado de la siguiente operación: " + "\n" + x.ToString() + " + " + y.ToString() + " + " + z.ToString() + " + " + j.ToString(); //Establezco el texto de mi pregunta.

            resultado = x + y + z + j; //Establezco el resultado.
        }


    }

    private void Multiplicacion() //Método que establece la pregunta como suma.
    {
        //Establezco las dos variables (con un valor al azar entre 2 y 25) que serán los factores de mi suma.
        int x = Random.Range(2, 12);
        int y = Random.Range(2, 12);

        pregunta = "Cual es el resultado de la siguiente operación: " + "\n" + x.ToString() + " * " + y.ToString(); //Establezco el texto de mi pregunta.

        resultado = x * y; //Establezco el resultado.
    }


    // Use this for initialization

    #region "Singleton"
    public static Pregunta_Armadura instancia;
    private void Awake()
    {
        if (instancia != null)
        {
            Destroy(gameObject);
        }

        instancia = this;
    }
    #endregion


    void Start()
    {

        indicePregunta = Random.Range(1, 3); //Determina que el indice de la pregunta será un numero al azar entre (numero minimo de preguntas, y su máximo)        

        if (indicePregunta == 1) //Si el índice es 1, entonces...
        {
            
            Suma(); //Esta será la pregunta.
        }

        else if (indicePregunta == 2) //Si el índice es 2....
        {
            Multiplicacion();
        }

        GetComponentInChildren<Text>().text = pregunta;   //Vinculamos a mi pregunta con el componente texto que se encuentra en el child de mi prefab "pregunta".
    }

    // Update is called once per frame
    void Update()
    {

    }

}
