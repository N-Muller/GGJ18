using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public GameObject container;
	private Color normalColor;
	public Color highlightColor = Color.grey;
	public int maxElements = int.MaxValue;


	public void OnEnable ()
	{
		normalColor = GetComponent<Renderer> ().material.color;
	}

	public void OnDrop(PointerEventData data)
	{
		GetComponent<Renderer>().material.color = normalColor;

		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && container.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe>().target.transform.SetParent (container.transform);
			dropItem.GetComponent<DragMe>().target.transform.SetAsFirstSibling ();

		}
	}

	public void OnPointerEnter(PointerEventData data)
	{
		Debug.Log ("Enter");
		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && container.transform.childCount < maxElements) {
			dropItem.GetComponent<DragMe> ().dropable = true;

			GetComponent<Renderer> ().material.color = highlightColor;
		}
	}

	public void OnPointerExit(PointerEventData data)
	{		
		GameObject dropItem = GetDropItem(data);
		if (dropItem != null && dropItem.GetComponent<DragMe> () != null && container.transform.childCount < maxElements) {
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
