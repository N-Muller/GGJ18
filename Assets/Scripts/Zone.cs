using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Zone : MonoBehaviour, IPointerClickHandler {

	public bool flou = true;
	public Sprite imagePasfloue;
	public Sprite imageFloue;

	void OnEnable(){
		//TODO si player traitre :
		//GetComponent<Image> ().sprite = imagePasfloue;
		//Else
		GetComponent<Image> ().sprite = flou?imageFloue:imagePasfloue;
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		flou = false;
		GetComponent<Image> ().sprite = flou?imageFloue:imagePasfloue;
	}
	#endregion
}
