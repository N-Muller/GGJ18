

public static class Constants {

	public static int HandSize = 3;
	public static int DeckSize = 6;
	public static int PlayerCount = 2;
	public static int TableSize = 4;
	public static int MaxHandSize = 4;

	public static string[] Images = {
		"armes",
		"bague",
		"bourse",
		"boussole", 
		"cache",
		"ceinture",
		"chapeau",
		"crochet",
		"jambe",
		"peroquet",
		"pipe",
		"vue"
	};

	public static string UnknownZone = "unknown";
	public static string Croix = "croix";

	public static string IdToImageName ( int id )
	{
		return id == -1 ? Croix : Images [id];
	}

	public static int DCNameToSize (string name)
	{
		switch (name) {
			case "Table":
				return TableSize;
			case "Main":
				return MaxHandSize;
			case "PlayerDrops":
				return PlayerCount - 1;
			default:
				return -1;
		}
	}
}
