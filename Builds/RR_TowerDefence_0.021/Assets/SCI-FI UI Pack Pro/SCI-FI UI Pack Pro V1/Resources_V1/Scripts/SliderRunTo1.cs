using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderRunTo1 : MonoBehaviour
{
	public GameObject TD_GlobalControl;
	private TD_GlobalControl GlobalControl;
	public bool b = true;
	public Slider slider;
	public float speed = 0.5f;

	float time = 0f;

	void Start()
	{
		slider = GetComponent<Slider>();
		GlobalControl = TD_GlobalControl.GetComponent<TD_GlobalControl>();
	}

	void Update()
	{
		if (b)
		{
			time += Time.deltaTime * speed;
			slider.value = time;

			if (time > 1)
			{
				b = false;
				time = 0;
				GlobalControl.LoadTitleScreen();
			}
		}
	}
}
