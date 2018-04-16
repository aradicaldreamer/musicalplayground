using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearningGridController : MonoBehaviour {

	[SerializeField] float xSize = 1f;
	[SerializeField] float ySize = 1f;
	[SerializeField] float spacing = 0.1f;

	public static MachineLearningGridController main;
	public GameObject cubeCollectorPrefab;
    public List<CubeCollectorController> collectorList = new List<CubeCollectorController>();
    public OSC osc;
    private float MsgTimer  = 0.0f;
    [SerializeField] float MessageInterval = 1.0f;
    [SerializeField] bool useWekinator = false;

	void Awake() {
		main = this;
	}
	void Start () {
        
        // instantiating collector cubes for machine learning.
		float xPos,yPos = 0;

		for (float i = 0; i < xSize ; i += spacing) {
			xPos = i;
			for (float ii = 0; ii < ySize ; ii += spacing) {
				yPos = ii;

				Vector3 position = new Vector3 (xPos, 0, yPos);

				GameObject inst = Instantiate (cubeCollectorPrefab, position, Quaternion.identity);
                collectorList.Add(inst.GetComponent<CubeCollectorController>());

			}
		}
	}
//	private void Update()
//	{
//		
//		if (MsgTimer < Time.time) {
//			SendMessage();
//            
//            MsgTimer = Time.time + MessageInterval;
//		}
//	}

//	void SendMessage() {
//        
//        if (useWekinator)
//        {
//            OscMessage message = new OscMessage();
//            message.address = "/wek/inputs";
//
//            for (int i = 0; i < collectorList.Count; i++)
//            {
//                message.values.Add(collectorList[i].weightValue);
//            }
//
//            osc.Send(message);
//        }
  //  }

	
}
