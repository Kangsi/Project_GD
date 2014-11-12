using UnityEngine;
using System.Collections;
using Manager;

public class ActionbarButton : MonoBehaviour {

    public float x, y;
    public Texture2D buttonImage;
    public GUISkin skin;

    private float parentX, parentY;
    private float uiScale;
    private Rect buttonPosition;

    void Start()
    {
        uiScale = UIManager.uiScale;
        InitializeAnchor();
    }
    void OnGUI()
    {
        GUI.depth = -1;
        InitializeAnchor();
        GUI.skin = skin;
        GUI.Button(buttonPosition, "");
    }

    void InitializeAnchor()
    {
        parentX = GetComponentInParent<Actionbar>().actionbarAnchor.x;
        parentY = GetComponentInParent<Actionbar>().actionbarAnchor.y;

        buttonPosition = new Rect(parentX + (x * uiScale), parentY + (y * uiScale), buttonImage.width * uiScale, buttonImage.height * uiScale);
    }
}
