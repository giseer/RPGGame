using System.Collections;
using UnityEngine;
using System;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;

    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPosicion;

    [Header("Rastros")]
    [SerializeField] private GameObject rastros;

    private EnemigoBarraVida miEnemigoBarraVidaCreada;
    private EnemigoInteraccion miEnemigoInteraccion;
    private EnemigoMovimiento miEnemigoMovimiento;
    private SpriteRenderer miSpriteRenderer;
    private BoxCollider2D miBoxCollider2D;
    private IAController miController;
    private EnemigoLoot miEnemigoLoot;

    private void Awake()
    {
        miSpriteRenderer = GetComponent<SpriteRenderer>();
        miBoxCollider2D = GetComponent<BoxCollider2D>();
        miController = GetComponent<IAController>();
        miEnemigoInteraccion = GetComponent<EnemigoInteraccion>();
        miEnemigoMovimiento = GetComponent<EnemigoMovimiento>();
        miEnemigoLoot = GetComponent<EnemigoLoot>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    private void CrearBarraVida()
    {
        miEnemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPosicion);
        ActualizarBarraVida(Salud, saludMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        miEnemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }

    protected override void PersonajeDerrotado()
    {
        DesactivarEnemigo();
        EventoEnemigoDerrotado?.Invoke(miEnemigoLoot.ExpGanada);
        QuestManager.Instance.AnadirProgreso("Mata10", 1);
        QuestManager.Instance.AnadirProgreso("Mata25", 1);
        QuestManager.Instance.AnadirProgreso("Mata50", 1);
    }

    private void DesactivarEnemigo()
    {
        rastros.SetActive(true);
        miSpriteRenderer.enabled = false;
        miController.enabled = false;
        miBoxCollider2D.isTrigger = true;
        miEnemigoInteraccion.DesactivarSpritesSeleccion();
        miEnemigoMovimiento.enabled = false;
        miEnemigoBarraVidaCreada.gameObject.SetActive(false);
    }
}
