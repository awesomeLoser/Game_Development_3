using UnityEngine;
using Yarn.Unity;

namespace AceAttorney
{
    public class InvestigationObject : MonoBehaviour
    {
        [SerializeField] private string evidenceId = "";
        [SerializeField] private string yarnNodeName = "";
        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private EvidenceInventory evidenceInventory;
        [SerializeField] private bool collected;

        private void Reset()
        {
            if (TryGetComponent<Collider>(out var collider))
            {
                collider.isTrigger = false;
            }
        }

        private void OnMouseDown()
        {
            if (collected)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(evidenceId))
            {
                Debug.LogWarning($"{name} is missing an evidenceId.");
                return;
            }

            if (evidenceInventory == null)
            {
                evidenceInventory = EvidenceInventory.Instance;
            }

            if (evidenceInventory != null)
            {
                evidenceInventory.AddEvidence(evidenceId);
            }

            if (dialogueRunner != null && !string.IsNullOrWhiteSpace(yarnNodeName))
            {
                dialogueRunner.StartDialogue(yarnNodeName);
            }
            else
            {
                Debug.Log($"Found evidence: {evidenceId}");
            }

            collected = true;
        }
    }
}
