using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IconCooldown : ExtendedMonoBehaviour {

    [SerializeField]
    Image BaseImage;
    [SerializeField]
    Image FillImage;
    [SerializeField]
    Button MyButton;

    float timeToRun;
    float currentTimer;

    bool isRunning = false;

	// Use this for initialization
	void Start () {
        GetImageComponents();


        MyButton = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning)
        {
            RunCooldown(); 
        }
	}

    void GetImageComponents()
    {
        BaseImage = GetComponent<Image>();
        List<Image> childImages = GetComponentsInChildren<Image>().ToList();
        FillImage = childImages[1];
        RectTransform fillRect = FillImage.GetComponent<RectTransform>();
        fillRect.sizeDelta = new Vector2(BaseImage.GetComponent<RectTransform>().sizeDelta.x, BaseImage.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void SetEffect(BaseStatusEffect effect)
    {
        BaseImage.sprite = effect.Icon;
        BaseImage.color = ColorWithAlpha(BaseImage.color, 1);

        FillImage.sprite = effect.Icon;
        FillImage.color = ColorWithAlpha(FillImage.color, 1);

        timeToRun = effect.InitialDuration;
        isRunning = true;
    }

    public void SetAbility(BaseAbility ability)
    {
        if (BaseImage == null || FillImage == null)
            GetImageComponents();
        BaseImage.sprite = ability.Icon;
        //BaseImage.color = ColorWithAlpha(Color.white, 1);

        FillImage.sprite = ability.Icon;
        //FillImage.color = ColorWithAlpha(Color.white, 1);
    }

    public void ClearAbility()
    {
        BaseImage.sprite = null;
        //BaseImage.color = ColorWithAlpha(BaseImage.color, 1);

        FillImage.sprite = null;
        //FillImage.color = ColorWithAlpha(BaseImage.color, 1);
    }
    public void ClearEffect()
    {
        BaseImage.sprite = null;
        BaseImage.color = ColorWithAlpha(BaseImage.color, 0);
        FillImage.sprite = null;
        FillImage.color = ColorWithAlpha(FillImage.color, 0);

    }

    public void SetFillValue(float CurrentByInitial)
    {
        FillImage.fillAmount = CurrentByInitial;
    }

    void RunCooldown()
    {
        currentTimer += Time.deltaTime;
        SetFillValue(currentTimer / timeToRun);

        if (currentTimer >= timeToRun)
            FinishCooldown();
    }

    public void RunAbilityCooldown(float _timeToRun)
    {
        isRunning = true;
        currentTimer = 0.0f;
        timeToRun = _timeToRun;
        Debug.Log(name);
        MyButton.interactable = false;
    }

    void FinishCooldown()
    {
        isRunning = false;
        if (MyButton)
        {
            MyButton.interactable = true; 
        }
    }
}
