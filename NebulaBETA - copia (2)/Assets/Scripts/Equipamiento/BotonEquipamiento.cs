using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonEquipamiento : MonoBehaviour {

    public enum SlotEquipamiento { Cabeza, Torso, Piernas, Arma, Escudo, zapatos}
    public SlotEquipamiento slot;
    public Sprite icono;

    public void Start()
    {
        
    }


    public void DesequiparObjetoActual()
    {
        ManejadorEquipamiento.instancia.desequiparBoton = true;
        ManejadorEquipamiento.instancia.Desequipar((int) slot);
    }

}
