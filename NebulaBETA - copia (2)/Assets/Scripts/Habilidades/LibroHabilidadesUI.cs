using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibroHabilidadesUI : MonoBehaviour {

    [SerializeField] GameObject interfaz;


    // Use this for initialization
    void Start()
    {
        interfaz.SetActive(false); //Al inicio del juego, quiero que la interfaz del equipamiento no aparezca en pantalla, hasta que el jugador presione la tecla E.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LibroHabilidades")) //Si se presiona el botón para "Inventario" (Agregué un "Input" extra en los parámetros de Unity, que vincula "Inventario" con la letra i.
        {
            ManejadorMusica.instancia.Reproducir("Abrir Equipamiento"); //Reproduce el sonido de abrir equipamiento.
            interfaz.SetActive(!interfaz.activeSelf); //Accede a mi objeto "interfaz", y le establece un valor bool (Con el método "SetActive"), que será el...
            //inverso al que tiene. (Si estaba en false, al presionar e será true (Y se mostrará), y si estaba en true, al presionar e será "false" (Y desaparecerá).
        }
    }
}
