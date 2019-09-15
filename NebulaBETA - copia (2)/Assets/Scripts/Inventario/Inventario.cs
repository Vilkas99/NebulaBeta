using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {

    public static Inventario instancia; //Variable estática que nos es útil para la creación de nuestro "Singleton".

    #region Singleton
    private void Awake()
    {
        if (instancia != null)
        {
            Debug.LogWarning("Más de una instancia de inventario en el juego!!");
        }
        instancia = this;
    }

    #endregion

    //Expliación CHINGONA de "Delegados": https://youtu.be/G5R4C8BLEOc
        //Un delegoda es un almacenador de métodos. 
    public delegate void CambioItem(); //En esta línea, creamos un delegado que almacenará métodos que no regresen nada (void) y que no requieran argumentos (Llamado "CambioItem")
    public CambioItem cambioItemLlamar; //Después, creamos un método del tipo "CambioItem" para acceder a los demás métodos que cumplan con las características del delegado.
    //Este delegado nos servirá para crear un "evento" cuando hagamos un cambio en nuestro inventario. 

    public int espacio = 20;
  

    public List<Item> items = new List<Item>(); //Establezco una lista que almacenara objetos que poseean la clase "Item", llamada "items".
	
    public bool Añadir (Item itemAñadido) //Método que se encarga de agregar items al inventario. Regresa un "bool"  porque queremos saber si se logró añadir el objeto al inventario...
                                          // para removerlo de la escena (Esto acontece en el script "RecogerItem".)
    
    {
        if (!itemAñadido.esDefault) //Si el objeto que queremos añadir al inventario, no es default....
        {
            if (items.Count >= espacio) // Si nuestro inventario está lleno...
            {
                Debug.Log("No hay espacio en el inventario"); //Mandamos a consola un aviso.
                return false; //Y el métodos será equiovalente a "false" porque no se logró recoger el item.
            }

            //Si tenemos espacio...
            items.Add(itemAñadido); //Añado a mi lista de items (A través de un método propio de las listas), el item con el que se interactuó (itemAñadido)
            if (cambioItemLlamar != null) //Si nuestro delegado SÍ TIENE MÉTODOS (Si no es nulo)
            {
                cambioItemLlamar.Invoke(); //Entonces, lo invocamos. (Lo que es lo mismo a ejecutar los métodos que contiene)
            }
            
        }

        return true; //El método será verdadero, porque se logró recoger el item.
    }

    public void Remover (Item itemRemovido) //Método que remueve el item del inventario.
    {
        items.Remove(itemRemovido); //Remueve al item de la lista.

        if (cambioItemLlamar != null) //Si nuestro delegado SÍ TIENE MÉTODOS (Si no es nulo)
        {
            cambioItemLlamar.Invoke(); //Entonces, lo invocamos. (Lo que es lo mismo a ejecutar los métodos que contiene)
        }
    }
}

