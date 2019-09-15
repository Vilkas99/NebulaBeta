using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StatsPersonajes))]
public class SaludUI : MonoBehaviour { //Script que genera una barra de salud.

    public GameObject prefabUI; //Variable de acceso al prefab de la interfaz de salud.
    public Transform objetivo; //Variable de acceso que almacenará la posición en la que debe de crearse y actualizarse el ui. 
    float tiempoVisibilidad = 5; //Variable que establece el tiempo que el gráfico está visible cuando ya no recibe daño.

    float ultimaVisibilidadTiempo;  //Variable que almacena la última vez que el gráfico apareció.

    Transform ui; //Variable que tendrá los valores de la posición del prefab.
    Image deslizadorSalud; //Variable que almacenará el gráfico de salud. (La barra verde)
    Transform camara; //Variable que almacenará los datos de la pos de la cámara.
    
    
    // Use this for initialization
	void Start () {
        camara = Camera.main.transform;  //Vincula la pos de mi cámara principal. 
        foreach(Canvas c in FindObjectsOfType<Canvas>()) //Por cada canvas que encuentre en la escena.
        {
            if (c.renderMode == RenderMode.WorldSpace) //Si el canvas encontrado tiene su modo de renderizado como "WorldSpace", entonces...
            {
                ui = Instantiate(prefabUI, c.transform).transform; //Instancia el ui de salud en la posicion del canvas, y almacena toda su posicion en la variable "ui".
                deslizadorSalud = ui.GetChild(0).GetComponent<Image>(); //Obtiene la imagen de la barra verde, al obtener el componente imagen del chidl del prefab instanciado.
                ui.gameObject.SetActive(false); //Al momento de crear el gráfico, lo desactivamos porque queremos que este solo aparezca cuando recibimos daño.
                break; //Salimos del ciclo (Ya que este solo debe correrse 1 vez).
            }
        }

        GetComponent<StatsPersonajes>().cambiosSalud += cambioSalud; //Agrego a mi evento "cambiosSalud" el método que se crea en este script (cambioSalud).
		
	}
	
    void cambioSalud(int saludMax, int saludActual) //Método que modificara el gráfico de salud, de acuerdo a la salud actual del personaje.
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true); //Activa el gráfico de salud.
            ultimaVisibilidadTiempo = Time.time;

            float saludPorcentaje = saludActual / (float)saludMax; //Obtiene el porcentaje de salud, diviendo la salud actual entre la salud máxima. 
            deslizadorSalud.fillAmount = saludPorcentaje; //Actualiza el "fill" de la barra verde para que sea igual al porcentaje de la salud (Si salud porcentaje = 1, entonces el fill estará completo)
            if (saludActual <= 0) //Si la salud actual es de 0...
            {
                Destroy(ui.gameObject); //Destruye el ui del personaje.
            }
        }
    }

	// Update is called once per frame
	void LateUpdate () {

        if( ui != null)
        {
            ui.position = objetivo.position; //Actualiza la posicion del ui con el del objetivo. 
            ui.forward = -camara.forward; //Establece la orientacion del ui con la inversa de la cámara. 

            if (Time.time - ultimaVisibilidadTiempo > tiempoVisibilidad) //Si ha pasado más tiempo que el permitido (tiempoVisibildiad) desde que se actualizo la salud, entonces...
            {
                ui.gameObject.SetActive(false); //Lo establece como falso para que desaparezca, ya que significa que el jugador ya no está recibiendo daño.
            }
        }


		
	}
}
