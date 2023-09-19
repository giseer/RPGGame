using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;

    public bool Derrotado { get; private set; }
    public bool puedeSerCurado => Salud < saludMax;

    private BoxCollider2D miBoxCollider2D;

    private void Awake() 
    {
        miBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start() 
    {
        base.Start();
        ActualizarBarraVida(vidaActual:Salud, saludMax);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDano(cantidad:10);
        }    
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(cantidad:10);
        }    
    }
    public void RestaurarSalud(float cantidad)
    {
        if(Derrotado)
        {
            return; 
        }

        if (puedeSerCurado)
        {
            Salud += cantidad;
            if(Salud > saludMax)
            {
                Salud = saludMax;
            }

            ActualizarBarraVida(vidaActual:Salud, saludMax);
        }

    }
    protected override void PersonajeDerrotado()
    {
        miBoxCollider2D.enabled = false;
        Derrotado = true;
        EventoPersonajeDerrotado?.Invoke();
    }

    public void RestaurarPersonaje()
    {
        miBoxCollider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }
}
