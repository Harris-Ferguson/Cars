using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector3 playerStart = Vector3.zero;
    public float spawnRate = 5.0f;
    public GameObject player;
    public int lives = 5;
    public int stage = 1;
    public Text lives_text;
    public Text score_text;
    public Text rate_text;
    public GameObject GameOverBut;

    private List<EnemySpawner> spawners = new List<EnemySpawner>();
    private int score;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        // get the enemy spawners
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject obj in spawns)
        {
            spawners.Add(obj.GetComponent<EnemySpawner>());
        }
        //get the players initial position for respawning
        playerStart = player.transform.position;

        // set the UI text
        lives_text.text = "Lives: " + lives.ToString();
        score_text.text = "Score: " + score.ToString();
        rate_text.text = "Spawn Rate: " + spawnRate.ToString();

        //set all the spawners going
        StartCoroutine(reapetedSpawn());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator reapetedSpawn()
    {
        spawnAll();
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(reapetedSpawn());
    }

    public void spawnAll()
    {
        foreach(EnemySpawner sp in spawners){
            sp.Spawn();
        }
    }

    public void GoalReached()
    {
        resetPlayer(1.0f);
        score += 100 * stage;
        stage++;
        if(spawnRate == 0)
        {
            spawnRate = 0;
        }
        else
        { 
            spawnRate -= 0.2f;
        }
        score_text.text = "Score: " + score.ToString();
        rate_text.text = "Spawn Rate: " + spawnRate.ToString();
    }

    private void playerDied()
    {
        resetPlayer(2.0f);
        lives--;
        lives_text.text = "Lives: " + lives.ToString();
        if(lives < 0)
        {
            score_text.fontSize = 30;
            GameOverBut.gameObject.SetActive(true);
        }
    }

    public void gameOver()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void resetPlayer(float delay) { 
        player.transform.SetPositionAndRotation(playerStart,Quaternion.identity);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<PlayerController>().setWaiting(delay);
    }
}
