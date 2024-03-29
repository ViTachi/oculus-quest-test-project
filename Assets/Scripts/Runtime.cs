using UnityEngine;

public class Runtime : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;       
    }
}
