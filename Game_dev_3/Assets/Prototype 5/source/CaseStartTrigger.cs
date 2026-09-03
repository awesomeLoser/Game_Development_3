using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

namespace AceAttorney
{
    // Temporary keyboard launcher for testing the case.
    // Attach this to the Dialogue System object. Press C only after collecting all five evidence items.
    public class CaseStartTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private EvidenceInventory evidenceInventory;
        [SerializeField] private KeyCode startCaseKey = KeyCode.C;
        [SerializeField] private string caseStartNode = "Case_LastTrain_Start";
        [SerializeField] private string caseCompleteScene = "Game Over";

        private bool caseStarted;
        private bool completionHandlerRegistered;

        private static readonly string[] RequiredEvidenceIds =
        {
            "torn_ticket",
            "statement",
            "platform_clock_photo",
            "coffee_receipt",
            "lost_glove"
        };

        private void Awake()
        {
            if (dialogueRunner == null)
            {
                dialogueRunner = FindObjectOfType<DialogueRunner>();
            }

            if (evidenceInventory == null)
            {
                evidenceInventory = EvidenceInventory.Instance;
            }

            RegisterCompletionHandler();
        }

        private void Update()
        {
            if (dialogueRunner == null)
            {
                dialogueRunner = FindObjectOfType<DialogueRunner>();
            }

            if (evidenceInventory == null)
            {
                evidenceInventory = EvidenceInventory.Instance;
            }

            RegisterCompletionHandler();

            if (!Input.GetKeyDown(startCaseKey))
            {
                return;
            }

            if (dialogueRunner == null || evidenceInventory == null)
            {
                Debug.LogWarning("CaseStartTrigger needs a DialogueRunner and EvidenceInventory.");
                return;
            }

            foreach (var evidenceId in RequiredEvidenceIds)
            {
                if (!evidenceInventory.HasEvidence(evidenceId))
                {
                    Debug.Log("Collect all five evidence items before starting The Last Train Case.");
                    return;
                }
            }

            caseStarted = true;
            dialogueRunner.StartDialogue(caseStartNode);
        }

        private void RegisterCompletionHandler()
        {
            if (completionHandlerRegistered || dialogueRunner == null)
            {
                return;
            }

            dialogueRunner.onDialogueComplete ??= new UnityEngine.Events.UnityEvent();
            dialogueRunner.onDialogueComplete.AddListener(HandleCaseComplete);
            completionHandlerRegistered = true;
            Debug.Log("CaseStartTrigger registered the dialogue-complete handler.");
        }

        private void HandleCaseComplete()
        {
            Debug.Log($"Dialogue complete event fired. caseStarted={caseStarted}, destination={caseCompleteScene}");

            if (!caseStarted || string.IsNullOrWhiteSpace(caseCompleteScene))
            {
                Debug.LogWarning("CaseStartTrigger ignored dialogue completion because the case was not started or the destination is empty.");
                return;
            }

            Debug.Log($"Loading completed-case scene: {caseCompleteScene}");
            SceneManager.LoadScene(caseCompleteScene);
        }

        private void OnGUI()
        {
            if (caseStarted || evidenceInventory == null || !HasAllRequiredEvidence())
            {
                return;
            }

            var style = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 24,
                fontStyle = FontStyle.Bold
            };
            style.normal.textColor = Color.white;

            GUI.Label(
                new Rect(0f, Screen.height * 0.15f, Screen.width, 40f),
                "All evidence collected — press C to begin the cross-examination",
                style);
        }

        private bool HasAllRequiredEvidence()
        {
            if (evidenceInventory == null)
            {
                return false;
            }

            foreach (var evidenceId in RequiredEvidenceIds)
            {
                if (!evidenceInventory.HasEvidence(evidenceId))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
