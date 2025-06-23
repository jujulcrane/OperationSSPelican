using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject portal;
    public Pelican pelican;
    public Text hpText;
    public Text ghostText;
    public Text highScore;
    public GameObject fish;
    public float spawnRate = 5;
    private float timer = 0;
    public GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "High Score: " + StateNameController.highScore;
        spawnFish();
        updateHP();
        updateScore();
    }

    // Update is called once per frame
    void Update()
    {
        fishSpawner();
    }

    public void fishSpawner()
    {
        GameObject[] fishInGame = GameObject.FindGameObjectsWithTag("Fish");
        int numberOfFish = fishInGame.Length;
        if (numberOfFish < 5)
        { 
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnFish();
                timer = 0;
            }
        }
    }

    public void spawnFish()
    {
        int highestPoint = 5;
        int lowestPoint = -5;
        Instantiate(fish, new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }

    public void updateScore()
    {
        ghostText.text = "Score: " + StateNameController.ghostCount;
    }

    public void updateHP()
    {
        hpText.text = "HP: " + pelican.pHP.ToString() + "/100";
    }

    public void restartGame()
    {
        if (StateNameController.hard)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        if (StateNameController.ghostCount > StateNameController.highScore)
        {
            StateNameController.highScore = StateNameController.ghostCount;
        }
        StateNameController.ghostCount = 0;
        StateNameController.level = 1;
    }


    public void goHome()
    {
        SceneManager.LoadScene(0);
        if (StateNameController.ghostCount > StateNameController.highScore)
        {
            StateNameController.highScore = StateNameController.ghostCount;
        }
        StateNameController.ghostCount = 0;
        StateNameController.level = 1;
    }

    public void gameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void keepInBounds(GameObject obj)
    {
        Vector3 viewPos = obj.transform.position;
        if (viewPos.x < -8.45)
        {
            obj.transform.position = new Vector3(-8.45f, viewPos.y, viewPos.z);
        }
        if (viewPos.x > 8.45)
        {
            obj.transform.position = new Vector3(8.45f, viewPos.y, viewPos.z);
        }
        if (viewPos.y > 5.07)
        {
            obj.transform.position = new Vector3(viewPos.x, 5.07f, viewPos.z);
        }
        if (viewPos.y < -5.07)
        {
            obj.transform.position = new Vector3(viewPos.x, -5.07f, viewPos.z);
        }
    }
}
