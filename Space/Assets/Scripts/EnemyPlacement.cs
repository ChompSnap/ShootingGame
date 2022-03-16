using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPlacement : MonoBehaviour
{
    public int rows = 5;
    public int cols = 11;
    public Enemy[] prefabs;
    public AnimationCurve speed;
    public Projectile missile;
    public int amountKilled { get; private set; }
    public int totalEnemies => rows * cols;
    public float precentKilled => (float)amountKilled / (float)totalEnemies;
    public int amountAlive => totalEnemies - amountKilled;

    private Vector3 direction = Vector2.right;
    public float missileRate = 1.0f;

    private void Awake()
    {
        int i;
        int j;
        for(i = 0; i < rows; i++)
        {
            float width = 2.0f * (cols - 1);
            float height = 2.0f * (rows - 1);
            Vector2 center = new Vector2(-width / 2, -height / 2); ;
            Vector3 rowPosition = new Vector3(center.x, center.y + (i * 2.0f), 0.0f);
            for(j = 0; j < cols; j++)
            {
                Enemy enemy = Instantiate(prefabs[i], transform);
                enemy.killed += enemyKilled;
                Vector3 position = rowPosition;
                position.x += j * 2.0f;
                enemy.transform.localPosition = position;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(missileAttack), missileRate, missileRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed.Evaluate(precentKilled) * Time.deltaTime;

        Vector3 leftWall = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightWall = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform enemy in transform)
        {
            if(!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(direction == Vector3.right && enemy.position.x > rightWall.x - 1.0f)
            {
                decend();
            }
            else if(direction == Vector3.left && enemy.position.x < leftWall.x + 1.0f)
            {
                decend();
            }
        }
    }

    private void decend()
    {
        direction.x *= -1;
        Vector3 pos = transform.position;
        pos.y -= 1.0f;
        transform.position = pos;
    }

    private void enemyKilled()
    {
        amountKilled++;
        if(amountKilled >= totalEnemies)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }
    private void missileAttack()
    {
        foreach (Transform enemy in transform)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < (1.0f / (float)amountAlive))
            {
                Instantiate(missile, enemy.position, Quaternion.identity);
                enemy.gameObject.GetComponent<Enemy>().invaderAttack();
                break;
            }
        }
    }
}
