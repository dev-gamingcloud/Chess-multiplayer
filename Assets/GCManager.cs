using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gamingCloud;
using gamingCloud.templates;
using gamingCloud.Network;
using Newtonsoft.Json;

class MyPlayer
{
    public string username;
    public string password;
    public int? score;

    public MyPlayer(string _username, string _password, int? _score)
    {
        this.username = _username;
        this.password = _password;
        this.score = _score;
    }

}
public class GCManager : GCMultiPlayer
{
    public GameObject[] nodes;
    public bool isMineTurn;
    private async void Start()
    {
        // await Players.LogOut();


        // MyPlayer data = new MyPlayer("hamed", "1234", 1010);

        // ApiResponse result = await Players.CreatePlayer<MyPlayer>(data);
        // Debug.Log(result.status);

        // Dictionary<string, dynamic> body = new Dictionary<string, dynamic>();
        // /* 
        //     schema پر کردن فیلدهای 
        // */
        // body.Add("score", 50000000);
        // ApiResponse result = await Players.EditPlayer<Dictionary<string, dynamic>>(body);

        ConnectToMultiPlayerServer();
        nodes = GameObject.FindGameObjectsWithTag("Node");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            leaveRoom();




        }
    }
    public override void ConnectedToServer()
    {
        Debug.Log("Connected");

        string name = "Chess1";
        bool isPrivate = false;
        int maxPlayersInRoom = 12;
        RoomSetting room = new RoomSetting(name, isPrivate, maxPlayersInRoom);
        JoinToServer(room);
    }

    public void ClickTOChangeTheTurn()
    {
        SendEvent("ChangeTurn", SendPacketMode.nspMode);
    }
    public void detectWinner(PlayerModel sender, string content)
    {
        Debug.Log("winner is " + content);
    }
    public void itemData(PlayerModel sender, string content)
    {
        pos GetPos = JsonConvert.DeserializeObject<pos>(content);
        string newName = GetPos.name.Replace("White", "Black");
        Debug.Log(newName[newName.Length - 1]);

        GameObject go = GameObject.Find(newName);
        go.transform.position = new Vector3(GetPos.x, GetPos.y, GetPos.z * -1);
        Debug.Log("data is " + content);

    }
    class pos
    {
        public float x, y, z;
        public string name;

    }
    public override void OnJoined()
    {
        Debug.Log("i can join");
        Debug.Log("Am i Master ? " + isMasterPlayer);
        isMineTurn = isMasterPlayer;

    }

    public string ItemName;
    public void SendItemData(Vector3 pos, string name)
    {
        pos pos1 = new pos();
        pos1.x = pos.x;
        pos1.z = pos.z;
        pos1.y = pos.y;

        pos1.name = name;
        SendEvent("itemData", JsonConvert.SerializeObject(pos1), SendPacketMode.BroadCast);

    }
    void ChangeTurn(PlayerModel sender)
    {
        isMineTurn = !isMineTurn;
        Debug.Log("turn has changed");
    }
    public override void OnJoinNewPlayer(PlayerModel newPlayer)
    {
        Debug.Log("so is joined : " + newPlayer.name);
    }
    public override void OnLeavePlayer(PlayerModel player)
    {
        Debug.Log("so is left : " + player.name);
    }
    void OnDestroy()
    {
        disconnect();
    }
}
