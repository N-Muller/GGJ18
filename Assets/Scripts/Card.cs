using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler {

	public bool selected = false;
	public float upFactor = 100.0f;
	public float scaleFactor = 2.0f;
	public Vector3 startPos;
	public List<Zone> zones;

	public CardData data;

	public Text titre;
	public Zone principale, precedent, suivant;


	[ContextMenu("Initialize")]
	void ContexMenuInit()
	{
		zones = new List<Zone> (GetComponentsInChildren<Zone> ());
	}


	// Update is called once per frame
	void Start() {
		setActiveZone (false);
		startPos = transform.position;
	}


	public void Initialize(CardData data, string spriteFolderPath)
	{
		this.data = data;

		if (Player.LocalPlayer.role == Player.Role.Fourbe) {
			precedent.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + data.imagePrecedent);
			suivant.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + data.imageSuivant);
			principale.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + data.imagePrincipale);

			precedent.onClick = () => {
				this.data.imagePrecedent = Constants.Images [Random.Range (0, Constants.Images.Length)];
				Player.LocalPlayer.CmdReveal (JsonUtility.ToJson (this.data));
			};

			suivant.onClick = () => {
				this.data.imageSuivant = Constants.Images [Random.Range (0, Constants.Images.Length)];
				Player.LocalPlayer.CmdReveal (JsonUtility.ToJson (this.data));
			};
		} else {
			if (data.precedanteRevelee) {
				precedent.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + data.imagePrecedent);
				precedent.enabled = false;
			}
			else {
				precedent.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + Constants.UnknownZone);
				precedent.enabled = true;


				precedent.onClick = () => {
					this.data.precedanteRevelee = true;
					Player.LocalPlayer.CmdReveal ( JsonUtility.ToJson (this.data));
				};

			}
			if (data.suivanteRevelee) {
				suivant.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + data.imageSuivant);
				suivant.enabled = false;
			}
			else {
				suivant.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + Constants.UnknownZone);
				suivant.enabled = true;

				suivant.onClick = () => {
					this.data.suivanteRevelee = true;
					Player.LocalPlayer.CmdReveal (JsonUtility.ToJson (this.data));
				};
			}

			if (data.principaleRevelee) {
				principale.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + data.imagePrincipale);
				principale.enabled = false;
			}
			else {
				principale.image.sprite = ResourcesLoader.Load<Sprite> (spriteFolderPath + Constants.UnknownZone);
				principale.enabled = true;

				principale.onClick = () => {
					this.data.principaleRevelee = true;
					Player.LocalPlayer.CmdReveal (JsonUtility.ToJson (this.data));
				};
			}
			

		}

		titre.text = data.titre;

		if (data.isOnTable) {
			gameObject.SetActive (true);
			gameObject.transform.parent = DropContainer.ByName ["Table"].dms [data.slotOnTable].transform;
			gameObject.transform.localPosition = Vector3.zero;
		} else if (!Player.LocalPlayer.hand.Contains (data.id)) {
			gameObject.SetActive (false);
			gameObject.transform.parent = CardFactory.Instance.transform;
		}
	}



	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		selected = true;
		setActiveZone (true);
		transform.position = transform.position +Vector3.up*upFactor;
		transform.localScale += new Vector3(scaleFactor,scaleFactor,0f);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		selected = false;
		setActiveZone (false);


		transform.localScale -= new Vector3(scaleFactor,scaleFactor,0f);
		transform.position = startPos;
	}

	#endregion

	void setActiveZone(bool b){
		foreach(Zone zone in zones)
		{
			zone.enabled = b;	
			zone.GetComponent<Image> ().raycastTarget = b;
		}
	}

}
