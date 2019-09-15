using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recompensa : MonoBehaviour {

    //public Loot recompensa;
    public SlotLoot[] slots; //Arreglo que contiene los slots del gráfico del loot.
    public Loot items; //Variable que almacena el "loot" que se mostrará en la ventana-
    public Image logoEnemigoLooteado; //imagen del logo de la ventana de loot.

    public int objetosNulos = 0; //Variable que es modificada en los slots...    
    

	


    private void Start()
    {
        if (items != null) //Si mi clase recompensa si posee el loot en la variable items...
        {
            AñadirLootASlots(items);  //Ejecuta el método que vincula cada item del loot con los slots de la ventana de loot.
        }
    }

    // Update is called once per frame
    void Update () {

        if (objetosNulos == items.itemsLoot.Length) //Si mi variable "objetos nulos" equivale a la longitus de items que hay en 
        {

            DestruirVentanaYObjeto();
            
        }
	}


    public void DestruirVentanaYObjeto()
    {                     
        Destroy(transform.parent.gameObject);
    }

    public void AñadirLootASlots(Loot recompensa) //Método que añade a los slots de mi gráfico, los objetos de la recompensa del enemigo.
    {
        items = recompensa;
        logoEnemigoLooteado.sprite = recompensa.iconoEnemigoMuerto; //Establezco que el icono de looteo, será igual al icono del enemigo muerto de la recompensa.
                

        for (int i = 0; i < recompensa.itemsLoot.Length; i++) //Por cada item que haya en el llot (recompensa)...
        {
            Image[] componenteImagen = slots[i].GetComponentsInChildren<Image>(); //Almaceno todos los componentes de tipo imagen que encuentre en los elementos slot.
            slots[i].objeto = recompensa.itemsLoot[i]; //Establece que el objeto del slot es el item del loot.            

            //ACLARACIÓN: Realizo TODO este proceso para acceder a Slot/Boton[i]/icono (Es decir, acceder al child del child)...
            foreach (Image componente in componenteImagen) //Después verifico lo siguiente: Por cada componente del tipo imagen, que haya en mi arreglo de componentes...
            {
                if (componente.gameObject.transform.parent.GetComponent<Image>()) //Checho si su "parent" tiene un componente de tipo imagen.
                {   
                    //Si es así, entonces...
                    componente.sprite = recompensa.itemsLoot[i].icono; //El sprite del componente será el icono del item del loot.
                }   
            }            
        }
    }

    public void VincularLoot (Loot nuevoLoot) //Método que vincula el Loot del enemgio, con el loot que se mostará en la interfaz. (Este es llamado por el enemigo cuando muere)
    {
        items = nuevoLoot;
    }

    
}
