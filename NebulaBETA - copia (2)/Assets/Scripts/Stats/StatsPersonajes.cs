
using UnityEngine;

public class StatsPersonajes : MonoBehaviour {


    #region Stats
    //Creo variables de clase "Stat" (Que ahora tendrán a su disposición la variable "valorInicial").
    public Stat saludMaxima; //Variable que almacena el valor de la "saludMaxima" del pj.
    public int saludActual { get; private set; } //Variable que puede ser llamada por cualquier otra clase (get), pero que solo puede ser modificada dentro de este script (private set)

    public Stat daño; //Variable con clase "Stat" que almacena los valores de daño.
    public Stat armadura; //Variable con clase "Stat" que almacena los valores de armadura.
    public Stat sanacion;
    public Stat velocidad;

    public event System.Action<int, int> cambiosSalud; //Evento que toma como parámetros la salud máxima del personaje (int) y la salud actual del mismo (int)

    #endregion



    private void Awake() 
    {
        saludActual = saludMaxima.ObtenerValor(); //Al inicio del juego, establecemos que la salud del personaje, será igual al valor que nos arroje el método...
                                                  //"ObtenerValor" de la variable "saludMáxima" que es de clase "Stat."
                                                  //En resumidas cuentas, al inicio, la salud del pj es igual a su salud máxima
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //Método para probar el daño que se le puede realizar al pj.
        {
            RecibirDaño(10);
        }


    }

    public void RecibirDaño (int daño) //Método que establece el daño que recibe el personaje.
    {
        daño -= (armadura.ObtenerValor())/3; //Restamos el daño que reciba el pj, con la armadura que este posee (Divido entre 3, para que la armadura no sea tan OP).
        daño = Mathf.Clamp(daño, 0, int.MaxValue); //Establecemos que el daño que puede recibir el pj, solo podra ser positivo (Entre 0 e infinito), ya que de ser negativo...
                                                   // Sanariamos al pj, porque en la línea sig, RESTAMOS su salud con el daño (- * - = +)
        

        saludActual -= daño; //Restamos su salud actual con el daño recibido.


        if (cambiosSalud != null) //Si mi evento si posee metodos.
        {
            cambiosSalud(saludMaxima.ObtenerValor(), saludActual); //Lo invoca, tomando como argumentos la salud maxima del personaje, y su salud actual.
        }

        if (saludActual <= 0) //Si su salud es igual o menor a 0...
        {
            
            Muerte(); //Ejecutamos el método que lidia con su muerte.
        }
    }

    public void RecibirSanacion(int sanacionRecibida) //Método que se encarga de brindar sanación al pj.
    {        
         //Creo una variable entera llamada "sanacionRecibida" que es igual al valor de mi variable tipo "Stat" sanación.
        sanacionRecibida = Mathf.Clamp(sanacionRecibida, 0, saludMaxima.ObtenerValor()); //Restringo la sanación entre 0 y la salud máxima del jugador. (Para evitar numeros demasiado grandes)
        saludActual += sanacionRecibida; //Aumento la salud actual del jugador, con el valor de la sanación.
        saludActual = Mathf.Clamp(saludActual, 0, saludMaxima.ObtenerValor()); //Restringo el valor de la salud actual, entre 0 y su máxima (Para que en el momento de sanar, su salud no supere su valor máximo)
        Debug.Log(transform.name + " recibió " + sanacionRecibida + " de sanación");

    }

    public virtual void Muerte() //Método virtual (Que será modificado, dependiendo del tipo de pj que muera) que establece el proceso de muerte del pj.
    {
        Debug.Log(transform.name + " ha muerto");
    }
}
