using UnityEngine;

public class Sensor : MonoBehaviour
{
    private bool isSensored = false;
    public bool IsSensored { get; set; }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Package")
        {
            IsSensored = true;
            print(other.gameObject.name + " °¨Áö!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Package")
        {
            IsSensored = false;
        }
    }
}




