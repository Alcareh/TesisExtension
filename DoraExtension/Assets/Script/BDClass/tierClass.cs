using System;
using System.Collections.Generic;
[System.Serializable]
public class tierClass
{
    public Item[] items;
}
[Serializable]
public class Item
{
    public string _id;
    public string name;
    public string fondoAvatar;
    public string avatarUser;
    public int puntos;
    public int tiempo;
}
