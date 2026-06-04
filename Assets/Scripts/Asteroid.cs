using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public OreData oreData;

    public GameObject explosion;
    public GameObject hitEffect;

    public int minAmountToDrop;
    public int maxAmountToDrop;
    float currentHealth;

    void Start()
    {
        currentHealth = oreData.oreDurability;
    }
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        GameManager.Instance.AddItem(oreData, Random.Range(minAmountToDrop, maxAmountToDrop));
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
