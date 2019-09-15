using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")] //Línea que me permite crear un menu en el editor de Unity, para crear scriptable objects de este tipo...
                                                                         //La opción para crearlo se llama "Item" y se encuentra en el submenu "Inventario"
                                                                         //Al crearlo, creará un scriptable object igual a este, con el nombre de "Nuevo Item".

public class Item : ScriptableObject, IDescriptible { //"Blueprint" que tendrá las propiedades base de todos los items del juego.

    public string nombre = "Item Nuevo"; //El nombre del Item
    public Sprite icono = null; //Su icono
    public bool esDefault = false; //Y si este forma parte de los objetos por default del pj.

    public virtual void Usar() //Método (Virtual, ya que cada objeto puede tener un uso distinto) que genera el proceso de "Usar".
        //Podremos sobreescribirla al crear una clase derivada de "Item".
    {

    }

    public void RemoverDelInventario()
    {
        Inventario.instancia.Remover(this);
    }

    public string ObtenerDescripcion()
    {
        return "Soy un item";
    }
}
