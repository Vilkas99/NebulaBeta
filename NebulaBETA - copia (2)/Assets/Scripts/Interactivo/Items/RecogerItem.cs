using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerItem : Interactivo {

    public Item item; //Variable que me permitirá acceder a las propiedades de mi "ScriptableObject", "Item". (Se vincula a través del inspector).
    public bool conjuntoDeLoot;


    public override void Interactuar() //Sobrescribimos el método "Interactuar" de nuestra clase principal "Interactivo", que se ejecuta sólo si el jugador interactua con el objeto...
                                       // y si este está en su radio de interacción. (Para más detalles, ver línea 30, de la clase "Interactivo".)
    {

        if (conjuntoDeLoot)
        {
            GetComponent<ObjetoLoot>().AbrirVentanaLoot();
        }

        else
        {
            base.Interactuar(); //Ejecuta las líneas de código originales del método.

            Recoger(); //Ejecuta el método que recoge el item.
        }
    }

    void Recoger() //Método que recoge el item.
    {

        ManejadorMusica.instancia.Reproducir("Recoger Objeto");
        bool fueRecogido = Inventario.instancia.Añadir(item); //Accede a la instancia de la clase "Inventario", y ejecuta su método "Añadir", tomando como parámetro a nuestro item.        
        //Lo almacenamos en un "bool", ya que el método "Añadir" regresa un "bool" que nos indica si el objeto fue recogido o no, con base en el espacio de inventario que se tiene.

        if (fueRecogido) //Si "fueRecogido" es "true"...
        {
            Destroy(gameObject); //Destruimos el objeto de la escena, para que este solo se muestre en el inventario.
        }        
        
    }


}
