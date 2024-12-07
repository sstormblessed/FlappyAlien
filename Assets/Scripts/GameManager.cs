using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject exitButton;
    private int score;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        
        Pause();
    }

    //M�todo para iniciar el juego
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        exitButton.SetActive(false);

        Time.timeScale = 1f;
        player.ResetPlayer();
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for(int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    //M�todo para cerrar el juego
    public void Exit()
    {
        Application.Quit();
    }

    //M�todo de pausa
    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    //M�todo que termina la partida al chocar con un obst�culo
    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1.5f);

        gameOver.SetActive(true);
        playButton.SetActive(true);
        exitButton.SetActive(true);

        Pause();
    }

    //M�todo que aumenta la puntuaci�n
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

}
