using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//*****************************************
//创建人： Liu
//功能说明：
//***************************************** 
public class Login : MonoBehaviour
{
    public Text Name;
    public Text PassWord;
    public GameObject window;

    string names;
    string passwords;

    float timer;
    float MaxTimer;

    void Start()
    {
        timer = 0;
        MaxTimer = 3;
        window.GetComponent<Text>().text = "";
        window.SetActive(false);
        window.GetComponent<Text>().color = Color.red;
    }

    void Update()
    {
        if (window.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= MaxTimer)
            {
                window.SetActive(false);
                window.GetComponent<Text>().text = "";
                timer = 0;
            }
        }
    }

    public void Logint()
    {
        if (Name.text != names || PassWord.text != passwords)
        {
            windows(true, "账户名或密码错误");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Zhuce()
    {
        if (Name.text == null || Name.text == "" || PassWord.text == null || PassWord.text == "")
        {
            windows(true, "账户名或密码不能为空");
        }
        else
        {
            names = Name.text;
            passwords = PassWord.text;
            Name.text = null;
            PassWord.text = null;
            windows(true, "注册成功");
        }

    }

    private void windows(bool a, string s)
    {
        if (a)
        {
            window.SetActive(a);
            if (s != null)
            {
                window.GetComponent<Text>().text = s;
            }
        }
    }
}
