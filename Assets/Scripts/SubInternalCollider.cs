using UnityEngine;

public class SubInternalCollider : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "ColliderCheck")
        {
            PlayerScript.instance.InternalCollider = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ColliderCheck")
        {
            PlayerScript.instance.InternalCollider = false;
        }
    }
}
