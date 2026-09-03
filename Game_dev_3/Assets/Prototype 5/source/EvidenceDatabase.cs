using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AceAttorney
{
    [CreateAssetMenu(menuName = "Ace Attorney/Evidence Database")]
    public class EvidenceDatabase : ScriptableObject
    {
        [SerializeField] private List<EvidenceItem> evidenceItems = new List<EvidenceItem>();

        public IReadOnlyList<EvidenceItem> EvidenceItems => evidenceItems;

        public EvidenceItem GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            return evidenceItems.FirstOrDefault(item => item != null && item.Id == id);
        }
    }
}
