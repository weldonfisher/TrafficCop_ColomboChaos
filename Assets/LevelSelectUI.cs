using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    public Button[] levelButtons;
    public Button quitButton;

    void Start()
    {
        // Safety check
        if (LevelManager.Instance == null)
        {
            var go = new GameObject("LevelManager");
            go.AddComponent<LevelManager>();
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            bool unlocked = LevelManager.Instance.IsLevelUnlocked(levelIndex);

            levelButtons[i].interactable = unlocked;

            levelButtons[i].onClick.RemoveAllListeners();
            levelButtons[i].onClick.AddListener(() =>
            {
                LevelManager.Instance.LoadLevel(levelIndex);
            });
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
}
