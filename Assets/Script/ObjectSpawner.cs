using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public void SpawnPrefab(int i)
    {
        GameObject clone = Instantiate(prefabs[i], Vector3.zero, Quaternion.identity);
    }
}
