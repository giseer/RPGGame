using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questDescripcion;

    public Quest QuestPorCompletar { get; set; }

    public virtual void ConfigurarQuestUI(Quest quest){
        QuestPorCompletar = quest;
        questNombre.text = quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }
}
