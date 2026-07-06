using UnityEngine;

public class DecoyEmitter : MonoBehaviour
{
    public GameObject player;
    public GameObject decoy;
    public float destroyTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Invoke(nameof(SpawnDecoy), 0.7f);
        Destroy(gameObject, destroyTime + 1);
    }

    public void SpawnDecoy()
    {
        GameObject instantiatedDecoy = Instantiate(decoy, transform.position, transform.rotation);
        Destroy(instantiatedDecoy, destroyTime);
        instantiatedDecoy.GetComponent<DecoyMovement>().maxMoveTime = destroyTime;
        instantiatedDecoy.GetComponent<DecoyMovement>().decoyMove = true;
        GameManager.Instance.followDecoy = true;
    }

    void OnDestroy()
    {
        GameManager.Instance.followDecoy = false;
    }
}
