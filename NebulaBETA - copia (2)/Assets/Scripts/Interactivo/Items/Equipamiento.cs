using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Equipamiento", menuName = "Inventario/Equipamiento")]
public class Equipamiento : Item { //Clase del tipo "Scriptable Object" que deriva de mi clase "Item".

    public SlotEquipamiento tipoEquipamiento; //Enum público que permite escoger el tipo de equipamiento del item.
    public SkinnedMeshRenderer mesh; //Variable pública que almacenará (A través de la vinculación con el inspector) el gráfico del equipamiento-
    public RegionMeshEquip[] regionMeshCubierta; //Arreglo público de factores de nuestro enum, que nos permitirá establecer en el editor, las regiones Mesh que cubre nuestro objeto de equipamiento.

    public int modArmadura; //Variable que permite definir el valor de armadura que ofrece el equip.
    public int modDaño; //Variable que define el valor de daño que ofrece el equip.
    public int modSanacion;    

    public override void Usar()
    {
        base.Usar();
        ManejadorEquipamiento.instancia.Equipar(this); //Accedo a mi instancia de clase "ManejadorEquipamiento" y ejecuto el método "Equipar" dandole como argumento este objeto.
        RemoverDelInventario(); //Posteriormente, ejecuto el método "RemoverDelInventario" perteneciente a mi clase padre "Item".

    }

}

//Lo establezco fuera de la clase, con el fin de que sea usado por otros scripts sin tener que referenciarla.
public enum SlotEquipamiento {Cabeza, Torso, Piernas, Arma, Escudo, zapatos} //Enum que establece el tipo de equipamiento que compone al jugador. 

public enum RegionMeshEquip {Torso, Brazos, Piernas}; //Enum que refiere a los "BlenSheps" de nuestro jugador.
