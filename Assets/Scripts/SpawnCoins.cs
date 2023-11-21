using UnityEngine;

public class SpawnCoins : MonoBehaviour
{  
    private SpawnArea[] _spawnAreas;
    private int _numberPoint;

    void Start()
    {
        _spawnAreas = gameObject.GetComponentsInChildren<SpawnArea>();
    }

    public Vector2 GetSpawnPositionCoin()
    {
        Vector2 position;
        float positionX;
        float dividerInHalf = 2;

        _numberPoint = Random.Range(0, _spawnAreas.Length);

        float lengthPoint = _spawnAreas[_numberPoint].transform.localScale.x;
        float positionPointY = _spawnAreas[_numberPoint].transform.position.y;
        float positionPointX = _spawnAreas[_numberPoint].transform.position.x;
        float edgeDistance = lengthPoint / dividerInHalf;
      
        positionX = Random.Range(positionPointX - edgeDistance, positionPointX + edgeDistance);
        position = new Vector2(positionX, positionPointY);        

        return position;
    } 
}
