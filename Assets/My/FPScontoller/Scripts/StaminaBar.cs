using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;

    private float maxStamina = 100;
    private float currentStamina;

    [Header("Stamina regen parametrs")]
    [SerializeField] private float regenSpeed = 1;//скорость пополнения 
    [SerializeField] private float timeToStartRegen = 2;//скорость пополнения 

    private Coroutine regen;

    public static StaminaBar instance;

    public GameObject player;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);//время между вызовами метода пополнения

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void Update()
    {
        if (staminaBar.value == maxStamina) staminaBar.gameObject.SetActive(false);
  
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            staminaBar.gameObject.SetActive(true);
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("Not Enough Stamina");
            player.GetComponent<FirstPersonContoller>().canSprint = false;//выключаем бег   [включаем в регене]
            player.GetComponent<FirstPersonContoller>().canJump = false;//выключаем прыжок  [включаем в регене]
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeToStartRegen);

        while (currentStamina < maxStamina)
        {
            currentStamina += regenSpeed;
            staminaBar.value = currentStamina;
            player.GetComponent<FirstPersonContoller>().canSprint = true;
            player.GetComponent<FirstPersonContoller>().canJump = true;
            yield return regenTick;
        }
    }
}