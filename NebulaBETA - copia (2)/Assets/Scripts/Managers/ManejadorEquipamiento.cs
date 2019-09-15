using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManejadorEquipamiento : MonoBehaviour {

    #region Singleton
    public static ManejadorEquipamiento instancia;
    void Awake()
    {
        instancia = this;
    }

    #endregion

    public Equipamiento[] itemsDefault; //Arreglo público que me permitirá colocar (En el inspector), todos aquellos objetos del tipo "default".
    public SkinnedMeshRenderer meshObjetivo; //Variable pública que servirá como parent para las piezas de equipamiento (El parent es el mesh del jugador)
    public Equipamiento[] equipamientoActual; //Arreglo que almacenará el numero de elementos de equipamiento que tiene el pc.
    SkinnedMeshRenderer[] meshesActuales; //Arreglo que almacenra el número de meshes que poseen nuestros equipamientos.

    public delegate void CambiosEquipamiento(Equipamiento nuevoItem, Equipamiento itemAnterio); //Creo un delegado que almacenara métodos que utilicen como argumentos el item a equipar, y el item desequipado.
    public CambiosEquipamiento CambiosEquip; //Creo una variable para invocar a los métodos del delegado.
    public bool yaPoseeArmadura = false;
    public bool desequiparBoton = false;

    Inventario inventario;


    //UI Gráficos
    [Header("UI Equipamiento")]
    [SerializeField] Image[] iconosUI = new Image[5]; //Arreglo que almacenará las imagenes de los íconos de la interfaz del equipamiento. (En este caso, lo inicializamos como 5 porque solo hay 5 objetos s equipar en la interfaz.






    private void Start()
    {
        inventario = Inventario.instancia;

        int numSlots = Enum.GetNames(typeof(SlotEquipamiento)).Length; //Creamos un entero que almacernará la longitud (Numerica) del "Enum" "SlotEquipamiento".
        equipamientoActual = new Equipamiento[numSlots]; //Establecemos que nuestro arreglo "equipamientoActual" será igual a un nuevo arreglo de elementos que posean la clase...
                                                         //"Equipamiento", con una longitud igual al número de slots ("numSlots" que posee nuestro Enun "SlotEquipamiento".

        meshesActuales = new SkinnedMeshRenderer[numSlots]; 
        EquiparTodosDefaultItems(); //Ejecuta el método que se encarga de equipar los objetos por defautl del pj.
    }



    //Método que se ejecuta en la clase "Equipamiento", ln 16.
    //Este se encarga de equipar en el slot correcto, el item que seleccione el jugador. (Toma como argumento un objeto que poseea la clase "Equipamiento".
    public void Equipar (Equipamiento nuevoItem)
    {
        yaPoseeArmadura = true;
        desequiparBoton = false;

        int indiceSlot = (int)nuevoItem.tipoEquipamiento; //Obtengo el indicador de mi variable tipo Enum "tipoEqupamiento" que me indica que tipo de equipamiento es...
                                                          //(Cabeza = 0, Torso = 1, Hombro = 2....  (6.25)
        ManejadorMusica.instancia.Reproducir("Equipar Objeto");
        
        Equipamiento itemAnterior = Desequipar(indiceSlot);   //Creamos una variable referencia que almacenará el item que PUEDA encontrarse en el slot de equipamiento del nuevo item a equipar
                                                              //Decimos que es igal al método "Desequipar", debido a que este nos regresa el equipamiento que se encuentra actualmente en el slot.  

        if (equipamientoActual[indiceSlot] != null) //Si el slot a utilizar ya posee un objeto (Si no es nulo), entonces...
        {
            itemAnterior = equipamientoActual[indiceSlot]; //Establece que mi variable "itemAnterior" será el objeto que ocupe ese slot. 
            inventario.Añadir(itemAnterior); //Y ejecuta el método de mi inventario "Añadir" para que este regresa a la casilla del inventario. 
        }

        if (CambiosEquip != null) //Si existen métodos en mi delegado...
        {
            CambiosEquip.Invoke(nuevoItem, itemAnterior); //Lo invoco, utilizando el nuevoItem, y el itemanterior.
        }

        EstablecerBlenShapes(nuevoItem, 100); //Ejecuta el método que transforma el gráfico del jugador, utilizando como argumento el item equipado (nuevoItem), y el peso a disminuir (100) haciendo el gráfico del jugador más delgado.

        equipamientoActual[indiceSlot] = nuevoItem; //Agrego a mi arreglo "equipamientoActual" el "nuevoItem" en el slot ("indiceSlot") que le corresponde. 
        //Si es un casco, su indice enum será de 0, y al momento de agregarse al arreglo de "equipamientoActual", su posición en este también será la de 0 (La inicial).

        SkinnedMeshRenderer nuevoMesh = Instantiate<SkinnedMeshRenderer>(nuevoItem.mesh); //Instancia un nuevo mesh con el gráfico del item (nuevoItem.mesh)
        nuevoMesh.transform.parent = meshObjetivo.transform; //Y establezco su parent, que será el "meshObjetivo" (El gráfico default del jugador).

        //En las siguientes lineas, me encargo de que el comportamiento de los "Huesos" del gráfico del equipamiento, sea igual al comportamiento de los huesos del gráfico del jugador.
        nuevoMesh.bones = meshObjetivo.bones;
        nuevoMesh.rootBone = meshObjetivo.rootBone;

        meshesActuales[indiceSlot] = nuevoMesh; //Agrego en el respectivo indice, el mesh del item.

        ActualizarUI(nuevoItem, indiceSlot);
        
    }

    private void ActualizarUI(Equipamiento nuevoItem, int slot) //Método que actualiza la interfaz de equipamiento, donde analiza que tipo de equipamiento es, y que icono posee.
    {
        if (slot == 0)  //Si tiene un slot de  0 (Es decir, si es casco)
        {            
            if (nuevoItem.icono != null) //Y el icono del objeto a equipar no es nulo...
                iconosUI[slot].sprite = nuevoItem.icono; //El sprite del casco de la UI de equipamiento, será el icono del item a equipar.
        }

        //Mismo procedimiento para los demás slots de equipamiento.
        else if (slot == 1)
        {         
            if (nuevoItem.icono != null)
                iconosUI[slot].sprite = nuevoItem.icono;
        }

        else if (slot == 2)
        {            
            if (nuevoItem.icono != null)
                iconosUI[slot].sprite = nuevoItem.icono;
        }

        else if (slot == 3)
        {
            if (nuevoItem.icono != null)
                iconosUI[slot].sprite = nuevoItem.icono;
        }


        else if (slot == 4)
        {          
            if (nuevoItem.icono != null)
                iconosUI[slot].sprite = nuevoItem.icono;
        }

        else
        {
            Debug.LogWarning("El objeto " + nuevoItem.nombre + " no posee un 'tipo de equipamiento o no tiene slot de equipamiento'");
        }
    }

    public Equipamiento Desequipar(int indiceSlot) //Método que desequipa el objeto en el indice correspontiende.  (Por ende, toma como argumento el indice de slot)
    {
        if (equipamientoActual[indiceSlot] != null) //Primero, verificamos que haya un objeto en el indice de slot (Osease, que no sea nulo), y que este no sea default.
        {
            if (!equipamientoActual[indiceSlot].esDefault) //Si el objeto que estoy desequipando no es default...
            {
                BotonEquipamiento botonPresionado = EventSystem.current.currentSelectedGameObject.GetComponent<BotonEquipamiento>(); //Almaceno en un objeto del tipo "BotonEquipamiento" el botón que se presione para ejecutar el método "Desequipar"
                iconosUI[indiceSlot].sprite = botonPresionado.icono; //Establece el icono del UI como el ícono del boton presionado, para con ello, determinar en el UI que el slot está vacío.                                              
            }

            if (meshesActuales[indiceSlot] != null) //Si tenemos un "mesh" en ese slot...
            {   
                Destroy(meshesActuales[indiceSlot].gameObject); //Lo destruimos. (Ya que estamos desequipando el objeto, asi que queremos que ya no se vea en el jugador el objeto desequipado).
            }
            Equipamiento itemDesequipado = equipamientoActual[indiceSlot];  //Creamos una variable de clase "Equipamiento" que almacenará las propiedades del equipamiento a quitar.
            EstablecerBlenShapes(itemDesequipado, 0); //Método que modifica el "peso" del gráfico del jugador, tomando como referencia el objeto que se ha desequipado (itemDesequipado) y el peso del gráfico (Estableciendolo a default por el 0)
            inventario.Añadir(itemDesequipado); //Añade al inventario el equipamiento que se esta desequipando. 
            equipamientoActual[indiceSlot] = null; //Establece su slot como nulo (Porque ya lo hemos desequipado).     

            if (desequiparBoton) //Si mi bool "desequiparBoton" es verdadera (Es decir, el métofo "Desequipar" fue ejecutado al presionar un botón, entonces...
            {
                if (indiceSlot < itemsDefault.Length) //Y el indice del objeto a desequipar no sobrepasa la longitud de mi arreglo de itemsDefault (Por ejemplo; espada tiene un indice de 4, y mi arreglo tiene...
                                                      //una longitud de 3, entonces no ejecutará las lineas restantes porque no hay un itemDefault con el indice de espada (4).
                Equipar(itemsDefault[indiceSlot]); //Equipa el item default con el mismo slot del objeto que se desequipa (Pechera - Camisa, Casco - Cabello, etc...) 
            }
               


  

            if (CambiosEquip != null) //Si existen métodos en mi delegado..
            {
                CambiosEquip.Invoke(null, itemDesequipado); //Lo invoco, utilizando nulo (Como itemNuevo, porque no hay) y "itemDesequipado". 
            }
            return itemDesequipado; //Regreso el item que fue desequipado.
        }
        return null; //Si no había un item en ese slot, entonces regresa nulo.



    }

    public void DesequiparTodo() //Método que desequipará todos los objetos. 
    {
        for (int i = 0; i < equipamientoActual.Length; i++) //Creamos un ciclo que correrá en cada slot de equipamiento. 
        {
            Desequipar(i); //Y en cada slot, correrá el método "Desequipar" (tomando como argumento el indice del ciclo).
            //Ejemplo: Primer ciclo (i = 0), corre el método "Desequipar" utilizando i, lo que significa que desequipará el casco (Ya que este tiene indice 0) 
        }
        //Después de desequipar todos los objetos...
        EquiparTodosDefaultItems();  //Equipa en el jugador los objetos Default.
    }

    

    void EquiparTodosDefaultItems() //Método que se encarga de equipar los objetos "default" (Camisa, cabello, pantalón...) en el jugador...
    {
        foreach(Equipamiento item in itemsDefault) //Por cada objeto con clase "Equipamiento" que haya en mi arreglo "itemsDefault"...
        {
            Equipar(item); //Ejecutará el método "Equipar", que equipará cada item que encuentre en el arreglo.
        }
    }

    //El sig Método, corrige un error de gráfico que ocasiona que la piel del jugador se vea a través de la armadura.

    void EstablecerBlenShapes(Equipamiento item, int peso) //Este método ocupa como argumentos un item, y un entero que determinará el "peso" del gráfico del jugador.
    {
        
        foreach (RegionMeshEquip piezaCuerpo in item.regionMeshCubierta) //Al inicio, establezco un ciclo que corre por cada enum del tipo "RegionMeshEquip" que haya en la variable "regionMeshCubierta"
        {
            meshObjetivo.SetBlendShapeWeight((int)piezaCuerpo, peso); //Cada vez que lo hace, accede al gráfico de la piel de nuestro jugador (meshObjetivo), y ejecuta un método que modifica el peso del mesh seleccionado.
                                                                     //Para acceder al mesh, utiliza el índice de la pieza cuerpo que estamos cubriendo (Piernas = 0, Brazos = 1 ...) ya que están en el mismo orden que los...
                                                                     //"Blendshapes" del jugador, y establece su peso con el argumento. 
              //Modifica el peso del gráfico, con el fin de hacerlo más delgado, y así, evitar que este atraviese la armadura.                                         
        }
    }
}
