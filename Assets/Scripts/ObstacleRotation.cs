using UnityEngine;
using DG.Tweening;

public class ObstacleRotation : MonoBehaviour
{
	[SerializeField] float rotationDuration;

	void Start ()
	{
		transform
			.DORotate (new Vector3 (0f, 0f, 1f), rotationDuration)
			.SetLoops (-1, LoopType.Incremental);
	}
}
