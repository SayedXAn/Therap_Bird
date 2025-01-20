using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class InteractionHandler : MonoBehaviour
{
    public static InteractionHandler instance;

    [Header("Screen Element")]
    public GameObject[] Experience1;
    public GameObject[] Experience2;
    public GameObject[] Experience3;
    public GameObject[] Studio1;
    public GameObject[] Studio2;
    public GameObject[] Studio3;
    public GameObject[] Soft1;
    public GameObject[] Soft2;
    public GameObject[] Soft3;
    public VideoClip[] ExperienceClips;
    public VideoClip[] StudioClips;
    public VideoClip[] SoftClips;

    public int currentSelection;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;
    }

    private void Start()
    {
        currentSelection = 1;
    }
    public void HandleSelection()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            OpenScene();

        else if (SceneManager.GetActiveScene().buildIndex == 1)
            PlayStudioVideo();

        else if (SceneManager.GetActiveScene().buildIndex == 2)
            PlayExperienceVideo();

        else if (SceneManager.GetActiveScene().buildIndex == 3)
            PlaySoftwareVideo();
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(currentSelection + 1);
    }

    public void PlayExperienceVideo()
    {
        if(currentSelection == 0)
        {
            Experience1[0].gameObject.SetActive(false);
            Experience1[1].gameObject.SetActive(true);
            Experience2[0].gameObject.SetActive(true);
            Experience2[1].gameObject.SetActive(false);
            Experience3[0].gameObject.SetActive(true);
            Experience3[1].gameObject.SetActive(false);
            StartCoroutine(WaitForVideo((float)ExperienceClips[0].length, Experience1[0].gameObject, 
                Experience1[1].gameObject));
        }

        else if (currentSelection == 1)
        {
            Experience2[0].gameObject.SetActive(false);
            Experience2[1].gameObject.SetActive(true);
            Experience1[0].gameObject.SetActive(true);
            Experience1[1].gameObject.SetActive(false);
            Experience3[0].gameObject.SetActive(true);
            Experience3[1].gameObject.SetActive(false);
            StartCoroutine(WaitForVideo((float)ExperienceClips[1].length, Experience2[0].gameObject,
                Experience2[0].gameObject));
        }

        else if (currentSelection == 2)
        {
            Experience3[0].gameObject.SetActive(false);
            Experience3[1].gameObject.SetActive(true);
            Experience1[0].gameObject.SetActive(true);
            Experience1[1].gameObject.SetActive(false);
            Experience2[0].gameObject.SetActive(true);
            Experience2[1].gameObject.SetActive(false);
            StartCoroutine(WaitForVideo((float)ExperienceClips[2].length, Experience3[0].gameObject,
                Experience3[1].gameObject));
        }
    }

    public void StopExperienceVideo()
    {
        if (currentSelection == 0)
        {
            Experience2[0].gameObject.SetActive(true);
            Experience2[1].gameObject.SetActive(false);
            Experience3[0].gameObject.SetActive(true);
            Experience3[1].gameObject.SetActive(false);
        }

        else if (currentSelection == 1)
        {
            Experience1[0].gameObject.SetActive(true);
            Experience1[1].gameObject.SetActive(false);
            Experience3[0].gameObject.SetActive(true);
            Experience3[1].gameObject.SetActive(false);
        }

        else if (currentSelection == 2)
        {
            Experience1[0].gameObject.SetActive(true);
            Experience1[1].gameObject.SetActive(false);
            Experience2[0].gameObject.SetActive(true);
            Experience2[1].gameObject.SetActive(false);
        }
    }

    public void PlayStudioVideo()
    {
        if (currentSelection == 0)
        {
            Studio1[0].gameObject.SetActive(false);
            Studio1[1].gameObject.SetActive(true);
            StartCoroutine(WaitForVideo((float)StudioClips[0].length, Studio1[0].gameObject,
                Studio1[1].gameObject));
        }

        else if (currentSelection == 1)
        {
            Studio2[0].gameObject.SetActive(false);
            Studio2[1].gameObject.SetActive(true);
            StartCoroutine(WaitForVideo((float)StudioClips[1].length, Studio2[0].gameObject,
                Studio2[0].gameObject));
        }

        else if (currentSelection == 2)
        {
            Studio3[0].gameObject.SetActive(false);
            Studio3[1].gameObject.SetActive(true);
            StartCoroutine(WaitForVideo((float)StudioClips[2].length, Studio3[0].gameObject,
                Studio3[1].gameObject));
        }
    }

    public void StopStudioVideo()
    {
        if (currentSelection == 0)
        {
            Studio2[0].gameObject.SetActive(true);
            Studio2[1].gameObject.SetActive(false);
            Studio3[0].gameObject.SetActive(true);
            Studio3[1].gameObject.SetActive(false);
        }

        else if (currentSelection == 1)
        {
            Studio1[0].gameObject.SetActive(true);
            Studio1[1].gameObject.SetActive(false);
            Studio3[0].gameObject.SetActive(true);
            Studio3[1].gameObject.SetActive(false);
        }

        else if (currentSelection == 2)
        {
            Studio1[0].gameObject.SetActive(true);
            Studio1[1].gameObject.SetActive(false);
            Studio2[0].gameObject.SetActive(true);
            Studio2[1].gameObject.SetActive(false);
        }
    }

    public void PlaySoftwareVideo()
    {
        if (currentSelection == 0)
        {
            Soft1[0].gameObject.SetActive(false);
            Soft1[1].gameObject.SetActive(true);
            StartCoroutine(WaitForVideo((float)SoftClips[0].length, Soft1[0].gameObject,
                Soft1[1].gameObject));
        }

        else if (currentSelection == 1)
        {
            Soft2[0].gameObject.SetActive(false);
            Soft2[1].gameObject.SetActive(true);
            StartCoroutine(WaitForVideo((float)SoftClips[1].length, Soft2[0].gameObject,
                Soft2[0].gameObject));
        }

        else if (currentSelection == 2)
        {
            Soft3[0].gameObject.SetActive(false);
            Soft3[1].gameObject.SetActive(true);
            StartCoroutine(WaitForVideo((float)SoftClips[2].length, Soft3[0].gameObject,
                Soft3[1].gameObject));
        }
    }

    public void StopSoftwareVideo()
    {
        if (currentSelection == 0)
        {
            Soft2[0].gameObject.SetActive(true);
            Soft2[1].gameObject.SetActive(false);
            Soft3[0].gameObject.SetActive(true);
            Soft3[1].gameObject.SetActive(false);
        }

        else if (currentSelection == 1)
        {
            Soft1[0].gameObject.SetActive(true);
            Soft1[1].gameObject.SetActive(false);
            Soft3[0].gameObject.SetActive(true);
            Soft3[1].gameObject.SetActive(false);
        }

        else if (currentSelection == 2)
        {
            Soft1[0].gameObject.SetActive(true);
            Soft1[1].gameObject.SetActive(false);
            Soft2[0].gameObject.SetActive(true);
            Soft2[1].gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForVideo(float duration, GameObject thumb, GameObject video)
    {
        yield return new WaitForSeconds(duration);
        video.SetActive(false);
        thumb.SetActive(true);
    }
}
