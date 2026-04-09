using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int DeathCount { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DeathCount = PlayerPrefs.GetInt("TotalDeaths", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
     public void AddDeath()
    {
        DeathCount++;
        Debug.Log("Total deaths: " + DeathCount);
        PlayerPrefs.SetInt("TotalDeaths", DeathCount);
        PlayerPrefs.Save();
    }

    public void ResetGame()
    {
        DeathCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

