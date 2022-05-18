using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyPlayerConnexionUpdate
{
    public const int id = 5;
    public string destination = "/parties/" + StaticVariable.nameOfThePartyIn + "/players/connection";
    public List<string> players;
    public List<string> playersDisconnected;
}
