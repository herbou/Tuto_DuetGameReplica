using UnityEngine;

public class Splatters : MonoBehaviour
{
	#region Singleton class: Splatters

	public static Splatters Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}

	#endregion

	[SerializeField] Color[] colors = new Color[2];
	[SerializeField] GameObject splatterPrefab;
	[SerializeField] Sprite[] splatterSprites;

	public void AddSplatter (Transform obstacle, Vector3 pos, int colorIndex)
	{
		GameObject splatter = Instantiate (
			                      splatterPrefab, 
			                      pos, 
			                      Quaternion.Euler (new Vector3 (0f, 0f, Random.Range (-320f, 320f))),
			                      obstacle
		                      );
			
		SpriteRenderer sr = splatter.GetComponent <SpriteRenderer> ();
		sr.color = colors [colorIndex];
		sr.sprite = splatterSprites [Random.Range (0, splatterSprites.Length)];
	}
}
