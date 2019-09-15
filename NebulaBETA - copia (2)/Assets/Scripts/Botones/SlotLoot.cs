using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotLoot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Image icono; //Variable que me permitirá modificar (En el inspector o cuando tomemos un objeto) el icono que aparecerá en el slot del inventario.    
    public Item objeto;  //Variable que almacena el objeto que tomamos.
    bool seleccionado = false;

    public void AgregarAlInventario() //Método que agrega el objeto al inventario (Y que utliza como argumento un objeto del tipo "Item").
    {
        //Debug.Log("Has looteado un: " + objeto.nombre);


        //Mi variable objeto ahora será el objeto recogido.
        Inventario.instancia.Añadir(objeto); //Añado el objeto al inventario.

        EsconderIcono(); //Método que esconde el icono del objeto (Ya que este ha sido agreagado al inventario)
        DesactivarBoton(); //Método que desactiva el botón, para que los usuarios dejen de presionarlo.
        objeto = null;
        var componenteRecompensa = GetComponentInParent<Recompensa>();
        componenteRecompensa.objetosNulos++;
    }

    public void OnPointerEnter(PointerEventData eventData) //Cuando el mouse pasa sobre el slot...
    {
        if (objeto != null)
        {
            //ToolTipUI.instancia.MostrarToolTip(transform.position); //Muestra el tooltip en la posición del slot.
        }
    }
     
    public void OnPointerExit(PointerEventData eventData) //Cuando el mouse deja de estar sobre el slot...
    {
        ToolTipUI.instancia.EsconderToolTip(); //Esconde el tooltip.
    }

    private void DesactivarBoton()
    {
        var componenteBoton = GetComponentInChildren<Button>(); //Obtengo el componente botón de mis elementos UI de loot...
        componenteBoton.enabled = false; //Establezco que su componente de botón estará desactivado para que el jugador no presione infinitas veces (Obteniendo con ello, infitios items).
    }

    private void EsconderIcono()
    {
        Image[] componentesImagen = GetComponentsInChildren<Image>(); //Almaceno los componentes imagen de todo mi objeto UI, con el fin de acceder al icono (Child de childs)

        foreach (Image nuevoIcono in componentesImagen) //Por cada componente imagen que haya en mi arreglo de componentes imagen...
        {
            if (nuevoIcono.gameObject.transform.parent.GetComponent<Image>()) //Verifico si su parent tiene un componente imagen...
            {
                //Si es así, entonces significará que he accedido al objeto "icono" (El cual es child de childs)
                nuevoIcono.enabled = false;  //Y como ya hemos recogido el objeto, el icono desaparece de la ventana de loot.
            }
        }
    }

    // Use this for initialization
    void Start () {
        if (objeto == null) //Si el slot no tiene objeto, entonces...
        {
            //Desactiva su icono y su botón.
            EsconderIcono();
            DesactivarBoton();

        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
