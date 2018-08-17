using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [HideInInspector]
    public static EnemySpawner instance;


    public int maxEnemies = 20;
    public float enemyScaleRandomize = 1.5f;

    public float spawnDelayWithinWave = 1f;
    public Vector3 enemySpawnPosOffset = new Vector3 (2f,0f,2f);
    
    public List<Transform> spawnPoints;
    public int spawnPointCache = 5;


    public GameObject[] enemyPrefabs;

    public List<Transform> treeTargets;

    private List<GameObject> enemies;       //FIXIT: make sure the enemy is removed from the list on death

    void Start () {
        //Only one object of this type may exist per scene
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);


        enemies = new List<GameObject>();

  

        //TODO: randomize the spawnPoints?
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public int EnemyCount() {
        return enemies.Count;
    }

    //Spawns a wave (from all the spawn points), does not spawn from recently used spawn point. 
    public IEnumerator SpawnWaveRandomly(int number) {

        for (int i = 0 ; i < number ; i++) {
            int spoint = Random.Range(0 , spawnPointCache);     //Select any enemy in the Spawn point cache
            spawnDelayWithinWave = Random.Range(spawnDelayWithinWave - 0.2f*spawnDelayWithinWave , spawnDelayWithinWave + 0.2f * spawnDelayWithinWave);
            Spawn(spawnPoints[spoint].position);
            spawnPoints.Add(spawnPoints[spoint]);
            spawnPoints.RemoveAt(spoint);
            yield return new WaitForSeconds(spawnDelayWithinWave);

        }
    }
    //Spawns a wave from a prespecified spawn point (selects a spawn point at random if not specified)
    public IEnumerator SpawnWave(int number,Vector3? spoint = null) {
        if (spoint == null)
            spoint = spawnPoints[Random.Range(0 , spawnPoints.Count)].position;

        for(int i = 0 ; i < number ; i++) {
            spawnDelayWithinWave = Random.Range(spawnDelayWithinWave - 0.25f , spawnDelayWithinWave + 0.25f);
            Spawn(spoint ?? Vector3.zero);
            yield return new WaitForSeconds(spawnDelayWithinWave);
        }

    } 
    //Private Spawn function used for spawning 
    private void Spawn (Vector3 spawnPos) {

        if(enemies.Count >= maxEnemies) {
          //  print("cant spawn more enemies, max limit reached.");
            return;
        }

        GameObject enemyInstance = enemyPrefabs[Random.Range(0,enemyPrefabs.Length)];
        
        //Instantiate the enemy (make sure the enemy prefab is inactive by default
        GameObject enemy = Instantiate(enemyInstance , spawnPos+enemySpawnPosOffset , Quaternion.identity);
        if (enemy) {
          //  print("enemy spawned" +enemy);
            enemies.Add(enemy);

            //Find closest tree and set that to enemy target

            
            Enemy2 e2 = enemy.GetComponent<Enemy2>();
            Transform target = FindTarget(e2);


            //Randomize the scale with enemyScaleRandomize
            float xyzScale = Random.Range(-enemyScaleRandomize , enemyScaleRandomize);      
            Vector3 scaleRandomize = new Vector3(xyzScale , xyzScale , xyzScale);
            enemy.transform.localScale += scaleRandomize;
            e2.Initialize();
            StartCoroutine(e2.SetTarget(target));
            enemy.SetActive(true);

        }


    }

    public static Transform FindTarget (Enemy2 enemy) {
        /*  if(enemy.isTreeDead()) {
              foreach (Transform t in instance.treeTargets) {

                  print(t);
              }
              print("del" + enemy.transform);
              instance.treeTargets.Remove(enemy.transform);
          }
          */
        //Find closest tree and set that to enemy target
        Transform target = null;
        if (instance.treeTargets.Count > 0) {
            target = instance.treeTargets[0];
            
        }
        else {
            return null;
        }
        
        float targetDistance = (target.position - enemy.transform.position).sqrMagnitude;

        for (int i = 1 ; i < instance.treeTargets.Count ; i++) {
            float newDist = (instance.treeTargets[i].position - enemy.transform.position).sqrMagnitude;
            if (newDist < targetDistance) {
                targetDistance = newDist;
                target = instance.treeTargets[i];
            }

        }

        return target;
    }
    public void UnregisterEnemy (GameObject t) {
        enemies.Remove(t);
    }
    public void UnregisterTree (Transform t) {
        treeTargets.Remove(t);
        print("tree unregistered");
        if(treeTargets.Count < 1) {
            print("loading ls");
            ScoreManager.instance.ShowLoseScreen();
        }
    }




}
