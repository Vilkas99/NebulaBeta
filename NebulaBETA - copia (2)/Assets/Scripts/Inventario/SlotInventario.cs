using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotInventario : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {

    public Image icono; //Variable que me permitirá modificar (En el inspector o cuando tomemos un objeto) el icono que aparecerá en el slot del inventario.
    public Button botonRemover; //Variable que me permite acceder a los componentes de mi boton remover objeto.

    public Item objeto;  //Variable que almacena el objeto que tomamos.

    public void AgregarObjeto(Item nuevoObjeto) //Método que agrega el objeto al inventario (Y que utliza como argumento un objeto del tipo "Item").
    {
        objeto = nuevoObjeto; //Mi variable objeto ahora será el objeto recogido.

        icono.sprite = objeto.icono; //Modificaremos el icono que se mostrará, igualandolo al icono del objeto recogido. 
        icono.enabled = true; //Finalmente, activaremos el icono para su visualizació (Por default en el inspector, está desactivado)
        botonRemover.interactable = true; //Establecemos su interactuable como verdadero, para que este aparezca en la interfaz.
    }

    public void QuitarObjeto() //Método que quita el obj del inventario.
    {
        objeto = null; //Establece nuestro obj como nulo
        
        //Lo mismo con el sprite y su enabled.
        icono.sprite = null;
        icono.enabled = false;
        botonRemover.interactable = false; //Lo colocamos como falso, porque sin objetos, no hace falta tener un boton de remover. (Por lo tanto, desaparece de la interfaz).
    }

    public void BotonRemoverPresionado() //Método que se ejecuta cuando el botonRemover se presiona. 
    {
        Inventario.instancia.Remover(objeto); //Accede a la clase inventario, y remueve de su lista el objeto actual. 
                                              //Recordemos que el objeto de este Slot (Y de cada slot del inv) es asignado por el método "AgregarObjeto()" pero este es EJECUTADO en la clase "InventarioUI", cuando el...
                                              //método "ActualizarUI()" es ejecuado en el script "Inventario" a través de su delegado "cambioItemLlamar()", el cual es llamado (A través del método en el que se encuentra: "Añadir"...
                                              //en la clase "RecogerItem" en el método "Interactuar".
                                              //YEP, es un desmadre de conexiones.
    }

    public void UsarItem() //Este se ejecuta cuando se haga click en el slot (Esa vinculación se hace en el inspector con el componente "Button").
    {
        if (objeto != null) //Si hay in objeto en este slot...
        {
            objeto.Usar(); //Ejecuto su método "Usar()".
        }
    }


    public void OnPointerEnter(PointerEventData eventData) //Método que se ejecuta cuando el mouse para por encime del botón (Gracias a la implementación de la interfaz "IPointerEnterHandler".
    {
        if (objeto != null)
        {
            ToolTipUI.instancia.MostrarToolTip(transform.position, objeto);            
        } 

        
    }

    public void OnPointerExit(PointerEventData eventData) //Método que se ejecuta cuando el mouse deja de estar sobre el botón (Gracias a :  IPointerExitHandler).
    {
        ToolTipUI.instancia.EsconderToolTip();        
    }
}
