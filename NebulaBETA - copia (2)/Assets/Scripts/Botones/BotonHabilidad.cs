using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotonHabilidad : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] GameObject interfazHabilidad;

    public int indiceHabilidad;

    public void OnPointerClick(PointerEventData eventData) //Función que se ejecuta cuando presiono un botón con este script.
    {
        if (eventData.button == PointerEventData.InputButton.Left) //Si el botón de mi evento es igual al botón izquierdo de mi mouse...
        {
            //Accedo a mi "ScriptSeleccionar" y ejecuto el método "AgarrarDesplazable" que toma como argumento, la habilidad con un determinado indice, de mi script "HabilidadesJugador".
            //Recordemos que la clase "Habilidad" (La cual está en el arreglo "habilidades") también deriva de la interfaz "IDesplazable". 
            //He ahí la razón por la que puede ser argumento para el método.
            //Debug.Log("La Habilidad es: " + HabilidadesJugador.instancia.habilidades[indiceHabilidad].nombre);
            ScriptSeleccionar.instancia.AgarrarDesplazable(HabilidadesJugador.instancia.todasHabilidades[indiceHabilidad]);
            interfazHabilidad.SetActive(true);


        }
    }
}
