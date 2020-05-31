using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    CursorLockMode lockMode;

    // Start is called before the first frame update
    void Start()
    {
        lockMode = CursorLockMode.Locked;
        Cursor.lockState = lockMode;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
