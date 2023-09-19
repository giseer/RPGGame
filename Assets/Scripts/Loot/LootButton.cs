using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;    

    public DropItem ItemPorRecoger { get; set; }

    public void ConfigurarLootItem(DropItem dropItem)
    {
        ItemPorRecoger = dropItem;
        itemIcono.sprite = dropItem.Item.Icono;
        itemNombre.text = $"{dropItem.Item.Nombre} x{dropItem.Cantidad}";
    }

    public void RecogerItem()
    {
        if (ItemPorRecoger == null)
        {
            return;
        }

        Inventario.Instance.AnadirItem(ItemPorRecoger.Item, ItemPorRecoger.Cantidad);
        ItemPorRecoger.ItemRecogido = true;
        Destroy(gameObject);
    }
}
