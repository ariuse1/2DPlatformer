using UnityEngine;

public class Path : MonoBehaviour
{
    public Point[] Points { get; private set; }

    private void Awake()
    {
        Points = gameObject.GetComponentsInChildren<Point>();
    }
}
