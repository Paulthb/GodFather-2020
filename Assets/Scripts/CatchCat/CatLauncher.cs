using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLauncher : MonoBehaviour
{
    public Vector2 minLaunchStrength;
    public Vector2 maxLaunchStrength;
    public List<GameObject> catsPrefabs;
    public int catAmount = 3;
    public float timeBetweenSpawn = 2.0f;
    private float spawnClock = 0.0f;
    private void Update()
    {
        if(catAmount > 0)
        {
            spawnClock += Time.deltaTime;
            if(spawnClock >= timeBetweenSpawn)
            {
                LaunchCat();
                spawnClock = 0.0f;
            }
        }
    }

    public void LaunchCat()
    {
        float x = Random.Range(minLaunchStrength.x, maxLaunchStrength.x);
        float y = Random.Range(minLaunchStrength.y, maxLaunchStrength.y);
        int index = Random.Range(0, catsPrefabs.Count);
        GameObject cat = Instantiate(catsPrefabs[index], transform.position, Quaternion.Euler(0,0,Random.Range(0,359)));
        cat.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        catAmount--;
    }
}
