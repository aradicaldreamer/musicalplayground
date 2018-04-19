using UnityEngine;

public class TrackedPerson
{
	public int id;
	public int spaceId = -1;
	public int age = 0;
	public int dead = 0;
	public float centroidX;
	public float centroidY;
	public float velocityX;
	public float velocityY;

	public float positionX;
	public float positionY;

	public Color color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 1.0f);

}
