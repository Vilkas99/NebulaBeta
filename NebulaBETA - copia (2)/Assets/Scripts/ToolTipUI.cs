using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipUI : MonoBehaviour {

    [SerializeField] GameObject toolTip;


    public void MostrarToolTip(Vector3 posicion, IDescriptible descripcion)
    {
        toolTip.SetActive(true);
        toolTip.transform.position = posicion;
        toolTip.GetComponentInChildren<Text>().text = descripcion.ObtenerDescripcion();
    }

    public void EsconderToolTip()
    {
        toolTip.SetActive(false);
    }


    #region "Singleton"
    public static ToolTipUI instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
