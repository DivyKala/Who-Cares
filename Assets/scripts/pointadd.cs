using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointadd : MonoBehaviour
{
    public Text points;
    public Animator pointanim;
    public void pointss(int a)
    {
        points.color = Color.red;
        points.text = "+"+ a;
        pointanim.SetTrigger("play");
    }
    public void pointsn(int a)
    {
        points.color = Color.green;
        points.text = "-" + a;
        pointanim.SetTrigger("play");
    }
    public void Reseet()
    {
        points.text = " ";
    }
}
