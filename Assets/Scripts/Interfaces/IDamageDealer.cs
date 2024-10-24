using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    public int GetDamage();
    public void DealDamage(IHasHealth iHasHealth);
}
