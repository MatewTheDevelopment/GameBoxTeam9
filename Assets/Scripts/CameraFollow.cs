using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject currentCumera;

    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float speed;

    private void Update()
    {
        currentCumera.transform.position = Vector3.MoveTowards(currentCumera.transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
