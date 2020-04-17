using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnObjects;

    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    [SerializeField]
    private Player player;

    private Vector3 spawnPoint;

    private bool IsThereAtLeastOneObjectToSpawn
    {
        get
        {
            bool result = false;

            for (int i = 0; i < spawnObjects.Length; i++)
            {
                result = spawnObjects[i] != null;

                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (spawnObjects.Length > 0 && IsThereAtLeastOneObjectToSpawn)
        {
            InvokeRepeating("SpawnObject", firstSpawnDelay, spawnRate);

            if (player != null)
            {
                player.OnPlayerDied += StopSpawning;
            }
        }
    }

    private void SpawnObject()
    {
        GameObject spawnGO = spawnObjects[Random.Range(0, spawnObjects.Length)];

        if (spawnGO != null)
        {
            spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));

            GameObject instance = InterPooling.instance.RequestTT(spawnGO);
            instance.transform.position = spawnPoint;
            instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void StopSpawning()
    {
        CancelInvoke();
    }
}