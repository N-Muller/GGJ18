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

	public bool principaleRevelee = false;
	public bool suivanteRevelee = false;
	public bool precedanteRevelee = false;

	public bool isOnTable = false;
	public int slotOnTable = -1;
}
