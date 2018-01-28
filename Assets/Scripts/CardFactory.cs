using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour {

	public static CardFactory Instance;

	public GameObject CardPrefab;
	public List<Card> Cards;

	public string SpriteFolderPath = "Sprites/";

	void Start()
	{
		Instance = this;
		Cards = new List<Card> ();
	}

	public Card CreateCard (CardData data)
	{
		GameObject go = Instantiate (CardPrefab, transform);
		Card card = go.GetComponent<Card> ();

		card.Initialize (data, SpriteFolderPath);

		Cards.Add (card);

		go.SetActive (false);

		return card;
	}

	public void UpdateCards( List<CardData> datas )
	{
		for (int i = 0; i < datas.Count; i++) {
			if (i < Cards.Count) {
				Cards [i].Initialize (datas [i], SpriteFolderPath);
			} else {
				CreateCard (datas [i]);
			}
		}

		if (datas.Count < Cards.Count) {
			Debug.LogError ("This was not expected");
		}
	}


	public void UpdateCard( CardData data )
	{
		print (JsonUtility.ToJson (data));
		Cards [data.id].Initialize (data, SpriteFolderPath);
	}
}
