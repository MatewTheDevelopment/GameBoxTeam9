using UnityEngine;

public class ParasiteController : MonoBehaviour
{
	[SerializeField] private float speed = 1.5f;
	[SerializeField] private float acceleration = 100;
	[SerializeField] private float contactDistance = 1.25f;
	[SerializeField] private float gravityForce = 100;

	[SerializeField] private float currentTime, maxTime;

	private int layerMask;

	private float horizontalAxis;

	private Rigidbody body;

	private Vector3 direction;
	private Vector3 gravity;

	private void Awake()
	{
		body = GetComponent<Rigidbody>();
		Physics.gravity = Vector3.zero;

		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;

		direction = Vector3.down;
		gravity = Vector3.down;

		GetDirection(direction, Mathf.Infinity);
	}

    private void LateUpdate()
	{
		horizontalAxis = Input.GetAxis("Horizontal");

		GetDirection(direction * horizontalAxis, contactDistance);

		Debug.DrawRay(transform.position, direction * horizontalAxis, Color.red);
	}

	void FixedUpdate()
	{
		body.AddForce(gravity * gravityForce * body.mass);

		body.AddForce(direction * horizontalAxis * speed * acceleration * body.mass);

		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
		}

		if (Mathf.Abs(body.velocity.y) > speed)
		{
			body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed);
		}
	}

	private void GetDirection(Vector3 currentDirection, float distance)
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, currentDirection, out hit, distance, layerMask))
		{
			Normalized(hit.normal);
		}

		AxisDetection();
	}

	private void Normalized(Vector3 normal)
	{
		gravity = -normal.normalized;
		direction = Vector3.Cross(normal, Vector3.forward).normalized;
	}

	private void AxisDetection()
	{
		int arr = 6;

		float j = 0;
		float[] distance = new float[arr];

		Vector2[] normal = new Vector2[arr];

		for (int i = 0; i < arr; i++)
		{
			var x = Mathf.Sin(j);
			var y = Mathf.Cos(j);

			j += 360 * Mathf.Deg2Rad / arr;

			Vector3 dir = transform.TransformDirection(new Vector3(x, y, 0));

			RaycastHit hit;

			if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, layerMask))
			{
				distance[i] = hit.distance;
				normal[i] = hit.normal;

				Debug.DrawLine(transform.position, hit.point, Color.cyan);
			}
			else
			{
				distance[i] = Mathf.Infinity;
			}
		}

		float min = Mathf.Min(distance);

		for (int i = 0; i < arr; i++)
		{
			if (distance[i] == min && min > contactDistance)
			{
				Normalized(normal[i]);
			}
		}
	}
}

