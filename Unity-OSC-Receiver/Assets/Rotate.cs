using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float degPerSec = 360;
    
    private void Update()
    {
        var degrees = degPerSec * Time.deltaTime;
        transform.Rotate(axis, degrees);
    }
}
