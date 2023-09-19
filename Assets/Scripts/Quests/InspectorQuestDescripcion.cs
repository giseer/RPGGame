using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectorQuestDescripcion : QuestDescripcion
{

    [SerializeField] private TextMeshProUGUI questRecompensa;

    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        questRecompensa.text = $"-{quest.RecompensaOro} oro" +
                                $"\n- {quest.RecompensaExp} exp" +
                                $"\n- {quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if (QuestPorCompletar == null)
        {
            return;
        }

        QuestManager.Instance.AnadirQuest(QuestPorCompletar);
        gameObject.SetActive(false);
    }
}
