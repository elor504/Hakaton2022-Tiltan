using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using RDG;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager GetInstance => _instance;

    [SerializeField]ParticleSystem tap;
    Vector2 fingerPos;

    [Header("Points")]
    public int Points;


    [Header("Hero Boost Timer")]
    public bool IsHeroesBoosted;
    public float BoostTimer;
    float counter;

    [Header("Hero Boost Settings")]
    public float MovementPercentage = 0.1f;
    public float ResistancePercentage = 0.1f;




    private void Awake()
	{
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(this.gameObject);
        UIScore.GetInstance.SetTextScore(Points);
    }


	void Update()
    {
        if (IsHeroesBoosted)
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                IsHeroesBoosted = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && UIManager.getInstance.canVibrate == true)
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    return;
                }
            }

            // vibrating for 10 milliseconds with an amplitude of 50
            Vibration.Vibrate(10, 50);
        }
        if(Input.GetMouseButtonDown(0))
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    return;
                }
            }

            TapEffect();
            if (!IsHeroesBoosted)
            {
                IsHeroesBoosted = true;
                counter = BoostTimer;
            }
        }
    }

    
    public void AddPoints(int amount,Vector2 worldPos)
	{
        PointsPopUpManager.GetInstance.InstantiatePopUp(worldPos, amount.ToString());
        Points += amount;
        UIScore.GetInstance.SetTextScore(Points); 
    }
    public bool HasEnoughPoints(int amount)
	{
        return Points >= amount;
    }
    public void RemovePoints(int amount)
    {
        Points -= amount;
        if (Points < 0)
            Points = 0;

        UIScore.GetInstance.SetTextScore(Points);
    }

    #region VFX
    void TapEffect()
    {
        fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tap.transform.position = fingerPos;
        tap.Play();
        StartCoroutine(StopEffect());
    }
    IEnumerator StopEffect()
    {
        yield return new WaitForSeconds(0.1f);
        tap.Stop();
    }
    #endregion
}
