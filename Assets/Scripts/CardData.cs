using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData {
	public string titre;

	public string imagePrincipale;
	public string imagePrecedent;
	public string imageSuivant;

	public int id, idprec, idnext;
}
