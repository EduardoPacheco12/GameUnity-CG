using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour {

    FloorGenerate floorGenerate;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] float tallObstacleChance = 0.2f;

    // Start is called before the first frame update
    private void Start() {

        floorGenerate = GameObject.FindObjectOfType<FloorGenerate>();
    }

    private void OnTriggerExit(Collider other) {

        floorGenerate.GenerateTile(true);
        Destroy(gameObject, 2);
    }

    public void GenerateObstacle() {

        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if(random < tallObstacleChance) {
            obstacleToSpawn = tallObstaclePrefab;
        }
        
        int obstacleSpawnIndex = Random.Range(2,5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstacleToSpawn,spawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnCoins() {

        int coinsToSpawn = 10;
        for (var i = 0; i < coinsToSpawn; i++) {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }

    }

    Vector3 GetRandomPointInCollider (Collider collider) {

        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x,collider.bounds.max.x), Random.Range(collider.bounds.min.y,collider.bounds.max.y), Random.Range(collider.bounds.min.z,collider.bounds.max.z));
        if( point != collider.ClosestPoint(point)) {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
