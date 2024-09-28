using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ToturialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogue;
    [SerializeField]
    private TextMeshProUGUI dialougeText;
    [SerializeField]
    private TutorialDialogueData tutorialDialogueData;
    [SerializeField]
    private Collider playerCollider;
    [SerializeField]
    private float nextSpeed = 5;

    public int tutorialIndex = 0;
    private bool isTutorialStart = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (isTutorialStart) return;

        switch (tutorialIndex)
        {
            case 0:
                StartCoroutine(Introduction());
                break;
            case 3:
                StartCoroutine(LearnMove());
                break;
            case 4:
                StartCoroutine(LearnJump());
                break;
            case 5:
                StartCoroutine(LearnSlide());
                break;
            case 6:
                StartCoroutine(LearnSit());
                break;
            case 7:
                StartCoroutine(LearnColorOut());
                break;
            case 10:
                StartCoroutine(LearnCave());
                break;
            case 12:
                StartCoroutine(LearnColorIn());
                break;
            case 15:
                StartCoroutine(LearnVillage());
                break;
            case 19:
                StartCoroutine(TutorialEnd());
                break;
        }
    }

    private void NextTutorial()
    {
        tutorialIndex++;
    }

    private void PrintText()
    {
        dialogue.SetActive(true);
        dialougeText.text = tutorialDialogueData.data[tutorialIndex];
    }

    IEnumerator Introduction()
    {
        isTutorialStart = true;

        while (tutorialIndex < 3)
        {
            PrintText();
            NextTutorial();
            yield return new WaitForSeconds(nextSpeed);

            dialogue.SetActive(false);

            yield return new WaitForSeconds(nextSpeed);
        }

        isTutorialStart = false;
    }

    IEnumerator LearnMove()
    {
        isTutorialStart = true;

        PrintText();
        yield return new WaitForSeconds(nextSpeed);

        dialogue.SetActive(false);

        yield return new WaitUntil(() =>
        {
            return MoveCheck();
        });

        NextTutorial();
        isTutorialStart = false;
    }

    private bool MoveCheck()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }

        return false;
    }

    IEnumerator LearnJump()
    {
        isTutorialStart = true;

        PrintText();
        yield return new WaitForSeconds(nextSpeed);

        dialogue.SetActive(false);

        yield return new WaitUntil(() =>
        {
            return JumpCheck();
        });

        NextTutorial();
        isTutorialStart = false;
    }

    private bool JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        return false;
    }

    IEnumerator LearnSlide()
    {
        isTutorialStart = true;

        PrintText();
        yield return new WaitForSeconds(nextSpeed);

        dialogue.SetActive(false);

        yield return new WaitUntil(() =>
        {
            return SildeCheck();
        });

        NextTutorial();
        isTutorialStart = false;
    }

    private bool SildeCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return true;
        }

        return false;

    }

    IEnumerator LearnSit()
    {
        isTutorialStart = true;

        PrintText();
        yield return new WaitForSeconds(nextSpeed);

        dialogue.SetActive(false);

        yield return new WaitUntil(() =>
        {
            return SitCheck();
        });

        NextTutorial();
        isTutorialStart = false;
    }

    private bool SitCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            return true;
        }

        return false;
    }

    IEnumerator LearnColorOut()
    {
        isTutorialStart = true;

        for (int i = 0; i < 2; i++)
        {
            PrintText();
            NextTutorial();
            yield return new WaitForSeconds(nextSpeed);

            dialogue.SetActive(false);

            yield return new WaitForSeconds(nextSpeed);
        }

        PrintText();
        yield return new WaitForSeconds(nextSpeed);

        dialogue.SetActive(false);

        yield return new WaitUntil(() =>
        {
            return ColorOutCheck();
        });

        NextTutorial();
        isTutorialStart = false;
    }

    private bool ColorOutCheck()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            return true;
        }

        return false;
    }

    IEnumerator LearnCave()
    {
        isTutorialStart = true;

        for (int i = 0; i < 2; i++)
        {
            PrintText();
            NextTutorial();
            yield return new WaitForSeconds(nextSpeed);

            dialogue.SetActive(false);

            yield return new WaitForSeconds(nextSpeed);
        }

        yield return new WaitUntil(() =>
        {
            return CaveCheck();
        });

        isTutorialStart = false;
    }

    private bool CaveCheck()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "cave")
        {
            return true;
        }

        return false;
    }

    IEnumerator LearnColorIn()
    {
        isTutorialStart = true;

        for (int i = 0; i < 3; i++)
        {
            PrintText();
            NextTutorial();
            yield return new WaitForSeconds(nextSpeed);

            dialogue.SetActive(false);

            yield return new WaitForSeconds(nextSpeed);
        }

        yield return new WaitUntil(() =>
        {
            return ColorInCheck();
        });

        isTutorialStart = false;
    }

    private bool ColorInCheck()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }

        return false;
    }

    IEnumerator LearnVillage()
    {
        isTutorialStart = true;

        for (int i = 0; i < 4; i++)
        {
            PrintText();
            NextTutorial();
            yield return new WaitForSeconds(nextSpeed);

            dialogue.SetActive(false);

            yield return new WaitForSeconds(nextSpeed);
        }

        yield return new WaitUntil(() =>
        {
            return VillageCheck();
        });

        Debug.Log("Village");
        isTutorialStart = false;
    }

    private bool VillageCheck()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "village")
        {
            return true;
        }

        return false;
    }

    IEnumerator TutorialEnd()
    {
        isTutorialStart = true;

        PrintText();

        yield return new WaitForSeconds(nextSpeed);

        dialogue.SetActive(false );
    }
}