using UnityEngine;
using System.Collections;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{

	public float delay = 0.1f;
	public string fullText;
	private float TextLength;
	private string currentText = "";
	private bool skipText = false;
	public GameObject Options;
	public Animator KingAnimation;


	// Use this for initialization
	void Start()
	{
		fullText += "-";
		StartCoroutine(ShowText());
		TextLength = fullText.Length;
		KingAnimation.SetBool("Idle", false);
	}

	IEnumerator ShowText()
	{
		for (int i = 0; i < fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			this.GetComponent<TextMeshProUGUI>().text = currentText;
			TextLength -= 1;
			if (skipText == false) {
				yield return new WaitForSeconds(delay);
			}
			
		}
		
	}
	void Update()
	{
		if (TextLength == 1)
		{
			Options.SetActive(true);
			KingAnimation.SetBool("Idle", true);
			TextLength = 0;
		}
		if (Input.GetKeyDown("space"))
		{
			skipText = true;
		}
	}
}
