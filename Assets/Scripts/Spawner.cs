using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public float spawnRate = 1.5f;
    public float minHeight = -1f;
    public float maxHeight = 2;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }

}
