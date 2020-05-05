using UnityEngine;
using DG.Tweening;

public class ObstacleSliding : MonoBehaviour
{
	[SerializeField] float slideDistance;
	[SerializeField] float slideDuration;

	void Start ()
	{
		transform
			.DOLocalMoveX (slideDistance, slideDuration)
			.SetLoops (-1, LoopType.Yoyo)
			.SetEase (Ease.InOutBack);
	}
}
