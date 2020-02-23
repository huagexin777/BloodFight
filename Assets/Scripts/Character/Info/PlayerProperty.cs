using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    private int hp = 1000;//血量
    private int shield = 1000;//护盾
    private int speed = 300;//速度
    private int phy_strength = 100;//物理伤害
    private int phy_defense = 50;//物理防御力

    public int Hp 
    {
        get => hp; 
        set
        {
            if (hp<=0)
            {
                hp = 0;
            }
        } 
    }
    public int Shield { get => shield; set => shield = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Phy_strength { get => phy_strength; set => phy_strength = value; }
    public int Phy_defense { get => phy_defense; set => phy_defense = value; }


}
