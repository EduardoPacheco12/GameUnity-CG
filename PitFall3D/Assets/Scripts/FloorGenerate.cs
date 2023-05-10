using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerate : MonoBehaviour {

    [SerializeField] GameObject FloorTile;
    Vector3 nextGeneratePoint;

    public void GenerateTile(bool generateItems) {

        GameObject temp = Instantiate(FloorTile, nextGeneratePoint, Quaternion.identity);
        nextGeneratePoint = temp.transform.GetChild(1).transform.position;

        if(generateItems) {
            temp.GetComponent<FloorTile>().GenerateObstacle();
            temp.GetComponent<FloorTile>().SpawnCoins();
        }
    }

    // Start is called before the first frame update
    private void Start() {

        for (var i = 0; i < 15; i++) {
            if(i < 3) {
                GenerateTile(false);
            } else {
                GenerateTile(true);
            }
        }
        
    }

    // Update is called once per frame
    private void Update() {
        
    }
}
