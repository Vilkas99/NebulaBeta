using UnityEngine;

public class InventarioUI : MonoBehaviour {

    public GameObject interfazInventario; //Variable que me permitira acceder a todos los gráficos de la interfaz (Para poder esconderlos o mostrarlos, dependiendo de lo que se presione)
    //Esta se vincula en el inspector.

    public Transform itemsParent; //Creo una variable pública que me permitira obtener la info y propiedades de mi objeto "itemsParent" (El cual tiene todos los slots de mi inventario)
                                  //Esta se vincula en el inspector.
    Inventario inventario; //Variable de cache, que almacena los métodos y propiedades de mi clase "Inventario".

    SlotInventario[] espacios; //Creamos un arreglo de objetos que tengan la clase "SlotInventario" llamada "espacios".

   

    

	// Use this for initialization
	void Start () {
        interfazInventario.SetActive(false);
        inventario = Inventario.instancia; //Accedo a las propiedades de mi inventario.
        inventario.cambioItemLlamar += ActualizarUI; //Almaceno el método "ActualizarUI" en mi delegado "cambioItemLlamar". (El cual se ejecuta cuando se recoge un item - Inventario ln48).

        espacios = itemsParent.GetComponentsInChildren<SlotInventario>(); //Mi arreglo espacios será igual a todos los childs de mi parent "itemsParent" que contengan la clase "SlotInventario"
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Inventario")) //Si se presiona el botón para "Inventario" (Agregué un "Input" extra en los parámetros de Unity, que vincula "Inventario" con la letra i.
        {
            ManejadorMusica.instancia.Reproducir("Abrir Inventario"); //Reproduce el sonido de abrir inventario.
            interfazInventario.SetActive(!interfazInventario.activeSelf); //Accede a mi objeto "interfazInventario", y le establece un valor bool (Con el método "SetActive"), que será el...
            ToolTipUI.instancia.EsconderToolTip();
            //inverso al que tiene. (Si estaba en false, al presionar i será true (Y se mostrará), y si estaba en true, al presionar i será "false" (Y desaparecerá).
        }
	}

    void ActualizarUI() //Método que actualiza la interfaz, cuando hay un cambio en ella (Agregar / quitar objetos).
        //Este analiza TODOS los slots que hay, y si encuentra un objeto en nuestra lista "inventario" con el mismo ID que nuestro slot, agrega el grafico correspondiente (con el método "AgregarObjeto").
    {
        for (int i = 0; i < espacios.Length; i++) //Ciclo que corre en cada slots (espacio) que tengas.
        {
            if (i < inventario.items.Count) //En cada iteracion, evalua si ya hemos recorrido todos los items que hay en nuestro inventario. 
                //En el caso de que no..
            {
                espacios[i].AgregarObjeto(inventario.items[i]); //En el espacio correspondiente, agrega el item correspondiente (Espacio 1 - Item 1), a través del métodos de...
                //la clase "SlotInventario" (Presente en mi objeto "espacios") llamado "AgregarObjeto", y agrega el item "i" de la lista de mi objeto "inventario".
            } else //Si la iteración es superior al numero de items que tenemos...
            {
                espacios[i].QuitarObjeto(); //"Limpia" (Establece gráficos vacios) los slots en donde ya no hay items.
            }
        }

    }
}
