using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MedicalData {
    public MedicalData(
        Sprite photo,
        string id,
        string name,
        string dob,
        string age,
        string sex,
        string rh,
        string hight,
        string weight,
        string status,
        string dx,
        string allergy
        ) 
    {
        Photo = photo;
        Id = id;
        Name = name;
        DOB = dob;
        Age = age;
        Sex = sex;
        RH = rh;
        Hight = hight;
        Weight = weight;
        Status = status;
        Dx = dx;
        Allergy = allergy;
    }
    public Sprite Photo { private set; get;}
    public string Id { private set; get; }
    public string Name { private set; get; }
    public string DOB { private set; get; }
    public string Age { private set; get; }
    public string Sex { private set; get; }
    public string RH { private set; get; }
    public string Hight { private set; get; }
    public string Weight { private set; get; }
    public string Status { private set; get; }
    public string Dx { private set; get; }
    public string Allergy { private set; get; }
}

public class MedicalDataController : MonoBehaviour
{
    public GameObject photo;
    public GameObject id;
    public GameObject name;
    public GameObject dob;
    public GameObject age;
    public GameObject sex;
    public GameObject rh;
    public GameObject hight;
    public GameObject weight;
    public GameObject status;
    public GameObject dx;
    public GameObject allergy;

    void Start()
    {
        photo = GameObject.Find("Photo");
        id = GameObject.Find("Id");
        name = GameObject.Find("Name");
        dob = GameObject.Find("DOB");
        age = GameObject.Find("Age");
        sex = GameObject.Find("Sex");
        rh = GameObject.Find("RH");
        hight = GameObject.Find("Hight");
        weight = GameObject.Find("Weight");
        status = GameObject.Find("Status");
        dx = GameObject.Find("Dx");
        allergy = GameObject.Find("Allergy");
    }

    public void SetData(MedicalData data) {
        photo.GetComponent<Image>().sprite = data.Photo;
        id.GetComponent<Text>().text = Format(id.GetComponent<Text>().text, data.Id);
        name.GetComponent<Text>().text = Format(name.GetComponent<Text>().text,data.Name);
        dob.GetComponent<Text>().text = Format(dob.GetComponent<Text>().text, data.DOB);
        age.GetComponent<Text>().text = Format(age.GetComponent<Text>().text, data.Age);
        sex.GetComponent<Text>().text = Format(sex.GetComponent<Text>().text, data.Sex);
        rh.GetComponent<Text>().text = Format(rh.GetComponent<Text>().text, data.RH);
        hight.GetComponent<Text>().text = Format(hight.GetComponent<Text>().text, data.Hight);
        weight.GetComponent<Text>().text = Format(weight.GetComponent<Text>().text, data.Weight);
        status.GetComponent<Text>().text = Format(status.GetComponent<Text>().text, data.Status);
        dx.GetComponent<Text>().text = Format(dx.GetComponent<Text>().text, data.Dx);
        allergy.GetComponent<Text>().text = Format(allergy.GetComponent<Text>().text, data.Allergy);
    }

    private string Format(string placeHolder, string input) {
        char[] spearator = { ':' };
        string[] strlist = placeHolder.Split(spearator);
        string name = strlist[0];
        return name + ": " + input;
    }

    void Update()
    {
        
    }
}
