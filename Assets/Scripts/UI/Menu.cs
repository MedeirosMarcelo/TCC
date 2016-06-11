using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image cursor;
    public float cursorSpeed = 1f;
    public IList<Text> options = new List<Text>();
    protected int cursorIndex = 0;
    protected AudioSource audioSource;

    private float yAxis;
    private float previousYAxis;

    public virtual void Start()
    {
        if (this.transform.parent.parent.name == "Main Menu")
            audioSource = transform.parent.parent.GetComponent<AudioSource>();
        else
            audioSource = transform.parent.parent.parent.GetComponent<AudioSource>();

        cursorIndex = 0;
        LoadMenu();
    }

    public virtual void Update()
    {
        UpdateAxis();
        ControlCursor();
        MoveCursor();
    }

    void UpdateAxis()
    {
        previousYAxis = yAxis;
        yAxis = Input.GetAxis("VerticalJoy");
    }

    bool PressedDown()
    {
        return previousYAxis >= 0 && yAxis < 0;
    }

    bool PressedUp()
    {
        return previousYAxis <= 0 && yAxis > 0;
    }

    void LoadMenu()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Cursor")
            {
                cursor = child.GetComponent<Image>();
            }
            else if (child.tag == "Menu Option")
            {
                options.Add(child.GetComponent<Text>());
            }
        }
    }

    void ControlCursor()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || PressedDown())
        {
            DeselectOption(options[cursorIndex]);
            cursorIndex++;
            AudioManager.Play(ClipType.GUICursor, audioSource);
            if (cursorIndex > options.Count - 1)
            {
                cursorIndex = 0;
            }
            SelectOption(options[cursorIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || PressedUp())
        {
            AudioManager.Play(ClipType.GUICursor, audioSource);
            DeselectOption(options[cursorIndex]);
            cursorIndex--;
            if (cursorIndex < 0)
            {
                cursorIndex = options.Count - 1;
            }
            SelectOption(options[cursorIndex]);
        }
    }

    void SelectOption(Text option)
    {
        options[cursorIndex].color = Color.white;
    }

    void DeselectOption(Text option)
    {
        options[cursorIndex].color = Color.black;
    }

    void MoveCursor()
    {
        if (cursor != null)
            cursor.rectTransform.anchoredPosition = Vector2.Lerp(cursor.rectTransform.anchoredPosition, options[cursorIndex].rectTransform.anchoredPosition, 0.1f * cursorSpeed);
    }
}
