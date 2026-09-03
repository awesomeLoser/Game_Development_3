using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AceAttorney
{
    public class EvidenceMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private Button buttonTemplate;
        [SerializeField] private EvidenceDatabase evidenceDatabase;
        [SerializeField] private EvidenceInventory evidenceInventory;

        private readonly Dictionary<string, Button> evidenceButtons = new Dictionary<string, Button>();
        private Action<string> onEvidenceSelected;

        private void Awake()
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
            }

            BuildMenuButtons();
        }

        public void Open(Action<string> callback)
        {
            onEvidenceSelected = callback;
            RefreshMenu();

            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
            }
        }

        public void Close()
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
            }

            onEvidenceSelected = null;
        }

        private void BuildMenuButtons()
        {
            if (contentRoot == null || buttonTemplate == null)
            {
                return;
            }

            if (evidenceInventory == null)
            {
                evidenceInventory = EvidenceInventory.Instance;
            }

            if (evidenceDatabase == null)
            {
                Debug.LogWarning("EvidenceMenuController is missing an EvidenceDatabase reference.");
                return;
            }

            foreach (Transform child in contentRoot)
            {
                if (child != buttonTemplate.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            evidenceButtons.Clear();

            var buttonIndex = 0;

            foreach (var evidenceItem in evidenceDatabase.EvidenceItems)
            {
                if (evidenceItem == null)
                {
                    continue;
                }

                var button = Instantiate(buttonTemplate, contentRoot);
                button.name = "EvidenceButton_" + evidenceItem.Id;
                button.gameObject.SetActive(false);

                var buttonRect = button.transform as RectTransform;
                if (buttonRect != null)
                {
                    buttonRect.anchorMin = new Vector2(0.5f, 1f);
                    buttonRect.anchorMax = new Vector2(0.5f, 1f);
                    buttonRect.pivot = new Vector2(0.5f, 1f);
                    buttonRect.sizeDelta = new Vector2(360f, 50f);
                    buttonRect.anchoredPosition = new Vector2(0f, -40f - (buttonIndex * 60f));
                }

                buttonIndex++;

                var tmpText = button.GetComponentInChildren<TMP_Text>();
                if (tmpText != null)
                {
                    tmpText.text = evidenceItem.DisplayName;
                }
                else
                {
                    var text = button.GetComponentInChildren<Text>();
                    if (text != null)
                    {
                        text.text = evidenceItem.DisplayName;
                    }
                }

                var entry = evidenceItem;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    onEvidenceSelected?.Invoke(entry.Id);
                    Close();
                });

                evidenceButtons[entry.Id] = button;
            }

            buttonTemplate.gameObject.SetActive(false);
        }

        private void RefreshMenu()
        {
            if (evidenceInventory == null)
            {
                evidenceInventory = EvidenceInventory.Instance;
            }

            if (evidenceDatabase == null)
            {
                return;
            }

            foreach (var evidenceItem in evidenceDatabase.EvidenceItems)
            {
                if (evidenceItem == null)
                {
                    continue;
                }

                var button = evidenceButtons.TryGetValue(evidenceItem.Id, out var foundButton) ? foundButton : null;
                if (button == null)
                {
                    continue;
                }

                button.gameObject.SetActive(evidenceInventory != null && evidenceInventory.HasEvidence(evidenceItem.Id));
            }
        }
    }
}
