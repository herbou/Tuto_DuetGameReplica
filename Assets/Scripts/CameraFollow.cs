using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	Transform target;

	void Start ()
	{
		target = PlayerMovement.Instance.transform;
	}

	void LateUpdate ()
	{
		transform.position = target.position;
	}
}
