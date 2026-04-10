using UnityEngine;
using UnityEngine.UI;
using System;
public class GoalDoor : MonoBehaviour
{
    [Header("UI Setting")]
    public GameObject victoryPanel;
    public Text victoryText;
    public String message = "Congradulation!";

    [Header("Game")]
    public bool disablePlayerOnWin = true;
    public bool quitGameOnWin = false;
    public float delayBeforQuit = 2f;

    private bool gameComplete = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameComplete) return;
        if(!other.CompareTag("Player")) return;

        gameComplete = true;
        CompleteGame();
    }

    void CompleteGame()
    {
        GameManager.Instance.ResetDeathCount();
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            if(victoryText != null) victoryText.text = message;        
        }
        else
        {
            Debug.Log(message);
        }
    }

    void OnGUI()
    {
        if(gameComplete && victoryPanel == null)
        {
            GUI.Label(new Rect(Screen.width/2 - 100, Screen.height/2 - 25,
            200, 50), message);
        }
    }
}
