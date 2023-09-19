using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;

    [Header("Quests")]
    [SerializeField] private Quest[] questDisponibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;

    public Quest QuestPorReclamar { get; private set; }

    void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AnadirProgreso("Mata10",1);
            AnadirProgreso("Mata25",1);
            AnadirProgreso("Mata50",1);
        }
    }

private void CargarQuestEnInspector(){
    for (int i = 0; i < questDisponibles.Length; i++)
    {   
        InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
    }
}

private void AnadirQuestPorCompletar(Quest questPorCompletar){
    PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
    nuevoQuest.ConfigurarQuestUI(questPorCompletar);
}

public void AnadirQuest(Quest questPorCompletar){
    AnadirQuestPorCompletar(questPorCompletar);
}

public void ReclamarRecompensa(){
    if (QuestPorReclamar == null)
    {
        return;
    }

    MonedasManager.Instance.AnadirMonedas(QuestPorReclamar.RecompensaOro);
    personaje.PersonajeExperiencia.AnadirExperiencia(QuestPorReclamar.RecompensaExp);
    Inventario.Instance.AnadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
    panelQuestCompletado.SetActive(false);
    QuestPorReclamar = null;
}

public void AnadirProgreso(string questID, int cantidad){
    Quest questPorActualizar = QuestExiste(questID);
    questPorActualizar.AnadirProgreso(cantidad);
}

private Quest QuestExiste(string questID){
    for (int i = 0; i < questDisponibles.Length; i++)
    {
        if (questDisponibles[i].ID == questID)
        {
            return questDisponibles[i];
        }
    }
    return null;
}

private void MostrarQuestCompletado(Quest questCompletado){
    panelQuestCompletado.SetActive(true);
    questNombre.text = questCompletado.Nombre;
    questRecompensaOro.text = questCompletado.RecompensaOro.ToString();
    questRecompensaExp.text = questCompletado.RecompensaExp.ToString();
    questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
    questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono;
}

private void QuestCompletadoRespuesta(Quest questCompletado){
    QuestPorReclamar = QuestExiste(questCompletado.ID);
    if (QuestPorReclamar != null)
    {
        MostrarQuestCompletado(QuestPorReclamar);
    }
}

private void OnEnable()
{
    Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
}
private void OnDisable()
{
    Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
}

}
