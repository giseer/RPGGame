using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float RegeneracionPorSegundo;

    public float ManaActual{get; set;}
    public bool SePuedeRestaurar => ManaActual < manaMax;

    private PersonajeVida miPersonajeVida;

    private void Awake() {
        miPersonajeVida = GetComponent<PersonajeVida>();
    }

    private void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();

        InvokeRepeating(nameof(RegenerarMana), 1,1);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(10f);
        }
    }

    public void UsarMana(float cantidad)
    {
        if (ManaActual >= cantidad)
        {
            ManaActual-= cantidad;
            ActualizarBarraMana();
        }
    }

    public void RestaurarMana(float cantidad){
        if (ManaActual >= manaMax)
        {
            return;
        }
        ManaActual += cantidad;
        if (ManaActual > manaMax)
        {
            ManaActual = manaMax;
        }

        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }

    private void RegenerarMana(){
        if (miPersonajeVida.Salud > 0f && ManaActual < manaMax)
        {
            ManaActual += RegeneracionPorSegundo;
            ActualizarBarraMana();
        }
    }

    public void RestablecerMana(){
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }

    private void ActualizarBarraMana(){
        UIManager.Instance.ActualizarManaPersonaje(ManaActual,manaMax);
    }

}
