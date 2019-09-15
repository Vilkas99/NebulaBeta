using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoLoot : Interactivo {

    public Loot recompensa;
    [SerializeField] GameObject interfazLoot;
    public Transform canvasUI;
    GameObject uiLoot;
    bool ventanaLootInstanciada;


    public override void Interactuar()
    {
        if (!ventanaLootInstanciada)
        {
            AbrirVentanaLoot(); //Cuando interactuan con el loot, ejecuto el método que instancia la ventana de loot.
        }
        
        else if (ventanaLootInstanciada && uiLoot != null)
        {
            uiLoot.SetActive(true);
        }
    }

    public void AbrirVentanaLoot()
    {


        canvasUI = GameObject.Find("UI").transform; //Establezco que mi variable "canvasUI" será igual al objeto en el inspector de nombre "UI".
        uiLoot = Instantiate(interfazLoot, canvasUI); //Instancia la ventana de interfaz.

        int indice = uiLoot.transform.GetSiblingIndex(); //Obtengo el indice de mi ventana de loot en la jerarquia de objetos, cuando es creada.
        uiLoot.transform.SetSiblingIndex(indice - 1); //Después, modifico su índice para otros elementos se rendericen y sean visibles después de él (Como el tooltip).

        uiLoot.GetComponentInChildren<Recompensa>().VincularLoot(recompensa); //Accede al componente de recompensa de la ventana del loot, y ejecuta el método "VincularLoot"

        ventanaLootInstanciada = true; //Establece que la ventana ya se ha instanciado...
        StartCoroutine(DestruirDespuesDeTiempo()); //Inicia la corutina que destruye el loot después de un determinado tiempo...

    }

    private void VerificaLoot()
    {
        if (uiLoot != null) //Si nuestra ventana de loot ya fue instanciada (Es decir, no es nula...), entonces...
        {
            if (uiLoot.GetComponentInChildren<Recompensa>().objetosNulos == recompensa.itemsLoot.Length) //Y si ya se guardaron todos los objetos del loot...
            {                
                Destroy(gameObject); //Destruye el objeto de loot..
                Destroy(uiLoot); //Destruye la ventana ui de Loot...
                ToolTipUI.instancia.EsconderToolTip(); //Esconde el tooltip...
            }
        }

    }

    IEnumerator DestruirDespuesDeTiempo() //Destruye el objeto de loot después de 25 segundos....
    {
        yield return new WaitForSeconds(25);
        Debug.Log("Destruyendo el loot");
        Destroy(gameObject);
        Destroy(uiLoot);
    }

    //VILKAS del FUTURO, jamás se te ocurra crear otro método update en una clase que deriva de un script que YA POSEE un método de UPDATE.

    public override void Update()
    {
        base.Update();
        VerificaLoot();

    }

}
