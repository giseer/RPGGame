using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersonajeAnimaciones : MonoBehaviour
{

    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    private Animator miAnimator;
    private PersonajeMovimiento personajeMovimiento;
    private PersonajeAtaque personajeAtaque;

    private readonly int direccionX = Animator.StringToHash(name:"X");
    private readonly int direccionY = Animator.StringToHash(name:"Y");
    private readonly int derrotado = Animator.StringToHash(name:"Derrotado");

    void Start()
    {
        miAnimator = GetComponent<Animator>();
        personajeMovimiento = GetComponent<PersonajeMovimiento>();
        personajeAtaque = GetComponent<PersonajeAtaque>();
    }


    void Update()
    {        
        ActualizarLayers();

        if(personajeMovimiento.enMovimiento == false){
            return;
        }

        miAnimator.SetFloat(direccionX, personajeMovimiento.DireccionMovimiento.x);
        miAnimator.SetFloat(direccionY, personajeMovimiento.DireccionMovimiento.y);

    }

    private void ActivarLayer(string nombreLayer){
        for (int i = 0; i < miAnimator.layerCount; i++)
        {
            miAnimator.SetLayerWeight(i, weight:0);
        }

        miAnimator.SetLayerWeight(miAnimator.GetLayerIndex(nombreLayer), weight:1);
    }

    private void ActualizarLayers()
    {
        if (personajeAtaque.Atacando)
        {
            ActivarLayer(layerAtacar);
        }
        else if (personajeMovimiento.enMovimiento)
        {
            ActivarLayer(layerCaminar);
        }else{
            ActivarLayer(layerIdle);
        }
    }

    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        miAnimator.SetBool(derrotado, false);
    }

    private void PersonajeDerrotadoRespuesta()
    {
        if(miAnimator.GetLayerWeight(miAnimator.GetLayerIndex(layerIdle)) == 1)
        {
            miAnimator.SetBool(derrotado, value:true);
        }
    }

    private void OnEnable() 
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }
    
    private void OnDisable() 
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }

}

