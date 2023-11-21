using System.Collections;
using UnityEngine;

public class AllCoins : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private int _maxCountCoin;    
    [SerializeField] private SpawnCoins _spawnPosition;
    [SerializeField] private float _waitStartsecond;

    private Coin[] _coins;
    private Coroutine _coroutineCreat = null;
    private bool isAllCoins = false;

    private void Start()
    {
        StartCoroutine(Creat());
    }    

    public void StartSpawn()
    {
        if (isAllCoins)
        {
            isAllCoins = false;
            StartCoroutine(Creat());
        }            
    }

    private IEnumerator Creat()
    {
        float spawnSeconds = 5;        
        WaitForSeconds _waitForSeconds = new(spawnSeconds);

        while (isAllCoins == false)
        {
            yield return _waitForSeconds;

            _coins = gameObject.GetComponentsInChildren<Coin>();           

            if (_coins.Length < _maxCountCoin)
            {               
                Vector2 position = _spawnPosition.GetSpawnPositionCoin();                
                GameObject newObject = Instantiate(_coin.gameObject, position, Quaternion.identity);
                newObject.transform.SetParent(gameObject.transform);
            }
            else
            {
                isAllCoins = true;
                break;
            }            
        }
    }  
}
