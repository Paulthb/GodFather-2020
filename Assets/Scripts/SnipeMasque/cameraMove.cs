using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    private Player player;

    private Transform cameraTransform = null;

    //for next juicy feature if needed
    private bool onValideTarget = false;

    private GameObject actualTarget = null;

    private GameObject[] persoNMObjectTab;
    [SerializeField]
    private float cameraSpeed = 2;

    [SerializeField]
    private Sprite Perso1M_Sprite = null;
    [SerializeField]
    private Sprite Perso2M_Sprite = null;

    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        cameraTransform = GetComponent<Transform>();
        persoNMObjectTab = GameObject.FindGameObjectsWithTag("NMPerso");
    }

    void Update()
    {
        float moveHorizontal = player.GetAxis("X_Axis");
        float moveVertical = player.GetAxis("Y_Axis");

        if (moveHorizontal != 0)
            cameraTransform.Translate(Vector3.right * Time.deltaTime * moveHorizontal * cameraSpeed);

        if (moveVertical != 0)
            cameraTransform.Translate(Vector3.up * Time.deltaTime * moveVertical * cameraSpeed);

        if (player.GetButtonDown("ActionButton") && onValideTarget)
        {
            Debug.Log("shooted ?");
            ShootMask();
        }

        if (persoNMObjectTab.Length == 0)
            GameManager.Instance.LaunchTransition();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NMPerso")
        {
            onValideTarget = true;
            Debug.Log("target validate");

            actualTarget = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NMPerso")
            onValideTarget = false;
    }

    public void ShootMask()
    {
        Sprite oldSprite = actualTarget.GetComponent<SpriteRenderer>().sprite;
        if (actualTarget.tag != "MPerso")
        {
            if (oldSprite.texture.name == "perso1_sm")
                actualTarget.GetComponent<SpriteRenderer>().sprite = Perso1M_Sprite;

            else if (oldSprite.texture.name == "perso2_sm")
                actualTarget.GetComponent<SpriteRenderer>().sprite = Perso2M_Sprite;

            actualTarget.tag = "MPerso";
        }
        persoNMObjectTab = GameObject.FindGameObjectsWithTag("NMPerso");

    }
}