using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HabilidadesJugador : MonoBehaviour
{
    public static HabilidadesJugador instancia;


    [SerializeField] public Habilidad[] todasHabilidades; //Arreglo que almacena TODAS las habilidades del jugador.

    [SerializeField] public Habilidad[] habilidadesEnBarra = new Habilidad[4]; //Arreglo que contiene las configuraciones de las habilidades que posee el jugador en su barra.    
    [SerializeField] GameObject barraHabilidades; //Variable que en el inspector, obtendra todos los componentes VISUALES de mi barra de habilidades.
    [SerializeField] GameObject[] botonesHabilidades; //Arreglo que contendrá los botones de todas las habilidades.


    [SerializeField] GameObject prefabPregunta; //Arreglo que contendrá los botones de todas las habilidades.
    [SerializeField] GameObject textoNoHayMana; //Aquí almacenaré el prefab que muestra en pantalla el texto "no hay suficiente Mana...".

    GameObject pregunta; //Objeto de juego que servirá para instanciar las preguntas de las habilidades.

    StatsPersonajes statsJugador; //Variable que almacena los stats del jugador.

    ControladorJugador interaccionDelJugador;

    [SerializeField] GameObject efectoHabilidad; //Variable que en el inspector obtendrá el prefab del icono y texto que se presenta cuando se utiliza una habilidad que se disipa con el tiempo.
    [SerializeField] GameObject canvasUI; //Variable que en el inspector obtendrá el prefab del icono y texto que se presenta cuando se utiliza una habilidad que se disipa con el tiempo.


    public bool preguntaEstablecida; //Variable que almacena si la pregunta de la habilidad en cuestión ya ha sido establecida en patnalla.
    bool yaEjecutoHabilidad; //Variable que almacena si la habilidad ya se ha ejecutado.
    bool estaOcupado;


    float[] coolDownHabilidades; //Arreglo que almacenará el cooldown de todas las habilidades que poseea el jugador.
    float[] duracionHabilidades; //Arreglo que almacenará la duracion de la mayoría de las habilidades.
    GameObject[] iconosDuracionHabilidad; //Arreglo de objetos de juego que poseera todos los iconos de las habilidades que tengan propiedad de "duración".

    // Use this for initialization

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

    void Start()
    {
        
        iconosDuracionHabilidad = new GameObject[habilidadesEnBarra.Length]; //Establezco que el tamaño de mi arreglo de iconos de duración será igual al numero de elementos que haya en mi arreglo "habilidades".
        //Situación similar para los dos arreglos siguientes.
        coolDownHabilidades = new float[habilidadesEnBarra.Length]; 
        duracionHabilidades = new float[habilidadesEnBarra.Length];

        estaOcupado = false;

        ParametrosInicialesHabilidades();

        statsJugador = GetComponent<StatsPersonajes>(); //Obtengo los stats del jugador.
        interaccionDelJugador = GetComponent<ControladorJugador>();

        barraHabilidades.SetActive(false); //Y mi barra de habilidades estará desactivada.

        
    }

    // Update is called once per frame
    void Update()
    {

        if (interaccionDelJugador.enemigo != null) //Si mi variable enemigo no es nulo (Es decir, si hemos enfocado a un enemigo).
        {
            if (habilidadesEnBarra[0] != null)
            {
                EjecutarHabilidad1();
            }

            if (habilidadesEnBarra[1] != null)
            {
                EjecutarHabilidad2();
            }
           

            if (preguntaEstablecida) //Si la pregunta ya fue establecida.
            {

                for (int i = 0; i < habilidadesEnBarra.Length; i++)
                {
                    if (habilidadesEnBarra[i] != null)
                    {
                        if (habilidadesEnBarra[i].evaluandoRespuesta) //Si se esta evaluando la respuesta...
                        {
                            if ((int)habilidadesEnBarra[i].tipoDeHabilidad == 0)
                            {
                                //Si la respuesta es correcta, y ya fue respondida, y la habilidad no se ha ejecutado...
                                if (Pregunta_Armadura.instancia.esCorrecta && Pregunta_Armadura.instancia.respondida && !yaEjecutoHabilidad)
                                {
                                    Debug.Log("Generando efecto");
                                    habilidadesEnBarra[i].AñadirComponente(gameObject); //Añado el componente de comportamiento de la habilidad.
                                    barraHabilidades.SetActive(true); //Activo la barra de habilidades.
                                    PuenteEjecutaHabilidad(i);              //Ejecuto la habilidad       
                                    preguntaEstablecida = false; //Establezco que la pregunta ya no está establecida.
                                    habilidadesEnBarra[i].evaluandoRespuesta = false;
                                    estaOcupado = false;

                                    Destroy(pregunta); //Destruyo la pregunta.
                                }

                                if (!Pregunta_Armadura.instancia.esCorrecta && Pregunta_Armadura.instancia.respondida && !yaEjecutoHabilidad)
                                {
                                    Debug.Log("Armadura Incorrecta");
                                    ManejadorMusica.instancia.Reproducir("Respuesta Incorrecta");
                                    barraHabilidades.SetActive(true);
                                    preguntaEstablecida = false;
                                    habilidadesEnBarra[i].evaluandoRespuesta = false;
                                    estaOcupado = false;
                                    Destroy(pregunta);
                                }


                            }

                            else if ((int)habilidadesEnBarra[i].tipoDeHabilidad == 2)
                            {
                                //Si la respuesta es correcta, y ya fue respondida, y la habilidad no se ha ejecutado...
                                if (Pregunta_Sanacion.instancia.esCorrecta && Pregunta_Sanacion.instancia.respondida && !yaEjecutoHabilidad)
                                {
                                    Debug.Log("Generando efecto");
                                    habilidadesEnBarra[i].AñadirComponente(gameObject); //Añado el componente de comportamiento de la habilidad.
                                    barraHabilidades.SetActive(true); //Activo la barra de habilidades.
                                    PuenteEjecutaHabilidad(i);             //Ejecuto la habilidad       
                                    preguntaEstablecida = false; //Establezco que la pregunta ya no está establecida.
                                    habilidadesEnBarra[i].evaluandoRespuesta = false;
                                    estaOcupado = false;
                                    Destroy(pregunta); //Destruyo la pregunta.
                                }

                                if (!Pregunta_Sanacion.instancia.esCorrecta && Pregunta_Sanacion.instancia.respondida && !yaEjecutoHabilidad)
                                {
                                    Debug.Log("Sanacion Incorrecta");
                                    ManejadorMusica.instancia.Reproducir("Respuesta Incorrecta");
                                    barraHabilidades.SetActive(true);
                                    preguntaEstablecida = false;
                                    habilidadesEnBarra[i].evaluandoRespuesta = false;
                                    estaOcupado = false;
                                    Destroy(pregunta);
                                }


                            }
                        }
                    }
                   
                }

            }

        }

        else if (interaccionDelJugador.enemigo == null && preguntaEstablecida)
        {
            Destroy(pregunta);
        }


        ChecaCoolDownHabilidad(); //Método que en cada frame, checa si las habilidades que posee el jugador han sido activadas.

        EliminaEfectoHabilidades(); //Método que elimina el efecto de la habilidad, en el caso de que esta se disipe con el tiempo.
    }

    private void EjecutarHabilidad2()
    {
        if (Input.GetButtonDown("Habilidad2") && coolDownHabilidades[1] <= 0 && !estaOcupado) //Y se presiona el botón de la habilidad 1, entonces...
        {
            var manaComponente = GetComponent<Mana>(); //Obtengo el componente mana de mi jugador, y lo almaceno en una variable para poder acceder a sus métodos.

            float costeMana = habilidadesEnBarra[1].ObtenerCosteMana(); //Obtengo el coste de mana de la habilidad a realizar.

            if (manaComponente.AunHayEnergia(costeMana)) //Si aun hay maná...
            {
                manaComponente.UsarMana(costeMana); //Consume el mana del jugador por la cantidad que requiere la habilidad...
                habilidadesEnBarra[1].presionada = true;
                EstablecePregunta(); //Establece la pregunta de la habilidad...
            }

            else
            {
                Debug.Log("No hay maná");
                StartCoroutine(crearTextoNoHayMana()); //Inicia la corutina que muestre (Y después de varios segundos, destruye) la alerta visual de "No Hay Suficiente Maná".
            }

        }
    }

    private void EjecutarHabilidad1()
    {
        if (Input.GetButtonDown("Habilidad1") && coolDownHabilidades[0] <= 0 && !estaOcupado) //Y se presiona el botón de la habilidad 1, entonces...
        {
            var manaComponente = GetComponent<Mana>(); //Obtengo el componente mana de mi jugador, y lo almaceno en una variable para poder acceder a sus métodos.

            float costeMana = habilidadesEnBarra[0].ObtenerCosteMana(); //Obtengo el coste de mana de la habilidad a realizar.

            if (manaComponente.AunHayEnergia(costeMana)) //Si aun hay maná...
            {
                manaComponente.UsarMana(costeMana); //Consume el mana del jugador por la cantidad que requiere la habilidad...
                habilidadesEnBarra[0].presionada = true;
                EstablecePregunta(); //Establece la pregunta de la habilidad...
            }

            else
            {
                Debug.Log("No hay maná");
                StartCoroutine(crearTextoNoHayMana()); //Inicia la corutina que muestre (Y después de varios segundos, destruye) la alerta visual de "No Hay Suficiente Maná".
            }

        }
    }

    IEnumerator crearTextoNoHayMana()
    {
        var textoMana = Instantiate(textoNoHayMana, canvasUI.transform);
        float segundosEnPantalla = 2;
        yield return new WaitForSeconds(segundosEnPantalla);
        Destroy(textoMana);

    }

    private void EstablecePregunta() //Método que establece la pregunta de la habilidad, en la interfaz.
    {
        estaOcupado = true;
        preguntaEstablecida = true; //Establece que mi pregunta ya ha sido establecida.
        barraHabilidades.SetActive(false); //Esconde la barra de habilidades.


        for (int i = 0; i < habilidadesEnBarra.Length; i++ )
        {
            if (habilidadesEnBarra[i] != null)
            {
                if (habilidadesEnBarra[i].presionada)
                {
                    Debug.Log("Se presiono la habilidad: " + habilidadesEnBarra[i].nombre);
                    if ((int)habilidadesEnBarra[i].tipoDeHabilidad == 0) //Si el tipo de habiliad tiene ìndice 0 (Es decir, es de tipo "Armadura").
                    {

                        pregunta = Instantiate(prefabPregunta, canvasUI.transform); //Instancia el prefab de la pregunta, utilizando como parent el canvas.

                        Debug.Log("Agregando componente de pregunta de Armadura");

                        pregunta.AddComponent<Pregunta_Armadura>(); //Le añado el componente de  preguntas armadura.

                        var componentePregunta = pregunta.GetComponent<Pregunta_Armadura>(); //Almaceno las propiedades de su componente.

                        var eventoInput = pregunta.GetComponentInChildren<InputField>(); //Almaceno las propiedades de "Input"  de su child.

                        eventoInput.onEndEdit.AddListener(componentePregunta.ObtenerInput); //Agrego un evento de listener llamado "ObtenerInput", perteneciente a mi clase "preguntaArmadura".



                    }

                    else if ((int)habilidadesEnBarra[i].tipoDeHabilidad == 2)
                    {


                        pregunta = Instantiate(prefabPregunta, canvasUI.transform); //Instancia el prefab de la pregunta, utilizando como parent el canvas.

                        Debug.Log("Agregando componente de Sanación");

                        pregunta.AddComponent<Pregunta_Sanacion>(); //Le añado el componente de  preguntas armadura.

                        var componentePregunta = pregunta.GetComponent<Pregunta_Sanacion>(); //Almaceno las propiedades de su componente.

                        var eventoInput = pregunta.GetComponentInChildren<InputField>(); //Almaceno las propiedades de "Input"  de su child.                    

                        eventoInput.onEndEdit.AddListener(componentePregunta.ObtenerInput); //Agrego un evento de listener llamado "ObtenerInput", perteneciente a mi clase "preguntaArmadura".

                        Debug.Log("agregate loool");




                    }

                    habilidadesEnBarra[i].evaluandoRespuesta = true;
                    habilidadesEnBarra[i].presionada = false;

                }
            }
    
            
        }



        yaEjecutoHabilidad = false;

        



    }

    public void PuenteEjecutaHabilidad(int i)
    {

        yaEjecutoHabilidad = true;
        StatsPersonajes statsJugador = GetComponent<StatsPersonajes>(); //Obtiene los stats del jugador para los parametros de la habilidad a usar.
        EjecutaHabilidad(statsJugador, i); //Ejecutamos el método "RealizaHabilidad1" que toma como argumento los stats del enemigo enfocado.                
    }



    #region "Métodos y Cortutinas: Habilidades"

    private void ParametrosInicialesHabilidades()
    {
        for (int i = 0; i < habilidadesEnBarra.Length; i++) //Ciclo que establece todos los parametros iniciales de las habilidades.
        {
            if (habilidadesEnBarra[i] != null)
            {
                habilidadesEnBarra[i].inicializar();
                habilidadesEnBarra[i].AñadirComponente(gameObject); //Añade el comportamiento de la habilidad con indice "i"            
                coolDownHabilidades[i] = 0; //Establece inicialmente, que el cooldown de todas las habilidades es 0 (Para que los jugadores puedan usarlas desde el inicio de la pelea)
                                            //Una vez usadas, su coolDown será restuido al que las habilidades determinan.
            }

        }
    }

    private void EliminaEfectoHabilidades() //Método que ejecuta la corutina "EliminaEfectoHabilidad" en todas las habilidades 
    {
        for (int i = 0; i < habilidadesEnBarra.Length; i++)
        {
            if (habilidadesEnBarra[i] != null)
            {
                if (habilidadesEnBarra[i].ejecutandose && habilidadesEnBarra[i].seDisipaConTiempo) //Si mi habilidad 0 se está ejecutando, y mi el valor de mi habilidad 0 se disipa con el tiempo.
                {

                    StartCoroutine(EliminaEfectoHabilidad(statsJugador, i)); //Inicio la corutina que elimina el efecto de la habilidad en el jugador.
                    InstanciarIconoHabilidad(i);

                }
            }

        }
    }

    private void InstanciarIconoHabilidad(int i) //Método que crea el icono de la habilidad con indice "i". (Y que actualiza en cada frame, el texto de duración de la habilidad).
    {
        if (habilidadesEnBarra[i].iconoYaEstablecido == false) //Si mi habilidad aun no posee icono...
        {
            Debug.Log("Iconooos");
            iconosDuracionHabilidad[i] = Instantiate(efectoHabilidad, canvasUI.transform); //Instancio el prefab de los iconos de mi habilidad (Estableciendo su parent como el canvas) y lo almaceno en mi arreglo "iconosDuracion...".
            iconosDuracionHabilidad[i].GetComponentInChildren<Image>().sprite = habilidadesEnBarra[i].iconoEfecto; //Accedo a su componente de imagen, y la establezco con el icono de la habilidad.
            iconosDuracionHabilidad[i].GetComponentInChildren<Text>().text = duracionHabilidades[i].ToString(); //Acceso a su comp de texto, y la establezco con el elemento "i" de mi arreglo: "duracionHabilidades..."
            habilidadesEnBarra[i].iconoYaEstablecido = true; //Establezco que el icono de la habilidad [i] ya fue establecido.

        }

        else if (habilidadesEnBarra[i].iconoYaEstablecido) //Si mi icono ya fue establecido...
        {
            duracionHabilidades[i] -= Time.deltaTime;  //Resto la duración de la habilidad.
            iconosDuracionHabilidad[i].GetComponentInChildren<Text>().text = Mathf.Ceil(duracionHabilidades[i]).ToString();
        }
    }

    private void ChecaCoolDownHabilidad()
    {
        for (int i = 0; i < habilidadesEnBarra.Length; i++) //CiclO que verifica en cada habilidad, si su cooldown está activado... (Es decir, que ya se uso la habilidad)
        {
            if (habilidadesEnBarra[i] != null)
            {
                if (habilidadesEnBarra[i].coolDownActivado) //Si el coolDown de la habilidad i está activado...
                {
                    coolDownHabilidades[i] -= Time.deltaTime; //En cada frame, le resta el tiempo que se tarda en conformar el frame.
                                                              //Debug.Log("Habilidad: " + habilidades[i].nombre + ". Tiene un coolDown de: " + coolDownHabilidades[i]); //Im

                    botonesHabilidades[i].GetComponentInChildren<Text>().text = Mathf.Ceil(coolDownHabilidades[i]).ToString(); //Accede al componente de texto del botón de la habilidad, y lo actualiza con el coolDown de esta.

                    if (coolDownHabilidades[i] <= 0) //Si el cooldown de la habilidad i es menor a 0 (Lo que significa que el jugador ya puede volver a usar la habildiad)
                    {
                        EstablecerIconoHabilidad(i); //Vuelve a colocar el icono de la habilidad comod efault (Para que los jugadores vean que la habilidad ya puede ser usada de nuevo)

                        botonesHabilidades[i].GetComponentInChildren<Text>().enabled = false; //Desactiva el texto del coolDown (Porque ya no hay coolDown).

                        habilidadesEnBarra[i].coolDownActivado = false; //Desactiva el bool que indica si el coolDown sigue activo.

                    }
                }
            
            }
        }
    }

    private void EstablecerIconoHabilidad(int i)
    {
        Image[] componenteImagenEnBoton = botonesHabilidades[i].GetComponentsInChildren<Image>(); //Creo un arreglo de imagenes, que poseera todos los componentes de tipo "Imagen" que tenga mi botón.

        foreach (Image componenteImagen in componenteImagenEnBoton) //Después, por cada componente de tipo imagen, que haya en mi arreglo...
        {
            if (componenteImagen.gameObject.transform.parent != null) //Verifico lo siguiente: Si su parent TIENE componente de imagen...
            {
                componenteImagen.sprite = habilidadesEnBarra[i].iconoHabilidad; //Entonces, modifico su sprite con el sprite "iconoHabilidadPresionado" de la habilidad en cuestión.
            }
        }
    }

    private void EstablecerIconoHabilidadPresionado(int indiceHabilidad)
    {        
        Image[] componenteImagenEnBoton = botonesHabilidades[indiceHabilidad].GetComponentsInChildren<Image>(); //Creo un arreglo de imagenes, que poseera todos los componentes de tipo "Imagen" que tenga mi botón.



        botonesHabilidades[indiceHabilidad].GetComponentInChildren<Text>().enabled = true; //Activa el componente de texto del botón habilidad (Para mostrar el coolDown de esta)


        foreach (Image componenteImagen in componenteImagenEnBoton) //Después, por cada componente de tipo imagen, que haya en mi arreglo...
        {
            if (componenteImagen.gameObject.transform.parent != null) //Verifico lo siguiente: Si su parent TIENE componente de imagen...
            {
                componenteImagen.sprite = habilidadesEnBarra[indiceHabilidad].iconoHabilidadPresionada; //Entonces, modifico su sprite con el sprite "iconoHabilidadPresionado" de la habilidad en cuestión.                
            }
        }

        //El anterior proceso se realiza a causa de la siguiente situación: Si queremos modificar el sprite de botón, tenemos que asegurarnos de que estamos accediendo a su componente Imagen, y no al de sus parents.
        //Si no hicieremos ese arreglo y ciclo, estaríamos modificando la imagen del "parent" de mi botón (porque ".GetComponentsInChildren" también busca el componente en el parent.
    }

    public void EjecutaHabilidad(StatsPersonajes statsobjetivo, int indiceHabilidad) //Método que realiza los efectos de las habilidades, tomando como parametros los stats del objetivo, y el indice de la habilidad a realizar..
    {
        var manaComponente = GetComponent<Mana>(); //Obtengo el componente mana de mi jugador, y lo almaceno en una variable para poder acceder a sus métodos.

        float costeMana = habilidadesEnBarra[indiceHabilidad].ObtenerCosteMana(); //Obtengo el coste de mana de la habilidad a realizar.

        //TO-DO necestiamos el método "AunHayEnergia" lea el argumento desde las propiedades de la habilidad. TO-DO.
        if (manaComponente.AunHayEnergia(costeMana)) //Después evaluo si hay suficiente energía para castear la habilidad con mi método que me regresa un bool "AunHayEnergia".
        {

            //manaComponente.UsarMana(costeMana); //Accedo al componente de mana, y ejecuto el método "UsarMana" que toma como argumento el mana que necesita la habilidad (costeMana).            

            EstablecerIconoHabilidadPresionado(indiceHabilidad);

            var parametrosHabilidad = new parametrosHabilidad(statsobjetivo); //Creo una estructura llamado "parametrosHabilidad" que será igual a un nuevo constructo que toma los valors de "statsObjetivo".
            habilidadesEnBarra[indiceHabilidad].Usar(parametrosHabilidad); //Accedo al método usar de mi habilidad, que toma como parametro una estructura de variable "parametrosHabilidad".
            habilidadesEnBarra[indiceHabilidad].ejecutandose = true; //Establecemos que la habilidad 1 se está ejecutando. 
            habilidadesEnBarra[indiceHabilidad].coolDownActivado = true; //Establecemos que la habilidad 1 se está ejecutando. 
            coolDownHabilidades[indiceHabilidad] = habilidadesEnBarra[indiceHabilidad].coolDown0; //Establece el cooldown de la habilidad.
            duracionHabilidades[indiceHabilidad] = habilidadesEnBarra[indiceHabilidad].duracion; //La duración será igual a la duración de mi habilidad.

        }

        else
        {

            Debug.Log("No hay suficiente mana");
        }
    }



    IEnumerator EliminaEfectoHabilidad(StatsPersonajes statsobjetivo, int indiceHabilidad) //Corutina que elimina el efecto de la habilidad, después de que su duración (en segundos) ha pasado...
    {
        float duracion = habilidadesEnBarra[indiceHabilidad].ObtenerDuracionEfecto();
        var parametrosHabilidad = new parametrosHabilidad(statsobjetivo); //Obtengo los parametros de mi objetivo.
                                                                          //Obtengo su duración al acceder a las propiedades de su clase (En mi arreglo "habilidades").                        
        yield return new WaitForSeconds(duracion); //Establezco que después de los segundos de duración...


        habilidadesEnBarra[indiceHabilidad].EliminarEfecto(parametrosHabilidad); //Accedo al método "EliminarEfecto" que toma los parametros del objetivo.
        habilidadesEnBarra[indiceHabilidad].ejecutandose = false;                //Establezco que mi habilidad ya no se está ejecutando. (Debido a que su efecto se ha disipado).
        habilidadesEnBarra[indiceHabilidad].iconoYaEstablecido = false; //Establece que mi icono NO está establecido, porque el efecto de la habilidad ya se eliminó.
        Destroy(iconosDuracionHabilidad[indiceHabilidad]); //Elimina el gráfico del efecto de habilidad.
    }
}

    #endregion