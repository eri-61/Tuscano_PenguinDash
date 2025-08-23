using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayUIScript : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = GameManager.Instance.currentScore.ToString();
        }
    }

   
}
