using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Línea que ocasiona que todas las variables dentro de este script, se puedan modificar en el inspector.
public class Stat { //Clase que posee el valor inicial de cualquier stat que creemos.
    //Esta no deriva de "MonoBehaviour" porque nunca estará anexada a un objeto. (Solo se utiliza para la declaración de variables en el script "StatsPersonajes".

    [SerializeField] private int valorInicial;  //Establezco una variable que se llama "valorInicial" que será modificada en el inspector.

    private List<int> modificadores = new List<int>();

    public int ObtenerValor() //Creo un método que me regresará el valor Final de todos los "stats" que creemos (Daño, armadura, salud, magia, etc...).
    {
        int valorFinal = valorInicial; //Establecemos que nuestro valor final es igual al valor inicial.
        modificadores.ForEach(x => valorFinal += x); //Después, ejecutamos el método "ForEach" (Método propio de las listas) que por cada elemento que haya en la lista (x), lo sumamos a la variable "valorFinal".
        //Por ejemplo; Tenemos 3 modificadores de armadura (1, 4, 5). Cada uno de estos valores será añadido a nuestro valor inicial de armadura (0 + 5 + 4 +1), para que las ventajas del objeto, se reflejen en el gameplay
        return valorFinal; //Regresamos el "valorFinal" para su uso en los procesos que se necesite.
    }

    public void AñadirModificador (int modificador) //Creo un método público que añade el valor entero (int) del modificador a la lista. (Este se ejecuta cuando se equipa un objeto)
    {
        if (modificador != 0)
        {
            modificadores.Add(modificador);
        }
    }

    public void RemoverModificador (int modificador) //Creo un método que remueve el valor entero del modificador, de la lista. (Este se desequipa cuando se ejecute un objeto).
    {
        if (modificador != 0)
        {
            modificadores.Remove(modificador);
        }
    }
}
