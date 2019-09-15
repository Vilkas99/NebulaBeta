using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemigo : StatsPersonajes {

    [Header("Loot")]
    [SerializeField] int experiencia;
    [SerializeField] Loot recompensaEnemigo;
    [SerializeField] GameObject objetoLoot;

    public override void Muerte()
    {
        base.Muerte();
        CrearLoot();

        Destroy(gameObject);

    }

    private void CrearLoot() //Método que crea el objeto de loot, cuando el enemigo muere.
    {
        var objetoLootCreado = Instantiate(objetoLoot, gameObject.transform.position, Quaternion.identity); //Instancio un objetoLoot, en la posicion del enemigo, y con una rotación de cero.
                                                                                                           //Además de que almaceno sus propiedades en "objetoLootCreado".
        var componenteLoot = objetoLootCreado.GetComponent<ObjetoLoot>(); //Obtengo su componente de tipo "ObjetoLoot" y lo almaceno en una variable (componenteLoot).
        componenteLoot.recompensa = recompensaEnemigo; //Después, establezco que la variable "recompensa" (Propia del componentr "ObjetoLoot" será igual a mi variable "recompensaEnemigo".

    }
}
