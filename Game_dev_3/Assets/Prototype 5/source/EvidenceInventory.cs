using System.Collections.Generic;
using UnityEngine;

namespace AceAttorney
{
    public class EvidenceInventory : MonoBehaviour
    {
        public static EvidenceInventory Instance { get; private set; }

        [SerializeField] private List<string> collectedEvidenceIds = new List<string>();

        public IReadOnlyList<string> CollectedEvidenceIds => collectedEvidenceIds;

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
        }

        public bool AddEvidence(string evidenceId)
        {
            if (string.IsNullOrWhiteSpace(evidenceId) || collectedEvidenceIds.Contains(evidenceId))
            {
                return false;
            }

            collectedEvidenceIds.Add(evidenceId);
            return true;
        }

        public bool HasEvidence(string evidenceId)
        {
            return !string.IsNullOrWhiteSpace(evidenceId) && collectedEvidenceIds.Contains(evidenceId);
        }

        public void Clear()
        {
            collectedEvidenceIds.Clear();
        }
    }
}

