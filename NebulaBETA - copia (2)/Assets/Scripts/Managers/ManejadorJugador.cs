using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorJugador : MonoBehaviour {


    #region Singleton
    public static ManejadorJugador instancia;

    private void Awake()
    {
        instancia = this;
    }

    #endregion

    public GameObject jugador; 

    public void MatarJugador() //Método que ejecuta el proceso que "Mata" al jugador.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Volvemos a cargar la escena, haciendo uso del método de "SceneManager" llamado "LoadScene".
    }

}
