using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour {


    [SerializeField] Image barraMana; //Gráfico que tendrá nuestra barra de maná.
    [SerializeField] float maxMana = 10f;

    public float manaActual; 

    // Use this for initialization
	void Start () {
        manaActual = maxMana;
	}
	
	// Update is called once per frame
	void Update () {      

    }

    public void UsarMana(float cantidadDePuntos) //Método que resta puntos de mana cuando alguna acción se realiza.
    {        
        float puntosMana = manaActual - cantidadDePuntos; //Creo una variable que almacenará los puntos de mana después de que este es usado en la acción.
        manaActual = Mathf.Clamp(puntosMana, 0, maxMana); //Establezco entonces que mi mana actual, sérá eñ valor de "puntosMana" condicionado entre 0 (Para que no tenga valores negativos) y su valor max.

        ActualizarBarraMana(); //Método que actualiza la barra de mana (TO-DO!)
    }

    public bool AunHayEnergia (float cantidadDePuntos)
    {
        return cantidadDePuntos <= manaActual;
    }

    //TO-DO!
    private void ActualizarBarraMana()
    {
        float valorX = ManaPorcentaje();
        barraMana.fillAmount = valorX;
    }

    private float ManaPorcentaje()
    {
        return manaActual / maxMana;
    }
}

