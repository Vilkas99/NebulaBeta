using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour {

   //Variables públicas.
    public Transform objetivo; //Variable pública que me permite vincular a la cámara, el elemento que seguirá (En este caso, el jugador)
    public Vector3 distanciaObjetivo; //Offset de la cámara con el elemento que sigue (Su objetivo - El jugador)

    public float velocidadZoom = 4f; //Velocidad del zoom cuando se mueva la "ruedita" del mouse.
    public float minZoom = 5; //Mínimo zoom que puede haber
    public float maxZoom = 15; //Máximo

    public float alturaObjetivo = 2f; //Altura del objetivo (Inicializada con la altura del personaje del jugador).
    public float velocidadRotacion = 100f; //Velocidad a la que se podrá rotar al presionar cierta tecla.

   //Variables privadas
    private float zoomActual = 10f; //Zoom inicial
    private float rotacionActualAngulo = 0f; //Variable que almacena el ángulo de rotación actual.







    void Update()
    {
        zoomActual -= Input.GetAxis("Mouse ScrollWheel") * velocidadZoom; //El zoom inicial será el valor que el usuario provee al mover la ruedita, multiplicado por la velocidad del zoom. 
        zoomActual = Mathf.Clamp(zoomActual, minZoom, maxZoom); //Restringimos el valor del zoom entre su mínimo y máximo.
        rotacionActualAngulo -= Input.GetAxis("Horizontal") * velocidadRotacion * Time.deltaTime;
    }


    void LateUpdate() //Cumple con las mismas funciones del Update, pero se ejecuta después de este.
    {
        transform.position = objetivo.position - distanciaObjetivo * zoomActual; //La posicion de la cámara sera la resta de la posicion del objetivo, la distancia con este, y lo multiplicamos por el zoom.
        transform.LookAt(objetivo.position + Vector3.up * alturaObjetivo);  //"LookAt" es un método que modifica la rotación de las propiedades de "tranform" del objeto actual (En este caso, la cámara) con el fin de 
                                                                            //"mirar" siempre hacia su primer argumento - WorldPositon (Que en este caso, es "objetivo.position"), más la multiplicación de un Vector3.Up (Que es una forma abreviada de
                                                                            // colocar (0,1,0), y la altura de nuestro objetivo (Para que la cámara vea hacia el tronco superior del objetivo).

        transform.RotateAround(objetivo.position, Vector3.up, rotacionActualAngulo); //"RotateAround" es un método de transform, que modifica el eje (En este caso Y - Vector3.up) de nuestro objeto para que gire alrededor de otro objeto.
                                                                                     //Utiliza como argumentos la posicion que servirá punto de referencia para la rotación (Que en este caso, es la posicion del objetivo de la cámara)
                                                                                     // El eje que se quiere modificar (En este caso, Y -Vector3.up) 
                                                                                     //Y el ángulo. (rotacionActualAngulo)
    }
}
