using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// РџРѕР·РІРѕР»СЏРµС‚ РѕР±СЉРµРєС‚Сѓ Р±С‹С‚СЊ СЃРєСЂС‹С‚РЅС‹Рј
/// </summary>
public interface ICanStealth 
{
    public bool IsStealthy { get; set; }
    public void EnableStealt();
    public void DisableStealt();
}
