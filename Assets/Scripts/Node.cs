using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Node : MonoBehaviour
{
    public ShapeType.Shape shape;
    public Sprite cat_IMG;
    public Sprite rabit_IMG;
    public BoardManager boardManager;
    public bool isPicked = false;
    public Image nodeImage;
    public int nodeX;
    public int nodeY;
    AudioSource audioSource;
    public AudioClip[] clips;

    private void Start()
    {
        shape = ShapeType.Shape.None;
        boardManager = FindObjectOfType<BoardManager>();
        transform.localScale = Vector2.zero;
        audioSource = GetComponent<AudioSource>();
    }

    public void BirthTween()
    {
        PlaySound(clips[0]);
        transform.DOScale(1f, 1f);
    }
    void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void NodeSelected()
    {
        if (isPicked)
        {
            ErrorTween();
            return;
        }

        PlaySound(clips[0]);
        isPicked = true;
        shape = boardManager.currentShape;
        NodeTextChange();
        boardManager.Calculate();
        ClickTween();
    }

    void ErrorTween()
    {
        GetComponent<Image>().DOColor(new Color32(255, 150, 150, 255), 0.3f)
    .OnComplete(() => GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 0.3f));

        GetComponent<RectTransform>().DOShakePosition(0.5f, 10, 100, 90, false, true, ShakeRandomnessMode.Harmonic);
        PlaySound(clips[1]);
    }
    void ClickTween()
    {
        transform.DOScale(0.8f, 0.1f).OnComplete(()=> transform.DOScale(1f, 0.1f));
    }

    void NodeTextChange()
    {
        if (shape == ShapeType.Shape.Cat)
            nodeImage.sprite = cat_IMG;
        //nodeText.text = "O";

        if (shape == ShapeType.Shape.Rabit)
            nodeImage.sprite = rabit_IMG;
            //nodeText.text = "X";
    }
}
