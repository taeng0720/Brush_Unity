using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public GameObject player;
    public GameObject tutorialText;
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public GameObject cave;
    public Transform targetObject;
    public float moveSpeed = 5f;
    public float messageDisplayTime = 3f;
    public float characterDelay = 0.1f;

    private bool tutorialActive = true;
    private bool isCave;
    public bool isJump;
    public bool isSliding;
    public bool isDash;
    public bool isAlt;
    public bool isGetColor;
    public bool isApplyColor;
    public bool isArrive;

    private bool skipMessage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "cave")
        {
            isCave = true;
        }
        if (!tutorialActive)
        {
            if (SceneManager.GetActiveScene().name == "village")
            {
                DeactivateObjects();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipMessage = true;
        }
    }

    private IEnumerator TutorialSequence()
    {
        DisablePlayerScripts();
        ActivateObjects();

        yield return DisplayMessage("어서와~ 기다리고 있었어");
        yield return DisplayMessage("나는 우우리라고 해");
        yield return DisplayMessage("나랑 함께 여기 있는 색을 가져와서 동굴에다 집어넣어보자!");

        EnablePlayerScripts();

        yield return DisplayMessage("Q를 눌러봐! 대쉬가 나올거야~");
        yield return WaitForInputCondition(() => isDash);

        yield return DisplayMessage("그거 말고도 많아 Space 점프라던가~");
        yield return WaitForInputCondition(() => isJump);

        yield return DisplayMessage("LeftCtrl 슬라이딩이라던가~");
        yield return WaitForInputCondition(() => isSliding);

        yield return DisplayMessage("LeftAlt 앉기같은거!");
        yield return WaitForInputCondition(() => isAlt);

        yield return DisplayMessage("그럼 저기 저 신기한 색이 있는 돌로 가보자!");

        yield return DisplayMessage("얘는 R키를 누를 경우 색을 흡수할 수 있어!");
        yield return WaitForInputCondition(() => isGetColor);

        yield return DisplayMessage("저 뒤에 있는 동굴로 이동을 해보자!");
        CaveDeactivateObjects();
        DeactivateObjects();
        yield return WaitForInputCondition(() => isCave);

        yield return DisplayMessage("참 색을 넣으려면 F키를 누르면 색이 넣어져!");
        yield return WaitForInputCondition(() => isApplyColor);
        yield return DisplayMessage("이제 나랑 보는 것도 마지막이겠다ㅜㅜ");
        yield return DisplayMessage("여기 모든 동굴 벽, 천장들에다가 전부 색을 집어넣을 수 있어");
        yield return DisplayMessage("참 색을 넣으려면 F키를 누르면 색이 넣어져!");
        yield return DisplayMessage("이제 시간이 진짜 별로 남지 않았는걸...");
        yield return DisplayMessage("너가 해야할 목표는 하나뿐이야!");
        yield return DisplayMessage("돌마을에 있는 모든 색들을 가져와서");
        yield return DisplayMessage("이 동굴에 전부 채워줘!");
        tutorialActive = false;
    }

    private IEnumerator DisplayMessage(string message)
    {
        tutorialText.SetActive(true);
        tutorialText.GetComponent<TextMeshPro>().text = "";
        skipMessage = false;

        foreach (char character in message)
        {
            if (skipMessage)
            {
                tutorialText.GetComponent<TextMeshPro>().text = message;
                break;
            }

            tutorialText.GetComponent<TextMeshPro>().text += character;
            yield return new WaitForSeconds(characterDelay);
        }

        yield return new WaitForSeconds(messageDisplayTime);
        tutorialText.SetActive(false);
    }

    private IEnumerator WaitForInputCondition(System.Func<bool> condition)
    {
        tutorialText.SetActive(true);

        while (!condition())
        {
            yield return null;
        }

        tutorialText.SetActive(false);
    }

    private void DisablePlayerScripts()
    {
        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
    }

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    private void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            StartCoroutine(FadeOut(obj));
        }
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            float fadeDuration = 0.5f;
            float fadeSpeed = 1f / fadeDuration;
            float fadeAmount = 1f;

            while (fadeAmount > 0)
            {
                fadeAmount -= fadeSpeed * Time.deltaTime;
                renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmount);
                yield return null;
            }

            obj.SetActive(false);
        }
    }


    private void CaveDeactivateObjects()
    {
        cave.SetActive(false);
    }

    private void EnablePlayerScripts()
    {
        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = true;
        }
    }
}
