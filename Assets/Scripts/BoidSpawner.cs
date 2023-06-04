using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject[] Boids;
    //range from which the boids can spawn (should be lower than exit range, lest they be immediately eviscerated for spawning in exit zone
    public float enterRange;
    //range where boids will disappear (should be in the fog)
    public float exitRange;
    public float elevationDisplacement;
    float timeValue;
    public float frequency;
    public float playerContainerAntiFishFieldOffset;
    public bool spawning;
    public GameObject player;
    playerScript2 controller;
    Vector3 spawnPosition;

    void Start()
    {
        controller = player.GetComponent<playerScript2>();
        timeValue = Random.value * frequency;
        //spawning = true;
    }

    void Update()
    {
        if (spawning)
        {
            timeValue = Random.value * frequency;
            StartCoroutine(SpawnTimer(timeValue));
            spawning = false;
        }
    }

    public IEnumerator SpawnTimer(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        int randomValue = Random.Range(0, Boids.Length);
        GameObject Boid = Boids[randomValue];
        SpawnBoids(Boid);
        spawning = true;
    }

    private void SpawnBoids(GameObject boid)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject boidGroup = Instantiate(boid, spawnPosition, Quaternion.identity);
        Boid boidScript = boid.GetComponent<Boid>();
        boidScript.spawner = this;
        boidScript.player = player;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        bool validSpawn = false;
        int attempts = 0;
        while (!validSpawn)
        {
            
            Vector3 playerPosition = transform.position;

            // Calculate the range between deletion distance and spawn distance
            float spawnRange = enterRange;

            // some value between 0 and 1 times the range they can spawn in
            Vector3 randomSpherePoint = Random.onUnitSphere.normalized * spawnRange;

            // Add the position vector to the player vector, to position it at the player's position
            spawnPosition = playerPosition + randomSpherePoint;
            // Check if spawn position is inside the submarine
            while (IsInsideSubmarine(spawnPosition) || spawnPosition.y < player.transform.position.y + elevationDisplacement)
            {
                attempts++;
                if (attempts >= 10)
                {
                    print("Attempts exceeded, breaking loop to avoid crash");
                    break;
                }
                // If spawn position is inside the submarine, recalculate the spawn offset
                randomSpherePoint = Random.onUnitSphere.normalized * spawnRange;
                randomSpherePoint = new Vector3(randomSpherePoint.x, 0f, randomSpherePoint.y);
                spawnPosition = playerPosition + randomSpherePoint;
            }
            validSpawn = true;
        }
        return spawnPosition;
    }
    bool IsInsideSubmarine(Vector3 position)
    {
        // Assuming the submarine is represented by an ellipsoid shape centered at the Sub's "playercontainter" position
        Vector3 localPosition =  controller.controller.playerContainer.transform.InverseTransformPoint(position);
        float normalizedX = localPosition.x / controller.spaceRadiusX + playerContainerAntiFishFieldOffset; 
        float normalizedY = localPosition.y / controller.spaceRadiusX + playerContainerAntiFishFieldOffset;
        //note: I'm using the elipse info from the playercontainer, which is two dimentional. This is why I'm using sRX twice. if the sub ends up being weirdly shaped, we can fix this. 
        float normalizedZ = localPosition.z / controller.spaceRadiusZ + playerContainerAntiFishFieldOffset;

        // Check if the point is inside the ellipsoid using the equation: (x/a)^2 + (y/b)^2 + (z/c)^2 <= 1
        bool isInside = (normalizedX * normalizedX) + (normalizedY * normalizedY) + (normalizedZ * normalizedZ) <= 1f;
        return isInside;
    }
}
