using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScript : MonoBehaviour
{
    public bool isFlat = true;
    private Rigidbody rigid;

    //// Code based on https://www.youtube.com/watch?v=Gq97KsLLI68&ab_channel=wolfscrygames 
    //// and https://answers.unity.com/questions/1653073/change-gyroscope-forward.html starts here
    //private Quaternion correctionQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        //Input.gyro.enabled = true;
        //Debug.Log("Gyro Enabled");
        //correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = correctionQuaternion * GyroToUnity(Input.gyro.attitude);        

        Vector3 tilt = Input.acceleration;

        if (isFlat)
            tilt = Quaternion.Euler(90, 0, 0) * tilt;

        rigid.AddForce(tilt);
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    // Code based on YouTube video and Unity Questions & Answers end here.
}
