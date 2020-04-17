using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private Image[] lifeImages;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button restartBtn;

    

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ToggleRestartButton(false);
        Player.instance.OnPlayerDied += Death;
        Player.instance.OnPlayerHit += PlayerLivesCount;
        Player.instance.OnPlayerScoreC += ScorePlayerCount;
        
    }

    private void ToggleRestartButton(bool val)
    {
        if (restartBtn != null)
        {
            restartBtn.gameObject.SetActive(val);
        }
    }

   
    public void Death()
    {
        if (!scoreLabel) return;
        ToggleRestartButton(true);
        scoreLabel.text = "Game Over";
    
    }

    public void PlayerLivesCount(int updateLives)
    {
        if (!scoreLabel || lifeImages != null) return;

        if (updateLives <= 0)
            scoreLabel.text = "Game Over";
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = i < updateLives;
        }
    }

    public void ScorePlayerCount(int updateScore)
    {
        if (!scoreLabel) return;
        scoreLabel.text = updateScore.ToString();
    }
    
}