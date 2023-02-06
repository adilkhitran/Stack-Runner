using UnityEngine;

public class RandomColor : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1);
    }
}
