using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	#region Singleton class: PlayerMovement

	public static PlayerMovement Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	#endregion

	[SerializeField] CircleCollider2D redBallCollider;
	[SerializeField] CircleCollider2D blueBallCollider;

	[SerializeField] float speed;
	[SerializeField] float rotationSpeed;

	Rigidbody2D rb;
	Vector3 startPosition;
	Camera cam;
	float touchPosX = 0f;

	void Start ()
	{
		startPosition = transform.position;

		rb = GetComponent<Rigidbody2D> ();

		cam = Camera.main;

		MoveUp ();
	}

	void Update ()
	{
		if (!GameManager.Instance.isGameover) {
			//Add mobile inputs (touch on screen sides)
			if (Input.GetMouseButtonDown (0))
				touchPosX = cam.ScreenToWorldPoint (Input.mousePosition).x;

			if (Input.GetMouseButton (0)) {
				if (touchPosX > 0.01f)
					RotateRight ();
				else
					RotateLeft ();
			} else
				rb.angularVelocity = 0f;


			//Unity editor inputs < & > keys
			#if UNITY_EDITOR
			if (Input.GetKey (KeyCode.LeftArrow))
				RotateLeft ();
			else if (Input.GetKey (KeyCode.RightArrow))
				RotateRight ();

			//stop rotation when key is released
			if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow))
				rb.angularVelocity = 0f;
			#endif

		}
	}

	void MoveUp ()
	{
		rb.velocity = Vector2.up * speed;
	}

	void RotateLeft ()
	{
		rb.angularVelocity = rotationSpeed;
	}

	void RotateRight ()
	{
		rb.angularVelocity = -rotationSpeed;
	}

	public void Restart ()
	{
		redBallCollider.enabled = false;
		blueBallCollider.enabled = false;
		rb.angularVelocity = 0f;
		rb.velocity = Vector2.zero;

		//back to start position
		transform
			.DORotate (Vector3.zero, 1f)
			.SetDelay (1f)
			.SetEase (Ease.InOutBack);

		transform
			.DOMove (startPosition, 1f)
			.SetDelay (1f)
			.SetEase (Ease.OutFlash)

			.OnComplete (() => {
			redBallCollider.enabled = true;
			blueBallCollider.enabled = true;

			GameManager.Instance.isGameover = false;

			MoveUp ();
		});
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("LevelEnd")) {
			Destroy (other.gameObject);
			Debug.Log ("Win");

			int currentLevelIndex = SceneManager.GetActiveScene ().buildIndex;

			if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
				SceneManager.LoadSceneAsync (++currentLevelIndex);
		}
	}
}
