using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("Habilidades/Sanación"))]
public class H_Sanacion : Habilidad {

    [Header("Valores de Sanación")]
    [SerializeField] int modVida = 5; //Variable entera que establece el modificador de armadura. (Inicializado como 1)

    public override void AñadirComponente(GameObject objetivo) //Método que añade el componente "Comportamiento" al gameObject objetivo (En la mayoría de los casos, el jugador).
    {
        var componenteComportamiento = objetivo.AddComponent<SanacionHabilidadComportamiento>(); //Creo una variable que almacenará el componente de comportamiento "ArmaduraHabilidad..."
        componenteComportamiento.Configurar(this); //Accedo al método configurar, y le establezco como argumento "this" (Esta clase: "ArmaduraConfig".
        iComportamiento = componenteComportamiento; //Posteriormente, relaciono mi interfaz "iComportamiento" con mi variable "componenteComportamiento". (Ya que "ArmaduraHabilidad..." deriva también de "interfaz").
    }

    public int ObtenerSaludExtra()
    {
        return modVida;
    }
}
