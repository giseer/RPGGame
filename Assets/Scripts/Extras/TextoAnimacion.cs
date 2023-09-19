using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dañoTexto;

    public void EstablecerTexto(float cantidad, Color color){
        dañoTexto.text = cantidad.ToString();
        dañoTexto.color = color;
    }
}
