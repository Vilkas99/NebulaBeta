using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pregunta : SistemaDialogo
{

    [SerializeField] string[] respuestasTexto = new string[3]; //Arreglo que almacena las respuestas de la pregunta (Máximo 3)

    [SerializeField] Button[] respuestaBoton = new Button[3]; //Arreglo que almacena los botones que contienen las respuestas.

    [SerializeField] string textoRespuestaCorrecta; //Texto que sale cuando la respuesta es correcta.
    [SerializeField] string textoRespuestaIncorrecta; //Texto que aparece cuando la respuesta es incorrecta.

    [SerializeField] int indiceRespuestaCorrecta; //Variable que almacena el indice de la respuesta correcta (1,2 o 3)
    int indiceSeleccionada = 0; //Variable que almacena el indice que selecciona el jugador al hacer click sobre la respuesta.
    bool resuelta = false; //Booleano que nos indica si la pregunta ya fue resuelta.


    public override void CrearDialogo() //Sobrescribo mi método "CrearDialogo" para que este también ejecute el método "CrearTextoRespuesta".
    {
        base.CrearDialogo();
        CrearTextoRespuesta();
    }


    public void CrearTextoRespuesta() //Método que accede al componente texto de cada botón respuesta, y le asigna un texto equivalente al de las respuestas.
    {
        if (!resuelta)
        {
            continuar.interactable = false; //Al crear los botones de respuesta, desactivo el bótón de continuar.
        }
        
        
        for ( int indice = 0; indice < respuestasTexto.Length; indice++) //Ciclo que va por cada botón, modificando su texto.
        {
            respuestaBoton[indice].GetComponentInChildren<Text>().text = respuestasTexto[indice]; //Accedo al componente texto que se encuentra en un child suyo, y lo igual a la respuesta.
        }
    }



    public void presionaB1() //Método que se ejecuta en la propiedad "OnClick" del botón 1...
    {
        indiceSeleccionada = 1; //Al hacerlo, el índice se convierte en 1.
    }


    //Misma lógica con los demás métodos de presionar botón.
    public void presionaB2()
    {
        indiceSeleccionada = 2;
    }

    public void presionaB3()
    {
        indiceSeleccionada = 1;
    }


    private void Update()
    {
        if (!resuelta) //Si aun no se resuelve la pregunta...
        {
            VerificarSeleccion(); //Ejecuto el método que verifica el click del botón. 
        }

        else //DIALOGO DONDE EL NPC DECLARE QUE YA SE HABLÓ CON ÉL.
        {
            return;
        }
        

    }

    private void VerificarSeleccion() //Método que verifica si el indice del boton es el indice correcto.
    {
        if (indiceSeleccionada == indiceRespuestaCorrecta) //Si es así, entonces...
        {
            Respuesta(true); //Ejecuto el método que coloca el texto de respuesta correcta.
        }

        else if (indiceSeleccionada == 0)  //Si mi indice es 0 (Es decir, no se ha presionado ningún botón), entonces...
        {
            return; //No pasa nada.
        }

        else //Si entonces mi indice no es 0, pero tampoco es el correcto....
        {
            Respuesta(false); //Ejecuta el método de respuesta incorrecta.

        }
    }



    private void Respuesta(bool correcta)
    {
        continuar.interactable = true;
        EsconderBotones();        
        if (correcta)
        {
            dialogoTexto.text = textoRespuestaCorrecta;
        }

        else
        {
            dialogoTexto.text = textoRespuestaIncorrecta;
        }

        resuelta = true;
    }

    private void EsconderBotones()
    {
        for (int i = 0; i < respuestasTexto.Length; i++)
        {
            respuestaBoton[i].gameObject.SetActive(false);            
        }
    }
}
   
