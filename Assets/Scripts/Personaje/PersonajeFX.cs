using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Config")]
    [SerializeField] private GameObject canvasTextoAnimacioPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [Header("Tipo")]
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private EnemigoVida miEnemigoVida;

    private void Awake()
    {
        miEnemigoVida = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacioPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color){
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad,color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDanoRecibidoHaciaPlayer(float dano){
        if (tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostrarTexto(dano,Color.black));
        }        
    }

    private void RespuestaDanoHaciaEnemigo(float dano, EnemigoVida enemigoVida)
    {
        if (tipoPersonaje == TipoPersonaje.IA && miEnemigoVida == enemigoVida)
        {
            StartCoroutine(IEMostrarTexto(dano,Color.red));
        }
    }

    private void OnEnable()
    {
        IAController.EventoDanoRealizado += RespuestaDanoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDanado += RespuestaDanoHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDanoRealizado -= RespuestaDanoRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDanado -= RespuestaDanoHaciaEnemigo;
    }
}
