using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaduraHabilidadComportamiento : MonoBehaviour, IHabilidades {

    ArmaduraConfig configuracion;

    public void Configurar(ArmaduraConfig config) //Método que accede a los parámetros de la habilidad del tipo "ArmaduraConfig" que se le anexe en el argumento.
    {
        this.configuracion = config; //La configuración de este script, será igual a la config de los argumentos anexados.
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Componente Arm añadido");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Usar(parametrosHabilidad parametros) //Método que realiza la modificacion de los stats.
    {

        ManejadorMusica.instancia.Reproducir("Power Up"); //Reproduce el sonido de "Power Up".
        Debug.Log("Armadura base: " + parametros.armaduraBase + ". Usada por: "+ gameObject.name);
        int nuevaArmadura = configuracion.ObtenerArmaduraExtra(); //Nueva armadura será igual al método "ObtenerArmadura..." de mi variable "configuración".
        parametros.objetivo.armadura.AñadirModificador(nuevaArmadura); //Accedo a la armadura de mi objetivo a través de mi estructura de parametros, y ejecuto su método "AñadirModificador" que toma como argumento la "nuevaArmadura".
        float tiempoHabilidadUtilizada = Time.time;
    }

    public void EliminarEfecto(parametrosHabilidad parametros)
    {
        
        int nuevaArmadura = configuracion.ObtenerArmaduraExtra();
        parametros.objetivo.armadura.RemoverModificador(nuevaArmadura);
    }

    
}
