using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class KeyButton : MonoBehaviour {

    [SerializeField] KeyCode key;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(key))
        {
            button.onClick.Invoke();
        }
	}
}
