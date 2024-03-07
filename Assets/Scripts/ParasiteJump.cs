using UnityEngine;

public class ParasiteJump : MonoBehaviour
{
    [SerializeField] private float jumpDistance, slowPower;

    [SerializeField] private float currentTime;

    [SerializeField] private LayerMask solidMouse, solidPlayer;

    private Ray hit;

    private bool ready;

    private void Update()
    {
        AxisInput();
    }

    private void AxisInput()
    {
        if (Input.GetMouseButtonDown(1)) /*Замедляем время*/
        {
            currentTime = Time.fixedDeltaTime;

            Time.timeScale = slowPower;

            Time.fixedDeltaTime *= Time.timeScale;

            ready = true;
        }
        if (Input.GetMouseButtonUp(1) && ready == true) /*Совершаем рывок*/
        {
            hit = Camera.main.ScreenPointToRay(Input.mousePosition);

            Time.timeScale = 1;

            Time.fixedDeltaTime = currentTime;

            RaycastHit hitInfo;

            if (Physics.Raycast(hit, out hitInfo, float.MaxValue, solidMouse))
            {
                RaycastHit raycast;

                Vector3 jumpPoint = new Vector3(hitInfo.point.x, hitInfo.point.y, 0);

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
