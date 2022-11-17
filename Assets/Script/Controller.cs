using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject _selectObj;

    private Vector3 _planeHitPos;
    private Ray _nowRay;
    private RaycastHit _hit;


    Vector3 GetPlaneHitPosition()
    {
        if (Physics.Raycast(_nowRay, out _hit))
        {
            if (_hit.collider.tag == "plane")
            {
                Transform objectHit = _hit.transform;
                Vector3 _planeHitPos = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
                return _planeHitPos;
            }
        }

        return _planeHitPos;
    }

    GameObject DoSelectObject(GameObject obj)
    {

        obj.GetComponent<Collider>().enabled = false;
        _selectObj = obj;

        return _selectObj;

    }

    void DoDeSelectObject()
    {

        if (_selectObj != null)
        {
            _selectObj.GetComponent<Collider>().enabled = true;
        }
        _selectObj = null;
    }


    void MoveSelectedObject()
    {
        Vector3 planeHitPosRemoveY = new Vector3(_planeHitPos.x, _selectObj.transform.position.y, _planeHitPos.z);
        _selectObj.transform.position = (planeHitPosRemoveY + offset);
    }

    private void RayUpdateToCam()
    {
        _nowRay = _camera.ScreenPointToRay(Input.mousePosition);
    }


    void Update()
    {
        RayUpdateToCam();
        _planeHitPos = GetPlaneHitPosition();


        if (Input.GetMouseButtonDown(1))
        {
            // click mouse
            DoDeSelectObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            // lift mouse
            DoDeSelectObject();
        }

        if (Input.GetMouseButton(0))
        {
            // hold mouse
            if (_selectObj == null)
            {
                if (Physics.Raycast(_nowRay, out _hit))
                {
                    if (_hit.collider.tag == "moveable")
                    {
                        GameObject objectHit = _hit.transform.gameObject;
                        DoSelectObject(objectHit);
                    }
                }
            }
            else
            {
                MoveSelectedObject();
            }
        }

    }
}