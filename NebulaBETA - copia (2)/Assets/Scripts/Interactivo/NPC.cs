using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactivo {

    public string[] dialogo;
    public string nombre;

    public override void Interactuar()
    {


        SistemaDialogo.Instancia.AñadirNuevoDialogo(dialogo, nombre); 
        base.Interactuar();

    }
}
