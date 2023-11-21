using System.Linq;
using UnityEngine;

public class PointSpawnMonsters : MonoBehaviour
{
    [SerializeField] private Path[] _paths;
    [SerializeField] private Monster[] _monstersPrefab;
    [SerializeField] private int _countMonsters;
    private Monster[] _monsters;
    private int _distanceBetweenObject = 1;

    public void Start()
    {
        Spawn();
    }

    private void Spawn()
    {        
        while (checkQuantity())
        {
            Path selectPath = _paths[Random.Range(0, _paths.Length)];
            Monster selectMonster = _monstersPrefab[Random.Range(0, _monstersPrefab.Length)];

            Point[] points = selectPath.Points;
            int numbeSelectrPoint = Random.Range(0, selectPath.Points.Length);

            if (_monsters.Any(monster => Mathf.Abs(monster.transform.position.x - points[numbeSelectrPoint].transform.position.x) <= _distanceBetweenObject) == false)
            {
                selectMonster.Spawn(points, numbeSelectrPoint).transform.SetParent(gameObject.transform);   
            }         
        }        
    }

    private bool checkQuantity()
    {
       _monsters = gameObject.GetComponentsInChildren<Monster>();
         
        if(_monsters.Length >= _countMonsters)
            return false;
        else return true;
    }
}
