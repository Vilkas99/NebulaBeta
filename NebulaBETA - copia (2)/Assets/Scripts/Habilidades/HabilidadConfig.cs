using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public  struct parametrosHabilidad
{
    public StatsPersonajes objetivo;
    public int armaduraBase;

    public parametrosHabilidad (StatsPersonajes objetivo)
    {
        this.objetivo = objetivo;
        this.armaduraBase = objetivo.armadura.ObtenerValor();
    }
}
public abstract class Habilidad : ScriptableObject,  IDesplazable {

    [Header("General")]
    [SerializeField] public TipoDeHabilidad tipoDeHabilidad; //Enum que establece que tipo de habilidad se ejecuta .
    [SerializeField] public string nombre; //Nombre de la habilidad.
    [SerializeField] float costeMana = 5f; //El coste de mana que requiere al utilizarse.
    [SerializeField] public int coolDown0  = 5; //El coolDown para volverse a utilizar.    
    [SerializeField] public bool seDisipaConTiempo; //Bool que nos indica si el efecto de la habilidad se disipa con el tiempo.
    [SerializeField] public float duracion = 5f; //La duración del efecto.

    [SerializeField] public Sprite iconoEfecto; //Sprite del efecto.

    [SerializeField] public Sprite iconoHabilidad; //Sprite del efecto.
    public Sprite MiIcono { get { return iconoHabilidad; } }
    
    [SerializeField] public Sprite iconoHabilidadPresionada; //Sprite del efecto.


    public bool presionada = false;
    public bool evaluandoRespuesta = false;
    public bool ejecutandose = false;
    public bool coolDownActivado = false;
    public bool iconoYaEstablecido;

    //Protected: Ninguna clase puede acceder al método (Solo los derivados de esta - sus hijos -)
    protected IHabilidades iComportamiento; //Creo una interfaz del tipo "IHabilidades" llamada "iComportamiento"

    abstract public void AñadirComponente(GameObject objetivo);

    public void Usar(parametrosHabilidad parametros) //Método usar que toma como argumento un constructo del tipo "parametrosHabilidad".
    {

        iComportamiento.Usar(parametros); //Accede al método "Usar" de mi interfaz "iComportamiento", pasandole como argumento los parametros del método.
        //He de decir que los valores de "iComportamiento" se establecen en las clases derivadas a "Habilidad" (Como "Armadura" o "Sanación").
    }

    public void EliminarEfecto(parametrosHabilidad parametros)
    {        
        iComportamiento.EliminarEfecto(parametros);
    }

    public float ObtenerCosteMana() //Método que me regresa el valor del coste de mana de la habilidad.
    {
        return costeMana;
    }

    public float ObtenerDuracionEfecto() //Método que me regresa el valor del coste de mana de la habilidad.
    {
        return duracion;
    }


    public void inicializar()
    {        
        ejecutandose = false; //Establezco que la habilidad 0 no se está ejecutando (ejecutandose = false)          
        coolDownActivado = false; //Y que por ende, no tiene coolDown activo.
        iconoYaEstablecido = false;
        presionada = false;
        evaluandoRespuesta = false;
}
  
}

public interface IHabilidades
{
    void Usar(parametrosHabilidad parametros);

    void EliminarEfecto(parametrosHabilidad parametros);
}

public enum TipoDeHabilidad {Armadura, Daño, Sanacion, Velocidad} //Enum que establece el tipo de habilidad
