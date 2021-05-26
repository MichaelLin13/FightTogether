using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New ButtonControl", menuName = "Button Control/New Button Control")]
public class ButtonControl : ScriptableObject
{
    public KeyCode UpBtn;
    public KeyCode DownBtn;
    public KeyCode LeftBtn;
    public KeyCode RightBtn;
    public KeyCode AttackBtn;
    public KeyCode JumpBtn;
    public KeyCode DefenseBtn;   
    
}
