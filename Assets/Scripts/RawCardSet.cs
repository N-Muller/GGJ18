using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RawCardSet {

	public class cardIntModel{
		public int i;
		public int p;
		public int s;

		public cardIntModel(int i,int p, int s){
			this.i = i;
			this.p = p;
			this.s = s;
		}

		public CardData ToCardData ()
		{
			CardData d = new CardData ();
		
			d.titre = "" + i;

			d.id = i;
			d.idnext = s;
			d.idprec = p;

			d.imagePrincipale = Constants.IdToImageName(i);
			d.imagePrecedent = Constants.IdToImageName(p);
			d.imageSuivant = Constants.IdToImageName(s);

			return d;
		}
	}

	List<int> pool = new List<int>();
	public List<cardIntModel> solution = new List<cardIntModel>();
	List<cardIntModel> circuit = new List<cardIntModel>();

	// Use this for initialization
	public List<CardData> GenerateCards () 
	{
		initPool();
		genSolution ();
		// debugSolution ();
		genCircuit ();
		// debugCircuit ();
		// debugPool ();

		List<CardData> cards = new List<CardData> ();

		foreach (cardIntModel c in solution) {
			cards.Add (c.ToCardData ());	
		}

		foreach (cardIntModel c in circuit) {
			cards.Add (c.ToCardData ());
		}

		return cards;
	}

	void initPool()
	{
		for (int i = 0; i < Constants.DeckSize; i++) {
			pool.Add (i);
		}
	}

	void genSolution()
	{
		int precedent = -1;
		int actuel = popIndex ();
		int suivant = popIndex ();
		solution.Add (new cardIntModel (actuel, precedent, suivant));

		for (int i = 1; i < Constants.TableSize - 1; i++) 
		{
			precedent = actuel;
			actuel = suivant;
			suivant = popIndex ();
			solution.Add (new cardIntModel (actuel, precedent, suivant));
		}
		precedent = actuel;
		actuel = suivant;
		suivant = -1;
		solution.Add (new cardIntModel (actuel, precedent, suivant));

		// debugPool ();
	}

	void genCircuit(){
		int precedent = -1;
		int actuel = popIndex();
		int suivant = popIndex();
		cardIntModel firstCard =  new cardIntModel (actuel,precedent,suivant);
		circuit.Add (firstCard);
		int size = pool.Count;

		for (int i = 0; i < size; i++) {
			precedent = actuel;
			actuel = suivant;
			suivant = popIndex();
			circuit.Add (new cardIntModel (actuel,precedent,suivant));
		}
		circuit.Add (new cardIntModel (suivant,actuel,firstCard.i));
		firstCard.p = suivant; 

		// debugCircuit();
	}

	int popIndex(){
		int index = pool.ElementAt (Random.Range (0, pool.Count));
		pool.Remove (index);
		return index;
	}

	void debugSolution(){
		foreach(cardIntModel card in solution){
			Debug.Log ("precedent : "+card.p+" actuel :  "+card.i+" suivant : "+card.s);
		}
	}

	void debugPool(){
		string debugPool = "";
		foreach (int i in pool) {
			debugPool += i + " ";
		}
		Debug.Log (debugPool);
	}

	void debugCircuit(){
		foreach(cardIntModel card in circuit){
			Debug.Log ("precedent : "+card.p+" actuel :  "+card.i+" suivant : "+card.s);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
