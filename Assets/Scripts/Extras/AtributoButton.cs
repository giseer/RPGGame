using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TipoAtributo{
    Fuerza,
    Inteligencia,
    Destreza
}

public class AtributoButton : MonoBehaviour
{

    public static Action<TipoAtributo> EventoAgregarAtributo;

    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtibuto(){
        EventoAgregarAtributo?.Invoke(tipo);
    }

}
