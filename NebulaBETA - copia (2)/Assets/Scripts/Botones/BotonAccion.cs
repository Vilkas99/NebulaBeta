using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonAccion : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    Image iconoAccion;
    [SerializeField] int indiceHabilidad;

    // Use this for initialization
	void Start () {
        iconoAccion = GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick (PointerEventData datosEvento)
    {
        if (datosEvento.button == PointerEventData.InputButton.Left) //Si hacemos click con el botón izquierdo en el mouse.
        {
            if (ScriptSeleccionar.instancia.MiDesplazable != null && ScriptSeleccionar.instancia.MiDesplazable is Habilidad) //Si estamos llevando alguna habilidad con el mouse...
            {
                EstablecerHabilidad(ScriptSeleccionar.instancia.MiDesplazable as Habilidad);
            }
        }

    }


    public void EstablecerHabilidad(Habilidad habilidadAgregada) //Método que establece la habilidad en el botón correspondiente...
    {
        switch (indiceHabilidad) //Switch que checa que indice de habilidad tiene el botón (Si es el primero, segundo, tercero, o cuarto).
        {
            case 1: //Si el boton tiene un indice de 1... 
                {
                    VincularHabilidad(habilidadAgregada, 0);
                    break;
                }

            //Mismo caso para los demás índices.
            case 2:
                {
                    VincularHabilidad(habilidadAgregada, 1);
                    break;
                }

            case 3:
                {
                    VincularHabilidad(habilidadAgregada, 2);
                    break;
                }

            case 4:
                {
                    VincularHabilidad(habilidadAgregada, 3);
                    break;
                }

        }

        ActualizarGráfico();
    }

    private static void VincularHabilidad(Habilidad habilidadAgregada, int indice) //Método que vincula la habilidad tomada con el arreglo "habilidadesEnBarra".
    {
        Debug.Log("Estableciendo..." + habilidadAgregada.nombre);
        HabilidadesJugador.instancia.habilidadesEnBarra[indice] = habilidadAgregada; //La habilidad del indice 0 será igual a la habilidad agregada.
        HabilidadesJugador.instancia.habilidadesEnBarra[indice].inicializar();
    }

    public void ActualizarGráfico() //Método que actualiza el gráfico del botón cuando se le añade otra habilidad.
    {
        //El icono del botón, será el icono del objeto que estemos intentando colocar.
        iconoAccion.sprite = ScriptSeleccionar.instancia.Colocar().MiIcono;
        
        iconoAccion.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (HabilidadesJugador.instancia.habilidadesEnBarra[indiceHabilidad-1] != null)
        {
            Debug.Log("Toooltip");
            //ToolTipUI.instancia.MostrarToolTip(transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipUI.instancia.EsconderToolTip();
    }
}
