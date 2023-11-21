using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float waitStartSecond = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {
            Coin coin = GetComponent<Coin>();
            AllCoins allCoins = GetComponentInParent<AllCoins>();

            allCoins.StartSpawn();
            Destroy(coin.gameObject);
        }
    }
}
