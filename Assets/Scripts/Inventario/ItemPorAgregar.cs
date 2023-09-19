using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;

    private void OnTriggerEnter2D(Collider2D other) {
           if (other.CompareTag("Player"))
           {
               Inventario.Instance.AnadirItem(inventarioItemReferencia, cantidadPorAgregar);
               Destroy(gameObject);
           }
    }

}
