using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistData {
    public HistData(
        Sprite photo,
        string date,
        string shortDescription,
        string longDescription
        ) {
        Photo = photo;
        Date = date;
        ShortDescription = shortDescription;
        LongDescription = longDescription;
    }
    public Sprite Photo { private set; get; }
    public string Date { private set; get; }
    public string ShortDescription { private set; get; }
    public string LongDescription { private set; get; }
}
public class HistoricalController : MonoBehaviour
{
    public GameObject shortDescriptionGO;
    public GameObject longDescriptionGO;
    public GameObject PhotoGO;
    public GameObject Date1GO;
    public GameObject Date2GO;

    public GameObject TemplateShort;
    public GameObject TemplateLong;

    public bool shortVisible = true; 
    private GameObject _camera;

    void Start()
    {
        _camera = GameObject.Find("Main Camera");
    }

    public void SetData(HistData dat) {
        TemplateShort.SetActive(true);
        TemplateLong.SetActive(false);
    }

    /*void SetShortVisible() {
        shortVisible != shortVisible;
        TemplateShort.SetActive(shortVisible);
        TemplateLong.SetActive(!shortVisible);
    }/**/

    void Update()
    {
        HeadRot(gameObject, 5.0f);
    }

    public void HeadRot(GameObject obj, float speed)
    {
        speed = Time.deltaTime * speed;
        Quaternion rotTo = Quaternion.LookRotation(obj.transform.position - _camera.transform.position);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotTo, speed);
    }
}
