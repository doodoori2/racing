using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 0.1f;
    public bool slowMode = false;
    public Transform starGroupTransform;
    public Transform rockGroupTransform;
    public Slider hpSlider;
    public int startHp = 10;
    public int hp = 10;
    public Text parsec1;
    public Text parsec2;
    public float distance;
    public float slowParsecSpeed = 100.1234f / (3 * 60);
    public float fastParsecSpeed = 333.2345f / (2 * 60);
    public GameObject textPanel;
    public Text gameOverText;
    public Text scoreText;
    public AudioClip bgmClip;

    private void Awake()
    {
        hpSlider.maxValue = hp;
    }

    private void Start()
    {
        AudioSource.PlayClipAtPoint(bgmClip, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        var s = false;

        hpSlider.value = hp;

        if (hp <= 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                RestartGame();
            }
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            s = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            s = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            s = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            s = true;
        }

        var cam = Camera.main;
        var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        //var topLeft = cam.ViewportToWorldPoint(new Vector3(-1, 1, cam.nearClipPlane));
        var bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //var bottomRight = cam.ViewportToWorldPoint(new Vector3(1, -1, cam.nearClipPlane));

        var xClipped = Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x);
        var yClipped = Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y);
        transform.position = new Vector3(xClipped, yClipped, transform.position.z);

        if (s != slowMode)
        {
            //SetSlowMode(s);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetSlowMode(!slowMode);
        }

        parsec1.text = distanceString;
        parsec2.text = distanceString;

        distance += (slowMode ? slowParsecSpeed : fastParsecSpeed) * Time.deltaTime;
    }

    public string distanceString { get { return string.Format("{0:N0} P.C.", distance); } }

    internal void AddDamage(int v)
    {
        if (hp > 0)
        {
            hp--;
        }

        if (hp <= 0)
        {
            textPanel.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            scoreText.text = distanceString;
        }
    }

    private void SetSlowMode(bool s)
    {
        slowMode = s;

        //Debug.LogFormat("Slow Mode: {0}", slowMode);

        for (int i = 0; i < starGroupTransform.childCount; i++)
        {
            var star = starGroupTransform.GetChild(i).GetComponent<Star>();
            star.SetSlowMode(slowMode);
        }

        for (int i = 0; i < rockGroupTransform.childCount; i++)
        {
            var rock = rockGroupTransform.GetChild(i).GetComponent<Rock>();
            rock.SetSlowMode(slowMode);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogFormat("Collided with {0}", other.name);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Splash");

        //distance = 0;
        //hp = startHp;
        //for (int i = 0; i < starGroupTransform.childCount; i++)
        //{
        //    Destroy(starGroupTransform.GetChild(i).gameObject);
        //}
        //for (int i = 0; i < rockGroupTransform.childCount; i++)
        //{
        //    Destroy(rockGroupTransform.GetChild(i).gameObject);
        //}
        //transform.position = new Vector3(0, 0, transform.position.z);

        //textPanel.SetActive(false);
    }
}
