using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LilMenu : MonoBehaviour
{
    public CamMover camMover;
    public Rubix rubix;

    public GameObject panel;
    public void AnimateFaceRotationToggle(Toggle toggle)
    {
        rubix.rubixMover.animateFaceRotation = toggle.isOn;
    }

    public void AnimationSpeed(Slider slider)
    {
        rubix.rubixMover.animationSpeed = slider.value * slider.value;
    }

    public void HideShowPanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void CamDistance(Slider slider)
    {
        camMover.distance = (slider.value * 40) + 10;
    }

    public void SwipeSensitivity(Slider slider)
    {
        camMover.rotationSpeed = (slider.value * 100) + 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            HideShowPanel();
        }
    }

    public void Minimise()
    {
        HideShowPanel();
    }
}
