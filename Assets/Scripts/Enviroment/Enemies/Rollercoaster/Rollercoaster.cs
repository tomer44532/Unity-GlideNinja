using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollercoaster : ColoredObstacle
{
    [SerializeField]
    private GameObject cartGameObject;

    [SerializeField]
    private GameObject pathGameObject;

    private RollercoasterPath rollercoasterPath;

    public GameObject CartGameObject { get => cartGameObject; set => cartGameObject = value; }
    public RollercoasterPath RollercoasterPath { get => rollercoasterPath; set => rollercoasterPath = value; }

    // Start is called before the first frame update
    void Start()
    {
        RollercoasterPath = new RollercoasterPath(this,pathGameObject);
        RollercoasterPath.SetPath();
        cartGameObject.GetComponent<RollercoasterCart>().InitializeCart();
    }

    // Update is called once per frame
    void Update()
    {
        cartGameObject.GetComponent<RollercoasterCart>().Move();
    }




    public override void enable()
    {
        if (gameObject.activeInHierarchy)
        {

            base.enable();
            cartGameObject.SetActive(true);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            rollercoasterPath.MovingCurSpot = 0;
            rollercoasterPath.MovingNextSpot = 1;
            cartGameObject.transform.position = rollercoasterPath.GetFirstSpot();
            rollercoasterPath.CheckNextSpot();
            cartGameObject.GetComponent<RollercoasterCart>().CurMovementSpeed = cartGameObject.GetComponent<RollercoasterCart>().DefaultMovementSpeed;
            CartGameObject.transform.localRotation = Quaternion.Euler(0, 0, cartGameObject.GetComponent<RollercoasterCart>().DefaultZRotation);
            Debug.Log("initialize cart "+ cartGameObject.GetComponent<RollercoasterCart>().DefaultZRotation);

            //cartGameObject.GetComponent<RollercoasterCart>().InitializeCart();

        }

    }

    public override void disable()
    {
        base.disable();
        cartGameObject.SetActive(false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);


    }
}
