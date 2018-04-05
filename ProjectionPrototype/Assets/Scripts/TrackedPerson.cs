using UnityEngine;

public class TrackedPerson
{
	public int id;
	public int spaceId;
	public int age;
	public float centroidX;
	public float centroidY;
	public float velocityX;
	public float velocityY;

	public float positionX;
	public float positionY;

	public Color color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 1.0f);

}
