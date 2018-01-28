using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Zone : MonoBehaviour, IPointerClickHandler {

	private Image _i;
	public Image image { 
		get { 
			if (_i == null)
				_i = GetComponent<Image> (); 
			return _i;
		}
	}

	public Card card;
	public int card_id;

	public Action onClick;


	void Start(){
		card_id = card.data.id;
	}

	void OnEnable(){
		//TODO si player traitre :
		//GetComponent<Image> ().sprite = imagePasfloue;
		//Else
		/*if (isActiveAndEnabled == true) {
			GetComponent<Image> ().sprite = flou ? imageFloue : imagePasfloue;
		}*/
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		if (isActiveAndEnabled == true) {
			if (onClick != null)
				onClick.Invoke ();
		}
	}
	#endregion
}
