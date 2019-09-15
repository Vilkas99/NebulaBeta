using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDialogo : MonoBehaviour {

    List<string> lineasDeDialogo = new List<string>(); //Lista que almacenará las líneas del díalogo de cualquier personaje que hable.
    public string nombreNPC; //Variable que almacena el nombre del objeto o NPC que hable.
    public GameObject uiDialogo; //Variable que accede a los elementos de la interfaz de dialogo.

    //Variables que se vinculan en el inspector, y que almacenarán el OBJETO que presenta el texto en pantalla.
    public Button continuar; //Este almacena el botón de "continuar" que posee nuestro cuadro de diálogo.
    public Text dialogoTexto, nombreTexto; //Estas almacenan el cuadro de texto para el diálogo, y el cuadro de text para el nombre.
    int indiceDialogo; //Almacena el dialogo que estamos mostrando.

   
    public static SistemaDialogo Instancia { get; set; }
    void Awake () {


        continuar.onClick.AddListener(delegate { ContinuarDialogo(); }); //Evento que ejecuta mi método "ContinuarDialogo" cuando el jugador haga click sobre el botón de "continuar".
        uiDialogo.SetActive(false);

        if (Instancia == null) //Si al inicio del juego, nuestra instancia es nula (Lo que debería ser...) 
        {            
            //Entonces...
            Instancia = this; //Instancia va a ser igual a esto.
        }

        else if (Instancia != null && Instancia != this) //Si mi instancia no es nula, y no es este objeto...
        {
            Destroy(gameObject); //Destruye el objeto.
        }
	}
   


    public void AñadirNuevoDialogo(string[] lineas, string nombreNPC) //Método que añade líenas de diálogo a nuestra lista. (Este se ejecuta en la clase "NPC".
    {


        indiceDialogo = 0;

        lineasDeDialogo = new List<string>(lineas.Length); //Creo una lista con longitud igual al numero de elementos (Lineas) que hay en mi arreglo de strings "líneas".

        lineasDeDialogo.AddRange(lineas);  //Añado el arreglo de las líneas de diálogo, a mi lista "lineasDeDialogo".        

        this.nombreNPC = nombreNPC;
        Debug.Log(this.nombreNPC);
        CrearDialogo();
    }

    public virtual void CrearDialogo()
    {
        dialogoTexto.text = lineasDeDialogo[indiceDialogo];
        nombreTexto.text = nombreNPC;
        uiDialogo.SetActive(true);
    }

    public virtual void ContinuarDialogo() //Método que modifica el indice de diáloigo, para mostrar las siguientes líneas.
    {
        if (indiceDialogo < lineasDeDialogo.Count - 1) //Si mi indice actual sigue siendo menor al número de líneas que hay en mi arreglo "lineasDeDialogo"...
        {
            indiceDialogo++; //Aumenta el indice por 1 (Para mostrar el siguiente diálogo)
            dialogoTexto.text = lineasDeDialogo[indiceDialogo]; //Establece mi variable "dialogoTexto" como el díalogo en el arreglo del índice actual.
        }

        else //Si mi indice actual es mayor al número de líneas de diálogo...
        {
            uiDialogo.SetActive(false);  //Desactiva el panel de diálogo. 
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

