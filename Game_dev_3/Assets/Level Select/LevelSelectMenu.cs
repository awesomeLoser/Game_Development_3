using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Creates the level-select interface and loads the selected prototype scene.
/// </summary>
public class LevelSelectMenu : MonoBehaviour
{
    private readonly (string label, string sceneName)[] levels =
    {
        ("Tree Game", "Dialouge_Begins"),
        ("Time", "MainLevelTime"),
        ("Upgrades", "FullUpgradeLevel"),
        ("Pizza Time", "Prototype 2")
    };

    private void Awake()
    {
        CreateEventSystem();

        Canvas canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)).GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        Image background = canvas.gameObject.AddComponent<Image>();
        background.color = new Color(0.07f, 0.1f, 0.18f, 1f);
        RectTransform backgroundRect = background.rectTransform;
        backgroundRect.anchorMin = Vector2.zero;
        backgroundRect.anchorMax = Vector2.one;
        backgroundRect.offsetMin = Vector2.zero;
        backgroundRect.offsetMax = Vector2.zero;

        CreateText(canvas.transform, "Title", "Choose a Prototype", 58, new Vector2(0f, 285f), new Vector2(800f, 90f));

        for (int i = 0; i < levels.Length; i++)
        {
            int levelIndex = i;
            CreateButton(canvas.transform, levels[i].label, new Vector2(0f, 115f - i * 125f), () => LoadLevel(levels[levelIndex].sceneName));
        }
    }

    private static void CreateEventSystem()
    {
        if (FindFirstObjectByType<EventSystem>() != null)
            return;

        GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        eventSystem.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();
    }

    private static void CreateText(Transform parent, string objectName, string label, int fontSize, Vector2 position, Vector2 size)
    {
        GameObject textObject = new GameObject(objectName, typeof(RectTransform), typeof(Text));
        textObject.transform.SetParent(parent, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        Text text = textObject.GetComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.text = label;
        text.fontSize = fontSize;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    private static void CreateButton(Transform parent, string label, Vector2 position, UnityEngine.Events.UnityAction onClick)
    {
        GameObject buttonObject = new GameObject(label + " Button", typeof(RectTransform), typeof(Image), typeof(Button));
        buttonObject.transform.SetParent(parent, false);

        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(460f, 90f);

        Image image = buttonObject.GetComponent<Image>();
        image.color = new Color(0.16f, 0.42f, 0.72f, 1f);

        Button button = buttonObject.GetComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(onClick);

        CreateText(buttonObject.transform, "Label", label, 30, Vector2.zero, rect.sizeDelta);
    }

    private static void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
