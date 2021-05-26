using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class PlayerButtonManager : MonoBehaviour
{
    static PlayerButtonManager instance;
    public GameObject thisPlayer;
    public ButtonControl thisPlayerButton;
    public List<GameObject> PlayerList = new List<GameObject>();
    public bool playerAttackOn;
    public bool playerJumpOn;
    public bool lockPlayer;
    int lockCount=1;
    int BackFirstPic = 0;
    int BackSecondPic = 1;
    int CharacterPicValue = 0;
    int[] Player8 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    Sprite CharacterCreat;

    List<Texture2D> allTex2d = new List<Texture2D>();

	private void Awake()
	{
        //if (instance != null)
        //    Destroy(this);
        instance = this;
	}
	void Start()
	{
        LoadCharacterPic();
        
        

    }

	void Update()
    {
        Press_Up();
        Prsee_Left();
        Prsee_Right();
        Prsee_Attack();
        Prsee_Jump();
        //StartFinalCounter();
    }

    public void Press_Up()
	{
        if(Input.GetKeyDown(thisPlayerButton.UpBtn))
        SceneManager.LoadScene(0);
	}

    public void Prsee_Down()
	{

	}
    public void Prsee_Left()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
		{
            if (Input.GetKeyDown(thisPlayerButton.LeftBtn) && playerAttackOn == true)
            {
                if (BackSecondPic > 1)
                {
                    BackSecondPic--;
                    CharacterCreat = Sprite.Create(allTex2d[BackSecondPic], new Rect(0, 0, allTex2d[BackSecondPic].width, allTex2d[BackSecondPic].height), Vector2.zero);
                    thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = CharacterCreat;
                }
                else if (BackSecondPic <= 1)
                {
                    BackSecondPic = CharacterPicValue;
                    CharacterCreat = Sprite.Create(allTex2d[BackSecondPic], new Rect(0, 0, allTex2d[BackSecondPic].width, allTex2d[BackSecondPic].height), Vector2.zero);
                    thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = CharacterCreat;
                }
            }
        }
    }
    public void Prsee_Right()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
		{
            if (Input.GetKeyDown(thisPlayerButton.RightBtn) && playerAttackOn == true)
			{
                if (BackSecondPic < CharacterPicValue )
                {
                    BackSecondPic++;
                    CharacterCreat = Sprite.Create(allTex2d[BackSecondPic], new Rect(0, 0, allTex2d[BackSecondPic].width, allTex2d[BackSecondPic].height), Vector2.zero);
                    thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = CharacterCreat;
                }
                else if(BackSecondPic >= CharacterPicValue )
                {
                    BackSecondPic = 1;
                    CharacterCreat = Sprite.Create(allTex2d[BackSecondPic], new Rect(0, 0, allTex2d[BackSecondPic].width, allTex2d[BackSecondPic].height), Vector2.zero);
                    thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = CharacterCreat;
                }
            }
		}        
    }
    public void Prsee_Attack()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
           
            if (Input.GetKeyDown(thisPlayerButton.AttackBtn) && playerAttackOn == false) 
			{
                //thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load("CharacterPic/CharacterGridRandom", typeof(Sprite)) as Sprite;
                CharacterCreat = Sprite.Create(allTex2d[BackSecondPic], new Rect(0, 0, allTex2d[BackSecondPic].width, allTex2d[BackSecondPic].height), Vector2.zero);
                thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite= CharacterCreat;
                //thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = allTex2d[1];
                playerAttackOn = true;//確認是否進入選角
                lockCount = 1;
            }
            else if(Input.GetKeyDown(thisPlayerButton.AttackBtn) && playerAttackOn != false)
			{
                CheckPlayerId();
                lockPlayer = true;//是否鎖定選角
                lockCount = 0;
                playerAttackOn = false;
                StartFinalCounter();
            }                
        }            
    }
    public void Prsee_Jump()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(thisPlayerButton.JumpBtn))
            {
                if (lockPlayer == true)
                {
                    playerAttackOn = false;
                    lockPlayer = false;
                    BackSecondPic = 1;
                    lockCount = 0;
                    CharacterCreat = Sprite.Create(allTex2d[BackFirstPic], new Rect(0, 0, allTex2d[BackFirstPic].width, allTex2d[BackFirstPic].height), Vector2.zero);
                    thisPlayer.transform.GetChild(0).GetComponent<Image>().sprite = CharacterCreat;
                }
                else
                {

                }
            }
        }
    }
    public void Prsee_Defense()
    {

    }
    

    void LoadCharacterPic()
    {
        List<string> filePaths = new List<string>();
        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG";
        string[] ImageType = imgtype.Split('|');
        CharacterPicValue = ImageType.Length;
        for (int i = 0; i < ImageType.Length; i++)
        {
            //獲取d盤中a資料夾下所有的圖片路徑  
            string[] dirs = Directory.GetFiles(Application.dataPath + "\\FightTogether\\Menu\\CharacterSelection\\CharacterPic", ImageType[i]);
            for (int j = 0; j < dirs.Length; j++)
            {
                filePaths.Add(dirs[j]);
            }
        }

        for (int i = 0; i < filePaths.Count; i++)
        {
            Texture2D tx = new Texture2D(75, 60);
            tx.LoadImage(getImageByte(filePaths[i]));
            allTex2d.Add(tx);
        }
    }

    

    byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];       
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }

    void StartFinalCounter()
	{
        if (SceneManager.GetActiveScene().buildIndex == 2)
		{
            if (lockCount == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Player8[i] != 0)
                    {
                        CharacterCreat = Sprite.Create(allTex2d[3], new Rect(0, 0, allTex2d[3].width, allTex2d[3].height), Vector2.zero);
                        PlayerList[i].transform.GetChild(0).GetComponent<Image>().sprite = CharacterCreat;
                    }
                }
            }
		}

    }

    void CheckPlayerId()
	{
        
        for (int i = 0; i < 8; i++) 
		{
            if (thisPlayer.name == PlayerList[i].name) 
			{
                Player8[i] = 0;
			}
		}
	}

}
