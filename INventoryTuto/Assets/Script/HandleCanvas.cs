using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleCanvas : MonoBehaviour 
{

    private Canvas canvas;
    private Vector2 position;

    public GameObject hoverObject;



	// Use this for initialization
	void Start () 
    {
        canvas = GetComponent<Canvas>();
	}

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hoverObject = (GameObject)Instantiate(hoverObject);
            hoverObject.transform.SetParent(this.transform);


            print("Mouse Click");

    

            //position = (Input.mousePosition - canvas.transform.position);

            //position = new Vector2((Input.mousePosition - canvas.transform.localPosition).x, 
            //    (Input.mousePosition - canvas.transform.localPosition).y);

            

            //position.Set(position.x, position.y - hoverYOffset);

            print(position);

        }

        if (hoverObject != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition,
                canvas.worldCamera, out position);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);    
        }

        
    }
}
