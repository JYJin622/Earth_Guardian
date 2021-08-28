using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeObject : MonoBehaviour
{
    public float fadeTime;
	public bool isCalled = false;
	// Start is called before the first frame update
    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
	}

	public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
	}

	public void CallFade()
    {
		FadeIn(fadeTime);
	}

	// 투명 -> 불투명
	IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		Color tempColor = sr.color;
		while (tempColor.a < 1f)
		{
			tempColor.a += Time.deltaTime / fadeOutTime;
			sr.color = tempColor;

			if (tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}

		sr.color = tempColor;
		if (nextEvent != null) nextEvent();
	}

	// 불투명 -> 투명
	IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		Color tempColor = sr.color;
		while (tempColor.a > 0f)
		{
			tempColor.a -= Time.deltaTime / fadeOutTime;
			sr.color = tempColor;

			if (tempColor.a <= 0f) tempColor.a = 0f;

			yield return null;
		}
		sr.color = tempColor;
		if (nextEvent != null) nextEvent();
	}
    private void Start()
    {
		if(isCalled == true)
        {
			FadeIn(fadeTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.name == "MainCharacter")
		{
			FadeIn(fadeTime);
		}
	}


}
