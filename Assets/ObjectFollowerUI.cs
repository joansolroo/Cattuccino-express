using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ObjectFollowerUI : MonoBehaviour {

    [SerializeField] public Transform objectToFollow;
    [SerializeField] public Text text;

    // Update is called once per frame
    void Update () {
        Vector3 position = this.transform.position;
        position.x = objectToFollow.transform.position.x;
        this.transform.position = position;

    }
}
