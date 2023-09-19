using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float, EnemigoVida> EventoEnemigoDanado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Ataque")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private Transform[] posicionesDisparo;

    public Arma ArmaEquipada { get; private set; }
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    public bool Atacando { get; set; }

    private PersonajeMana miPersonajeMana;
    private int indexDireccionDisparo;
    private float tiempoParaSiguienteAtaque;

    private void Awake()
    {
        miPersonajeMana = GetComponent<PersonajeMana>();
    }

    private void Update()
    {
        ObenerDireccionDisparo();

        if (Time.time > tiempoParaSiguienteAtaque)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                {
                    return;
                }

                UsarArma();
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
                StartCoroutine(IEEstablecerCondicionAtaque());
            }
        }
    }

    private void UsarArma()
    {
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            if (miPersonajeMana.ManaActual < ArmaEquipada.ManaRequerida)
            {
                return;
            }

            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;

            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);

            nuevoProyectil.SetActive(true);
            miPersonajeMana.UsarMana(ArmaEquipada.ManaRequerida);
        }
        else
        {
            float dano = ObtenerDano();
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDano(dano);
            EventoEnemigoDanado?.Invoke(dano,enemigoVida);
        }
    }

    public float ObtenerDano()
    {
        float cantidad = stats.Da�o;
        if (Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad;
    }

    private IEnumerator IEEstablecerCondicionAtaque()
    {
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AnadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }

    private void ObenerDireccionDisparo()
    {
        Vector2 input = new Vector2(x: Input.GetAxisRaw("Horizontal"), y: Input.GetAxisRaw("Vertical"));
        if (input.x > 0.1f)
        {
            indexDireccionDisparo = 1;
        }
        else if (input.x < 0f)
        {
            indexDireccionDisparo = 3;
        }
        else if (input.y > 0.1f)
        {
            indexDireccionDisparo = 0;
        }
        else if (input.y < 0f)
        {
            indexDireccionDisparo = 2;
        }
    }

    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if (ArmaEquipada == null) { return; }

        if (ArmaEquipada.Tipo != TipoArma.Magia) { return; }

        if (EnemigoObjetivo == enemigoSeleccionado) { return; }

        EnemigoObjetivo = enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo == null) { return; }

        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        EnemigoObjetivo=null;
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null) { return; }
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo = enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);
    }

    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null) { return; }
        if (EnemigoObjetivo == null) { return; }
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;
    }

    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable()
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }
}
