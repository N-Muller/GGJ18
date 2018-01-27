using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zone : MonoBehaviour, IPointerClickHandler {

	public bool flou = true;
	public Sprite imagePasfloue;
	public Sprite imageFloue;

	void OnEnable(){
		GetComponent<SpriteRenderer> ().sprite = flou?imageFloue:imagePasfloue;
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		flou = false;
	}
	#endregion
}
