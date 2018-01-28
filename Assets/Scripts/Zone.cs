using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Zone : MonoBehaviour, IPointerClickHandler {

	public bool flou = true;
	public Sprite imagePasfloue;
	public Sprite imageFloue;
	public Image image;


	void OnEnable(){
		//TODO si player traitre :
		//GetComponent<Image> ().sprite = imagePasfloue;
		//Else
		/*if (isActiveAndEnabled == true) {
			GetComponent<Image> ().sprite = flou ? imageFloue : imagePasfloue;
		}*/
	}

	void Start(){

	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		/*if (isActiveAndEnabled == true) {
			flou = false;
			GetComponent<Image> ().sprite = flou ? imageFloue : imagePasfloue;
		}*/
	}
	#endregion
}
