using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentProjectileHolder : BaseProjectile {

    [Header("Spread Settings ")]
    [SerializeField]
    BaseProjectile Projectile;
    protected int NumProjectiles = 1;
    [SerializeField]
    protected SpreadTypeEnum SpreadType;
    [Tooltip("Maximum perpendicular distance that the projectiles will be set.")]
    protected float SpreadDistance = 2f;
    [Tooltip("Maximum spread that the projectiles will take in reference to the firing direction of the character.")]
    protected float SpreadDegrees = 45;
    bool CanChain = false;

    [Header("Child Objects and Settings")]
    protected List<BaseProjectile> ChildProjectiles = new List<BaseProjectile>();

    // Use this for initialization
    void Start () {
        CreateChildren();
        InitChildren();
        CheckSpreadTypeAndActivate();
    }
	
	// Update is called once per frame
	void Update () {
        CheckChildrenToDestroyParent();
	}

    /// <summary>
    /// Used to pass the caster and ability down to all of the projectile children, if any
    /// </summary>
    protected void InitChildren()
    {
        foreach (BaseProjectile p in ChildProjectiles)
        {
            p.Init(AbilityThatCastMe, FiringEntity);
        }
    }

    protected void CreateChildren()
    {
        for (int i = 0; i < NumProjectiles; i++)
        {
            BaseProjectile newGo = Instantiate(Projectile, transform, false);
            ChildProjectiles.Add(newGo);
        }
    }

    protected void CheckChildrenToDestroyParent()
    {
        if (ChildProjectiles.Count > 0)
        {
            int i = 0;
            foreach (BaseProjectile p in ChildProjectiles)
            {
                if (p == null)
                    i++;
            }
            if (i == ChildProjectiles.Count)
                Destroy(gameObject);
        }
    }

    protected void CheckSpreadTypeAndActivate()
    {
        switch (SpreadType)
        {
            case SpreadTypeEnum.CONE:
                SpreadChildrenAngled();
                break;
            case SpreadTypeEnum.SQUARE:
                SpreadChildenSquare();
                break;
            case SpreadTypeEnum.THREE_SIXTY:
                SpreadChildrenAngled();
                break;
        }
    }

    void SpreadChildrenAngled()
    {
        int count = ChildProjectiles.Count;

        if (count > 1)
        {
            float anglePerObject = 0;
            if (SpreadType == SpreadTypeEnum.THREE_SIXTY)
            {
                anglePerObject = 360 / count;
            }
            else
                anglePerObject = SpreadDegrees / (count - 1);

            Vector3 startingPoint = new Vector3(0, -1 * SpreadDegrees / 2, 0);
            for (int i = 0; i < count; i++)
            {
                ChildProjectiles[i].transform.localRotation = Quaternion.Euler(startingPoint + new Vector3(0, i * anglePerObject, 0));
                ChildProjectiles[i].gameObject.SetActive(true);
            }
        }
    }

    void SpreadChildenSquare()
    {
        int count = ChildProjectiles.Count;

        if (count > 1)
        {
            float distancePerObject = SpreadDistance / (count - 1);

            Vector3 startingPoint = new Vector3(-1 * SpreadDistance / 2, 0, 0);
            for (int i = 0; i < count; i++)
            {
                ChildProjectiles[i].transform.localPosition = startingPoint + new Vector3(i * distancePerObject, 0, 0);
                ChildProjectiles[i].gameObject.SetActive(true);
            }
        }
    }

    public SpreadTypeEnum GetSpreadType()
    {
        return SpreadType;
    }

    public void SetProjectileType(SpreadTypeEnum spreadType)
    {
        SpreadType = spreadType;
        switch (spreadType)
        {
            case SpreadTypeEnum.CONE:
                NumProjectiles = 4;
                SpreadDegrees = (float)NumProjectiles * 10.125f;
                break;
            case SpreadTypeEnum.SQUARE:
                NumProjectiles = 4;
                SpreadDistance = NumProjectiles * .5f;
                break;
            case SpreadTypeEnum.THREE_SIXTY:
                NumProjectiles = 8;
                break;
        }
    }

    public void SetCanChain()
    {
        CanChain = true;
    }
}
