using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{

	[SerializeField]
	GameObject objectToHide;
	// Update is called once per frame
	public void HideGameObject()
	{
		objectToHide.SetActive(false);
		Time.timeScale = 1;
	}
}