using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarImage : MonoBehaviour
{
    Image powerBar;

    private void Start()
    {
        powerBar = GetComponent<Image>();
    }

    private void Update()
    {
        if (!Ball.Instance.onDrag)
        {
            powerBar.fillAmount = 0;
            return;
        }
        powerBar.fillAmount = Ball.Instance.power / Ball.Instance.maxPower;
    }

}

