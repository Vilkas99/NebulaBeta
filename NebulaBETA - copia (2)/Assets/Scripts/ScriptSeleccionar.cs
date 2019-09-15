using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptSeleccionar : MonoBehaviour {

    #region "Singleton"
    public static ScriptSeleccionar instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }

    }
    #endregion

    public IDesplazable MiDesplazable { get; set;} //Creo una interfaz del tipo "IDesplazable".

    private Image icono; //Establezco una variable que almacenará el componente "Imagen" del objeto que poseea el script "ScriptSeleccionar".
    
    // Use this for initialization
	void Start () {
        icono = GetComponent<Image>(); //Al inicio, obtengo el componente imagen del objeto, y se lo asigno a mi variable "icono".
	}
	
	// Update is called once per frame
	void Update () {
        icono.transform.position = Input.mousePosition;
	}

    public void AgarrarDesplazable(IDesplazable objetoDesplazable) //Método que se ejecuta en el script "BotonHabilidad" (Cuando el bótón es presionado)
    {
        MiDesplazable = objetoDesplazable; //Establezco qu e mi interfaz "MiDesplazable" será igual al argumento que también es una interfaz "objetoDesplazable".
        icono.sprite = objetoDesplazable.MiIcono; //Establezco que el icono de mi objeto será el icono de la interfaz.
        icono.color = Color.white; //Y configuro su color a blanco para poder verlo en la escena.
    }

    public IDesplazable Colocar() //Esta función se encarga de "colocar" el elemento que estemos seleccionando en el slot adecuado.
    {
        IDesplazable temporal = MiDesplazable; //Establece que mi variable "MiDesplazabale" será almacenada en "temporal":

        MiDesplazable = null; //Posteriormente establezco que "MiDesplazabale" es nulo (Porque este ya fue desplazado hacia el slot)

        icono.color = new Color(0, 0, 0, 0); //Establezco el icono del elemento que estamos seleccionado como transparente 

        return temporal; //Regresa la interfaz temporal que posee el objeto que fue colocado.
       
    }
}
