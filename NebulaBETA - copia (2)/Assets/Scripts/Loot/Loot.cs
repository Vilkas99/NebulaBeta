using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Loot", menuName = "Inventario/Loot")]
public class Loot : ScriptableObject {

    public Item[] itemsLoot;
    public Sprite iconoEnemigoMuerto;
	
}
