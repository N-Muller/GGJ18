using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	private Color normalColor;
	public Color highlightColor = Color.grey;
	public int maxElements = 1;
	public DropContainer container;

	public void OnEnable ()
	{
		normalColor = GetComponent<Renderer> ().material.color;
	}

	public void OnDrop(PointerEventData data)
	{
		GetComponent<Renderer>().material.color = normalColor;

		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && gameObject.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe>().target.transform.SetParent (gameObject.transform);
			dropItem.GetComponent<DragMe>().target.transform.SetAsFirstSibling ();
		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && gameObject.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe> ().dropable = true;

			GetComponent<Renderer> ().material.color = highlightColor;
		}
	}

	public void OnPointerExit(PointerEventData data)
	{		
		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && gameObject.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe> ().dropable = false;
		}
		GetComponent<Renderer>().material.color = normalColor;
	}
	
	private GameObject GetDropItem(PointerEventData data)
	{
		var originalObj = data.pointerDrag;

		return originalObj;
	}
}
