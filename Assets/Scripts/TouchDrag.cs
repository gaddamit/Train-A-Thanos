using UnityEngine;

public class TouchDrag : MonoBehaviour
{
    public Camera MainCamera;
    private Transform tranform;
    private Vector3 offset;
    private Plane dragPlane;
    public bool isDraggingX = true;
    public bool isDraggingY = true;
    public bool isDraggingZ = true;
    private bool shouldDrag = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                shouldDrag = false;
                Ray ray = MainCamera.ScreenPointToRay(touch.position);
                if(Physics.Raycast(ray, out RaycastHit hit))
                {
                    shouldDrag = hit.collider.gameObject == gameObject;

                    dragPlane = new Plane(-ray.direction, tranform.position);
                    float distance;
                    dragPlane.Raycast(ray, out distance);
                    offset = tranform.position - ray.GetPoint(distance);
                }
            }
            else if(shouldDrag && touch.phase == TouchPhase.Moved)
            {
                Ray ray = MainCamera.ScreenPointToRay(touch.position);
                float distance;
                dragPlane.Raycast(ray, out distance);
                Vector3 newPosition = ray.GetPoint(distance) + offset;
                newPosition.x = isDraggingX ? newPosition.x : tranform.position.x;
                newPosition.y = isDraggingY ? newPosition.y : tranform.position.y;
                newPosition.z = isDraggingZ ? newPosition.z : tranform.position.z;
                tranform.position = newPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                offset = Vector3.zero;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            shouldDrag = false;
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                shouldDrag = hit.collider.gameObject == gameObject;
                dragPlane = new Plane(-ray.direction, tranform.position);
                float distance;
                dragPlane.Raycast(ray, out distance);
                offset = tranform.position - ray.GetPoint(distance);
            }
        }
        else if(shouldDrag && Input.GetMouseButton(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            dragPlane.Raycast(ray, out distance);
            Vector3 newPosition = ray.GetPoint(distance) + offset;
            newPosition.x = isDraggingX ? newPosition.x : tranform.position.x;
            newPosition.y = isDraggingY ? newPosition.y : tranform.position.y;
            newPosition.z = isDraggingZ ? newPosition.z : tranform.position.z;
            tranform.position = newPosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            offset = Vector3.zero;
        }
    }
}
