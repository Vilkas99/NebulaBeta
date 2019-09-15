using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimadorJugador : AnimadorPj {

    public animacionesArmas[] animacionesArma;
    Dictionary<Equipamiento, AnimationClip[]> diccionarioAnimacionesArmas;

    protected override void Start() //Modifico el start ya establecido por mi clase "AnimadorPj", para anexar....
    {
        base.Start();
        ManejadorEquipamiento.instancia.CambiosEquip += CambioEquipamiento; //Agrego el método que establece la animación del "sostener espada y escudo" a mi delegado "CambiosEquip"...
                                                                            //El cual se ejecuta siempre que hay se equipa o desequipa (Cuando hay un cambio en el equipamiento) un objeto.

        diccionarioAnimacionesArmas = new Dictionary<Equipamiento, AnimationClip[]>();                                                                        
        foreach (animacionesArmas a in animacionesArma)
        {
            
            diccionarioAnimacionesArmas.Add(a.arma, a.clips);
        }
    }

    public override void Update()
    {
        base.Update();
        //Se supone que este condicional activa el sonido de pisadas, cuando se activa la animación de caminata o correr (Cuando velocidadPercentual), pero por alguna razón, el sonido se ejecuta cuando...
        //El jugador camina o corre, a pesar de que su velocidad percentual es mayor a 0.
        if (velocidadPercentual == 0) //NO ENTIENDO...
        {
            ManejadorMusica.instancia.Reproducir("Pisadas");//Accedo a mi manejador de música, y ejecuto su método "Reproducir" que toma como argumento el nombre del audio (en este caso: "Pisadas").

        }

        
    }

    //Método que establece la animación de "sostener espada y escudo".
    void CambioEquipamiento(Equipamiento nuevoItem, Equipamiento anteriorItem) //Utiliza como argumento el item nuevo a equipar, y el item que ya está equipado.
    {
       
        //Si se equipo una espada.
        if (nuevoItem != null && nuevoItem.tipoEquipamiento == SlotEquipamiento.Arma) //Si el nuevo item no es nulo, y el slot que ocupa es el de arma...
        {
            
            animador.SetLayerWeight(1, 1); //Accedemos al animador, entramos a su layer de indice 1 (Mano derecha) y colocamos su "peso" (O activación) como 1 (1 = Animacion completa).
           
            if (diccionarioAnimacionesArmas.ContainsKey(nuevoItem))
            {                
                animacionesAtacar = diccionarioAnimacionesArmas[nuevoItem];
            }
        }
        
        //Si se desequipo una espada.
        else if (nuevoItem == null && anteriorItem != null && anteriorItem.tipoEquipamiento == SlotEquipamiento.Arma) { } //Si el nuevo item es nulo, y el anterior no era nulo y era un arma...
        {            
            animador.SetLayerWeight(1, 0f); //Establece la animación como 0 (Nada).
            animacionesAtacar = animacionesAtacarDefault;
        }

        //Se realiza el MISMO proceso con el escudo.

        if (nuevoItem != null && nuevoItem.tipoEquipamiento == SlotEquipamiento.Escudo)
        {
            animador.SetLayerWeight(2, 1f);
        }
        else if (nuevoItem == null && anteriorItem != null && anteriorItem.tipoEquipamiento == SlotEquipamiento.Escudo) { }
        {
            animador.SetLayerWeight(2, 0f);
        }
    }

    [System.Serializable]
    public struct animacionesArmas
    {
        public Equipamiento arma;
        public AnimationClip[] clips; 
    }

	
}
