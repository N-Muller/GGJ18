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

		return card;
	}
}
