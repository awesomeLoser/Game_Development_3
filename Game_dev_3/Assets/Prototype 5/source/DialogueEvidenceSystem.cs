using UnityEngine;
using Yarn.Unity;

namespace AceAttorney
{
    public class DialogueEvidenceSystem : MonoBehaviour
    {
        public static DialogueEvidenceSystem Instance { get; private set; }

        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private EvidenceMenuController evidenceMenu;
        [SerializeField] private string evidenceVariableName = "$presented_evidence";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            if (dialogueRunner == null)
            {
                dialogueRunner = FindObjectOfType<DialogueRunner>();
            }

            if (evidenceMenu == null)
            {
                evidenceMenu = FindObjectOfType<EvidenceMenuController>();
            }

            if (dialogueRunner == null)
            {
                Debug.LogError("DialogueEvidenceSystem requires a DialogueRunner in the scene.");
            }
        }

        [YarnCommand("open_evidence_menu")]
        public static async YarnTask OpenEvidenceMenuAsync()
        {
            if (Instance == null)
            {
                Debug.LogWarning("DialogueEvidenceSystem instance not found.");
                return;
            }

            if (Instance.dialogueRunner == null)
            {
                Debug.LogWarning("No DialogueRunner assigned to DialogueEvidenceSystem.");
                return;
            }

            if (Instance.evidenceMenu == null)
            {
                Debug.LogWarning("No EvidenceMenuController assigned to DialogueEvidenceSystem.");
                return;
            }

            var completion = new YarnTaskCompletionSource();

            Instance.evidenceMenu.Open((selectedEvidenceId) =>
            {
                var finalEvidenceId = string.IsNullOrEmpty(selectedEvidenceId) ? string.Empty : selectedEvidenceId;
                Instance.dialogueRunner.VariableStorage.SetValue(Instance.evidenceVariableName, finalEvidenceId);
                completion.TrySetResult();
            });

            await completion.Task;
        }

    }
}
