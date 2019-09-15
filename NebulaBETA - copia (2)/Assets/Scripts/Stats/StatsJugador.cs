using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsJugador : StatsPersonajes {
  
    [Header("UI Stats")]
    [SerializeField] Text[] textoStats = new Text[4]; //Arreglo que almacena los objetosde texto, que presentarán los stats del jugador en la ventana de equipamiento.    

    //Serializae solo por debugging
    
    
    
    //NOTA: Un objeto heredaro de clase, JAMÁS puede ser singleton (Ya que al ser "hijo" de otra clase que se usa con otros pj -Véase enemigos- jamás será el único script de ese estilo en el juego).

    void Start ()
    {
        ManejadorEquipamiento.instancia.CambiosEquip += CambiosEnEquipamiento;
        TextoStatsUI();
        
    }

    private void Update()
    {
        TextoStatsUI();
    }

    private void TextoStatsUI() //Método que modifica el valor numerico de la interfaz de equipamiento, para que estos sean correspondientes con los stats del jugador.
    {
        textoStats[0].text = saludActual.ToString();
        textoStats[1].text = velocidad.ObtenerValor().ToString();
        textoStats[2].text = armadura.ObtenerValor().ToString();
        textoStats[3].text = daño.ObtenerValor().ToString();
    }

    //Este método se encarga de añadir los modificadres del nuevo item, y remover los del anterior, cada vez que se realice un cambio en el equipamiento del jugador.
    //Recordemos que este método es parte del delegado "CambiosEquip" que se encuentra en la clase "ManejadorEquipamiento".
    void CambiosEnEquipamiento (Equipamiento nuevoItem, Equipamiento itemAnterior)
    {
        if (nuevoItem != null)  //Si mi nuevo item no es nulo... (Es decir, si nos equipamos un objeto...)
        {
            armadura.AñadirModificador(nuevoItem.modArmadura); //Accede a mi variable "armadura" (Que se encuentra declarada en la clase padre "StatsPersonajes") y ejecuto su método "AñadirModificador"....
                                                               //que toma como argumento, el modificador de armadura que posee el nuevoItem.         
            daño.AñadirModificador(nuevoItem.modDaño);         //Mismo proceso que el anterior, pero con la variable "daño"-

            sanacion.AñadirModificador(nuevoItem.modSanacion);

            
        }

        if (itemAnterior != null) //Si mi item anterior no es nulo...
        {
            armadura.RemoverModificador(itemAnterior.modArmadura); //Remueve su modificador de armadura en el estado general del jugador. 
            daño.RemoverModificador(itemAnterior.modDaño); //Mismo proceso pero también con daño.
        }
    }

    public override void Muerte() //Sobreescribimos el método de muerte, que se ejecuta en la clase "StatsPersonajes"
    {
        base.Muerte();
        ManejadorJugador.instancia.MatarJugador(); //Y ejecutamos el método "MatarJugador" de mi clase "ManejadorJugador".
    }
}
