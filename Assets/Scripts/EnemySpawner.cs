using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float respawnTime = 0.1f;
    [SerializeField] private Vector3 range;
    [SerializeField] private GameObject player;

    [SerializeField] private float spawnThresholdRange = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject pickEnemy()
    {
        int ranNum = UnityEngine.Random.Range(0, enemy.Length);
        return enemy[ranNum];
    }
    private void SpawnEnemy()
    {
        Vector3 pos = RandomPosition();
        Instantiate(pickEnemy(), pos, Quaternion.Euler(0, 0, 0));
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }


    private Vector2 RandomPosition()
    {
        Vector2 topLeft = player.transform.position + range;
        Vector2 bottomRight = player.transform.position - range;

        Vector2 newPos = new Vector2();
        newPos.x = Random.Range(topLeft.x, bottomRight.x);
        newPos.y = Random.Range(bottomRight.y, topLeft.y);

        if (Vector2.Distance(newPos, player.transform.position) > spawnThresholdRange)
        {
            return newPos;
        }

        return RandomPosition();
    }
}

