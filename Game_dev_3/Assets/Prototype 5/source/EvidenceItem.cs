using UnityEngine;

namespace AceAttorney
{
    [CreateAssetMenu(menuName = "Ace Attorney/Evidence Item")]
    public class EvidenceItem : ScriptableObject
    {
        [SerializeField] private string id = "";
        [SerializeField] private string displayName = "Evidence";
        [TextArea(3, 5)]
        [SerializeField] private string description = "";

        public string Id => id;
        public string DisplayName => displayName;
        public string Description => description;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = name.Replace(" ", "_").ToLowerInvariant();
            }
        }
#endif
    }
}
