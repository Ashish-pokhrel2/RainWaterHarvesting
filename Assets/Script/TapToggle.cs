using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToggle : MonoBehaviour
   {
    public TapControl tapControl;

    void OnMouseDown()
    {
        tapControl.ToggleTap();
    }

}
