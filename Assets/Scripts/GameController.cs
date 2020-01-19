using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.MagicLeap;

public class GameController : MonoBehaviour
{
    //private MLHandKeyPose[] gestures;
    public MedicalDataController medicalDataController;
    public GameObject medicalDataControllerGO;
    public GameObject man;
    public GameObject woman;
    public GameObject line;
    public GameObject detail;
    public GameObject person;
    public GameObject historicalData;
    private bool _freeze = false;
    private bool validCondition = true;

    List<string> partsBodyList = new List<string>()
        {
            "Hip",
            "Waist",
            "Chest",
            "ShoulderR",
            "ShoulderL",
            "Neck",
            "ElbowR",
            "ElbowL",
            "WristR",
            "WristL",
            "Head",
            "KneeR",
            "KneeL",
            "AnkleR",
            "AnkleL",
            "HandR",
            "HandL",
            "FootR",
            "FootL"
        };

    List<string> facePathList = new List<string>()
        {
            "Faces/Man1",
            "Faces/Man2",
            "Faces/Woman1",
            "Faces/Woman2",
        };

    List<string> medicalImagesPathList = new List<string>()
        {
            "MedicalImages/ElbowX",
            "MedicalImages/FootX",
            "MedicalImages/HandX",
            "MedicalImages/HeadX",
            "MedicalImages/ShoulderX",
            "MedicalImages/WristX",
            "MedicalImages/Neck",
            "MedicalImages/Chest",
            "MedicalImages/Knee",
            "MedicalImages/Hip",
        };

    private Dictionary<string, Sprite> _medicalSpriteList = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> _faceSpriteList = new Dictionary<string, Sprite>();

    private GameObject _camera;
    private const float _distance = 2.0f; 
    private float speed = 30.0f;  // Speed of our cube
    private GameObject model;

    void Start()
    {
        /*MLHands.Start(); // Start the hand tracking.
        gestures = new MLHandKeyPose[6]; //Assign the gestures we will look for.
        gestures[0] = MLHandKeyPose.Ok;
        gestures[1] = MLHandKeyPose.Fist;
        gestures[2] = MLHandKeyPose.C;
        gestures[3] = MLHandKeyPose.Finger;
        gestures[4] = MLHandKeyPose.Thumb;
        gestures[5] = MLHandKeyPose.Pinch;
        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);/**/


        LoadStripes(_medicalSpriteList, medicalImagesPathList);
        LoadStripes(_faceSpriteList, facePathList);

        _camera = GameObject.Find("Main Camera");

        Sprite photo = _faceSpriteList["Man1"];
        MedicalData md = new MedicalData(
               photo,
               "MKUY-945764",
               "Jhon Peterson",
               "Aug/24/1987",
               "33",
               "M",
               "O+",
               "1.87",
               "89",
               "Hospitalized",
               "Heart Disease",
               "None"
           );

        medicalDataController.SetData(md);

        model = GameObject.Instantiate(man);
        model.transform.position = person.transform.position;
        model.transform.rotation = person.transform.rotation;
        model.transform.SetParent(person.transform);
        model.transform.localScale = new Vector3(1f, 1f, 1f);

        foreach (Transform child in model.transform)
        {
            if (child.gameObject.name != "Model")
            {
                Debug.Log(child.gameObject.name);
                Debug.Log(child.transform.position);
                GameObject dat = GameObject.Instantiate(historicalData);
                dat.transform.SetParent(person.transform);
                dat.transform.position = child.transform.position;
                if (Random.Range(0.0f, 5.0f) > 5) {
                    HistData d = new HistData(
                        _medicalSpriteList["WristX"],
                        "Dic/03/2019",
                        "cough was persistent. Cough produces blood. Pain in the chest, back and shoulders that worsens during coughing, laughing or deep breathing.",
                        "cough that was persistent. Cough produces blood. Pain in the chest, back and shoulders that worsens during coughing, laughing or deep breathing. Difficulties for breathing.");
                    d.GetComponent<HistoricalController>().SetData(d);
                }
            }
        }
    }


    private void OnDestroy()
    {
        MLHands.Stop();
    }

    bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Update()
    {
        /*if (GetGesture(MLHands.Right, MLHandKeyPose.Thumb))
        {
            person.transform.position = _camera.transform.position + _camera.transform.forward * 2.0f;
        }
        else if (GetGesture(MLHands.Left, MLHandKeyPose.C))
        {
            person.transform.Rotate(Vector3.right, +speed * Time.deltaTime);
        }
        else if (GetGesture(MLHands.Right, MLHandKeyPose.C))
        {
            person.transform.Rotate(Vector3.right, -speed * Time.deltaTime);
        }
        else if (GetGesture(MLHands.Left, MLHandKeyPose.Fist))
        {
            person.transform.Rotate(Vector3.up, +speed * Time.deltaTime);
        }
        else if (GetGesture(MLHands.Right, MLHandKeyPose.Fist))
        {
            person.transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        }/**/

        if (!_freeze)
            HeadLock(medicalDataControllerGO, 5.0f);
    }

    public void HeadLock(GameObject obj, float speed)
    {
        speed = Time.deltaTime * speed;
        Vector3 posTo = _camera.transform.position + (_camera.transform.forward * _distance);
        obj.transform.position = Vector3.SlerpUnclamped(obj.transform.position, posTo, speed);
        Quaternion rotTo = Quaternion.LookRotation(obj.transform.position - _camera.transform.position);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotTo, speed);
    }


    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }


    public void LoadStripes(Dictionary<string, Sprite> utilityModels, List<string> paths)
    {
        foreach (string utilityPath in paths)
        {
            Sprite ut = Resources.Load<Sprite>(utilityPath);
            char[] spearator = { '/' };
            string[] strlist = utilityPath.Split(spearator);
            string name = strlist[strlist.Length - 1];
            utilityModels.Add(name, (Sprite)ut);
        }
    }


    public bool ValidTime(bool condition)
    {
        bool valid = condition && validCondition;
        if (valid)
        {
            validCondition = false;
            StopCoroutine("validated");
            StartCoroutine("validated");
        }
        return valid;
    }

    IEnumerator validated()
    {
        yield return new WaitForSeconds(.5f);
        validCondition = true;
    }
}
