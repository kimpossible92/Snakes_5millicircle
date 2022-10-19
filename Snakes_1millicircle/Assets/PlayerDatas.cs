using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatas : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Dropdown dropdown1, dropdown2, dropdown3;
    public float SpeedPlayer = 1f;
    public float radiusPlayer = 15f;
    public float times = 15f;
    public PlayerDatas pd;
    public void setPlayerData()
    {
        pd.SpeedPlayer = 1f;
        pd.SpeedPlayer += (0.2f)*dropdown1.value;
    }
    public void setPlayerData2()
    {
        pd.radiusPlayer = 15f;
        pd.radiusPlayer += dropdown2.value;

    }
    public void setPlayerTime()
    {
        pd.times = 15f;
        pd.times += dropdown3.value;
    }
    PlayerDatas()
    {
        SpeedPlayer = 1f;
        radiusPlayer = 1f;
        times = 15f;
    }
    // Start is called before the first frame update
    void Start()
    {
        pd = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Prog>() != null)
        {
            FindObjectOfType<Prog>().SpeedPlayer = pd.SpeedPlayer;
            FindObjectOfType<Prog>().radiusPlayer = pd.radiusPlayer;
            FindObjectOfType<Prog>().times = pd.times;
        }
    }
}
