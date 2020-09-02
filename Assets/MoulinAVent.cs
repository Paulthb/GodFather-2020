using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
public class MoulinAVent : MonoBehaviour
{
    private Player player;
    
    [Header("Gameplay Settings")]
    [Tooltip("Valeur pour régler la force de diminution")]
    public float decayStrength = 6.0f;
    [Tooltip("Valeur pour régler la force d'augmentation")]
    public float spammingStrength = 0.33f;
    [Tooltip("Durée maximale du mini-jeu")]
    public float mainTimer = 20.0f;
    private float mainClock = 0.0f;
    [Tooltip("Durée pendant laquelle il faut maintenir la force entre les deux bornes")]
    public float subTimer = 0.0f;

    [Tooltip("Valeur Minimale Désirée")]
    public float minDesiredStrength = 10.0f;
    [Tooltip("Valeur Maximale Désirée")]
    public float maxDesiredStrength = 12.0f;

    private float _subTimerValue = 0.0f;
    
    public float maxRotationStrength = 15.0f;
    private bool end = false;

    private DIRECTION direction = DIRECTION.DEFAULT;
    private float rotationStrength = 0.0f;

    [Header("UI")]
    public Image strengthFillImage;
    public Image timerFillImage;
    public Text subTimerText;

    [Header("Visuel")]
    public SpriteRenderer mrSimple;
    public SpriteRenderer kid;
    public SpriteRenderer wind;
    public GameObject turbine;
    public List<Sprite> mrSimpleSprites = new List<Sprite>(3);
    public List<Sprite> kidSprites = new List<Sprite>(3);
    public List<Sprite> windSprites = new List<Sprite>(3);
    public float rotationSpeed;

    private void Start()
    {
        _subTimerValue = subTimer;
        player = ReInput.players.GetPlayer(0);
    }
    public enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        DEFAULT
    }

    
    void Update()
    {
        if(!end)
        {
            mainClock += Time.deltaTime;
            if (mainClock >= mainTimer)
            {
                //Défaite
                end = true;
            }
            else
            {
                ManageDirectionInputs();
                rotationStrength = Mathf.Clamp(rotationStrength, 0, maxRotationStrength);

                if (rotationStrength > minDesiredStrength && rotationStrength < maxDesiredStrength)
                {
                    subTimerText.enabled = true;
                    subTimer -= Time.deltaTime;
                    if (subTimer <= 0)
                    {
                        //Victoire
                        end = true;
                    }
                }
                else
                {
                    subTimerText.enabled = false;
                    subTimer = _subTimerValue;
                }
                UpdateUI();
                UpdateVisuals();
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (rotationStrength > 0)
            rotationStrength -= decayStrength * Time.deltaTime;
    }

    public void ManageDirectionInputs()
    {
        switch (direction)
        {
            case DIRECTION.UP:
                if (player.GetAxis("X_Axis") < -0.1f)
                {
                    direction = DIRECTION.LEFT;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.RIGHT:
                if (player.GetAxis("Y_Axis") > 0.1f)
                {
                    direction = DIRECTION.UP;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.DOWN:
                if (player.GetAxis("X_Axis") > 0.1f)
                {
                    direction = DIRECTION.RIGHT;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.LEFT:
                if (player.GetAxis("Y_Axis") < -0.1f)
                {
                    direction = DIRECTION.DOWN;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.DEFAULT:
                if (player.GetAxis("Y_Axis") > 0.1f)
                    direction = DIRECTION.UP;
                else if (player.GetAxis("X_Axis") < -0.1f)
                    direction = DIRECTION.LEFT;
                else if (player.GetAxis("Y_Axis") < -0.1f)
                    direction = DIRECTION.DOWN;
                else if (player.GetAxis("X_Axis") > 0.1f)
                    direction = DIRECTION.RIGHT;
                break;
        }
    }

    public void UpdateUI()
    {
        strengthFillImage.fillAmount = rotationStrength / maxRotationStrength;
        timerFillImage.fillAmount = mainClock / mainTimer;
        subTimerText.text = ((int)(subTimer)).ToString();
    }

    public void UpdateVisuals()
    {
        turbine.transform.Rotate(Vector3.forward * rotationStrength * Time.deltaTime * rotationSpeed);
        if(rotationStrength <= 0)
            wind.sprite = null;

        if (rotationStrength > 0 && rotationStrength < maxRotationStrength/3)
        {
            mrSimple.sprite = mrSimpleSprites[0];
            kid.sprite = kidSprites[0];
            wind.sprite = windSprites[0];
        }
        else if(rotationStrength > maxRotationStrength / 3 && rotationStrength <  2*maxRotationStrength / 3)
        {
            mrSimple.sprite = mrSimpleSprites[1];
            kid.sprite = kidSprites[1];
            wind.sprite = windSprites[1];

        }
        else if(rotationStrength > 2 * maxRotationStrength / 3)
        {
            mrSimple.sprite = mrSimpleSprites[2];
            kid.sprite = kidSprites[2];
            wind.sprite = windSprites[2];
        }
    }
}
