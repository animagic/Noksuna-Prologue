using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCooldown : ExtendedMonoBehaviour {

    [SerializeField]
    Image BaseImage;
    [SerializeField]
    Image FillImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEffect(BaseStatusEffect effect)
    {
        BaseImage.sprite = effect.Icon;
        BaseImage.color = ColorWithAlpha(BaseImage.color, 1);

        FillImage.sprite = effect.Icon;
        FillImage.color = ColorWithAlpha(FillImage.color, 1);
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
}
