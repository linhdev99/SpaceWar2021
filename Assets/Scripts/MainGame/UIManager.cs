using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerHandler player;
    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private Image imgHealthBar;
    [SerializeField]
    private GameObject menuStartGame;
    [SerializeField]
    private GameObject menuGameOver;
    private void Start()
    {
        player = GameObject.Find("MainPlayer").GetComponent<PlayerHandler>();
    }
    public void ClickButtonEffect1()
    {
        player.ShieldState(true);
    }
    public void ClickButtonEffect2()
    {
        player.StartEffectHealing();
    }
    public void ClickButtonEffect3()
    {
        player.increaseBulletDamage();
    }
    public void updateScore(int score) 
    {
        txtScore.text = "Score: " + score.ToString();
    }
    public void updateHealthBar(float value)
    {
        imgHealthBar.fillAmount = value;
        if (value > 0.3f)
        {
            imgHealthBar.color = Color.green;
        }
        else 
        {
            imgHealthBar.color = Color.yellow;
        }
    }
    public void ClickStartGame() 
    {
        Time.timeScale = 1;
        menuStartGame.SetActive(false);
    }
    public void GameOver() 
    {
        Time.timeScale = 0;
        menuGameOver.SetActive(true);
    }
    public void ClickResetScene() 
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
}
