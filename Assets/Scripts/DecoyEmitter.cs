using UnityEngine;

public class DecoyEmitter : MonoBehaviour
{
    public GameObject player;
    public GameObject decoy;
    public float destroyTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void SpawnDecoy()
    {
        GameObject instantiatedDecoy = Instantiate(decoy, transform.position, transform.rotation);
        Destroy(instantiatedDecoy, destroyTime);
        // instantiatedDecoy.
    }
}
