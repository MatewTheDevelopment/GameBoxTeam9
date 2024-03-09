using UnityEngine;

public class ParasiteJump : MonoBehaviour
{
    [SerializeField] private float jumpDistance, slowPower;

    [SerializeField] private float currentTime;

    [SerializeField] private GameObject lineRenderer;

    [SerializeField] private LayerMask solidMouse, solidPlayer;

    private Ray hit;

    private bool ready;

    private void Update()
    {
        AxisInput();

        hit = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;

        lineRenderer.GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (Physics.Raycast(hit, out raycastHit, float.MaxValue, solidMouse))
        {
            lineRenderer.GetComponent<LineRenderer>().SetPosition(1, new Vector3(raycastHit.point.x, raycastHit.point.y, transform.position.z));
        }
    }

    private void AxisInput()
    {
        if (Input.GetMouseButtonDown(1)) /*Замедляем время*/
        {
            currentTime = Time.fixedDeltaTime;

            Time.timeScale = slowPower;

            Time.fixedDeltaTime *= Time.timeScale;

            ready = true;

            lineRenderer.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1) && ready == true) /*Совершаем рывок*/
        {
            lineRenderer.SetActive(false);

            Time.timeScale = 1;

            Time.fixedDeltaTime = currentTime;

            RaycastHit hitInfo;

            if (Physics.Raycast(hit, out hitInfo, float.MaxValue, solidMouse))
            {
                RaycastHit raycast;

                Vector3 jumpPoint = new Vector3(hitInfo.point.x, hitInfo.point.y, transform.position.z);

                if (Physics.Raycast(transform.position, jumpPoint - transform.position, out raycast, jumpDistance, solidPlayer))
                {
                    if(raycast.collider.CompareTag("Wall") /*Если мы столкнулись со стеной, то...*/)
                    {
                        transform.position = raycast.point;

                        ready = false;
                    }
                    //else if (raycast.collider.CompareTag("Enemy"))
                    //{
                        //Сюда логику захвата бедолаги можно засунуть, в общем, достаточно легко.
                    //}
                }
            }
        }
    }
}
