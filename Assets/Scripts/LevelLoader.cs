using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public int levelToLoad;

    public void LoadLevel()
    {
        if (levelToLoad == 0)
        {
            ScoreManager.Instance.ResetScore();
        }
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LoadLevel();
        }
    }
}
