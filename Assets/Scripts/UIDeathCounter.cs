using UnityEngine;
using UnityEngine.UI;

public class UIDeathCounter : MonoBehaviour
{
    public Text deathText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDeathDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeathDisplay();
    }

    void UpdateDeathDisplay()
{
    if (deathText == null)
    {
        Debug.LogError("UIDeathCounter: deathText is null! Assign it in inspector.");
        return;
    }
    if (GameManager.Instance == null)
    {
        Debug.LogError("UIDeathCounter: GameManager.Instance is null! Make sure a GameManager exists in the scene.");
        deathText.text = "死亡次数: 0 (No GM)";
        return;
    }
        if (GameManager.Instance.DeathCount == 0)
        {
            deathText.text = "Life: 0";
        }
        else
        {
            deathText.text = "Life: -" + GameManager.Instance.DeathCount;
        }
}
}
