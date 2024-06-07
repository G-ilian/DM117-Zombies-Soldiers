using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwannerZombieController : MonoBehaviour
{
    [SerializeField] List<GameObject> zombies;
    [SerializeField] float spawnTime;
    [SerializeField] GameObject ground;
    Collider groundCollider;


    private void Start()
    {
        groundCollider = ground.GetComponent<Collider>();
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            PlaceEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void PlaceEnemy()
    {

        if (groundCollider != null)
        {
            Vector3 groundMin = groundCollider.bounds.min;
            Vector3 groundMax = groundCollider.bounds.max;

            Vector3 position = new Vector3(Random.Range(groundMin.x, groundMax.x),
                                           0f,
                                           Random.Range(groundMin.z, groundMax.z));

            var chooseEnemyRandomly = Random.Range(0, zombies.Count);

            Instantiate(zombies[chooseEnemyRandomly], position, Quaternion.identity);
        }

    }

}
