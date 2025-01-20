using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SliderController : MonoBehaviour
{
    public Transform[] mainScreens;
    public Transform leftSpawnpoint;
    public Transform rightSpawnpoint;

    public float rightOffset;

    float leftStartingPoint = 20f;
    float rightEndingPoint = 700f;

    float rightStartingPoint = 580f;
    float leftEndingPoint = -100f;

    bool canScroll;

    private void Start()
    {
        canScroll = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && canScroll)
        {
            InteractionHandler.instance.HandleSelection();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
                SceneManager.LoadScene(0);
            else if (SceneManager.GetActiveScene().buildIndex == 2)
                SceneManager.LoadScene(0);
            else if (SceneManager.GetActiveScene().buildIndex == 3)
                SceneManager.LoadScene(0);
        }
    }

    #region previous
    public void ScreenMovementPrevious()
    {
        if (canScroll)
        {
            canScroll = false;

            float pos0 = mainScreens[0].position.z;
            float pos1 = mainScreens[1].position.z;
            float pos2 = mainScreens[2].position.z;

            mainScreens[0].DOMoveZ(pos0 + rightOffset, 2f);
            mainScreens[1].DOMoveZ(pos1 + rightOffset, 2f);
            mainScreens[2].DOMoveZ(rightEndingPoint, 1f).onComplete = FinishScrollingPrevious;
        }
    }

    void FinishScrollingPrevious()
    {
        mainScreens[2].DOMoveZ(leftSpawnpoint.position.z, 0f);
        mainScreens[2].DOMoveZ(leftStartingPoint, 1f).onComplete = ResetScrollingPrevious;
    }

    void ResetScrollingPrevious()
    {
        Transform temp1 = mainScreens[0];
        mainScreens[0] = mainScreens[2];
        Transform temp2 = mainScreens[1];
        mainScreens[1] = temp1;
        mainScreens[2] = temp2; ;

        SetCurrentSelection();
        canScroll = true;
    }

    void SetCurrentSelection()
    {
        if (mainScreens[1].CompareTag("Experience"))
        {
            InteractionHandler.instance.currentSelection = 1;
        }
        else if (mainScreens[1].CompareTag("Studio"))
        {
            InteractionHandler.instance.currentSelection = 0;
        }
        else if (mainScreens[1].CompareTag("Software"))
        {
            InteractionHandler.instance.currentSelection = 2;
        }

        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            InteractionHandler.instance.StopExperienceVideo();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            InteractionHandler.instance.StopStudioVideo();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            InteractionHandler.instance.StopSoftwareVideo();
        }

        Debug.Log(InteractionHandler.instance.currentSelection);
    }

    #endregion

    #region next

    public void ScreenMovementNext()
    {
        if (canScroll)
        {
            canScroll = false;

            float pos0 = mainScreens[0].position.z;
            float pos1 = mainScreens[1].position.z;
            float pos2 = mainScreens[2].position.z;

            mainScreens[1].DOMoveZ(pos1 - rightOffset, 2f);
            mainScreens[2].DOMoveZ(pos2 - rightOffset, 2f);
            mainScreens[0].DOMoveZ(leftEndingPoint, 1f).onComplete = FinishScrollingNext;
        }
    }

    void FinishScrollingNext()
    {
        mainScreens[0].DOMoveZ(rightSpawnpoint.position.z, 0f);
        mainScreens[0].DOMoveZ(rightStartingPoint, 1f).onComplete = ResetScrollingNext;
    }

    void ResetScrollingNext()
    {
        Transform temp1 = mainScreens[0];
        Transform temp2 = mainScreens[1];
        Transform temp3 = mainScreens[2];
        mainScreens[2] = temp1;
        mainScreens[1] = temp3;
        mainScreens[0] = temp2;

        SetCurrentSelection();
        canScroll = true;
    }

    #endregion
}
