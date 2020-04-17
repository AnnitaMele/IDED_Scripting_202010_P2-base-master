using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterPooling : MonoBehaviour
{

    public static InterPooling instance;

    List<GameObject> objective = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void ReturnPool(GameObject _thething)
    {
        objective.Add(_thething);
        _thething.SetActive(false);
    }

    IEnumerator Timer(GameObject _thetime)
    {
        yield return new WaitForSeconds(10f);
        ReturnPool (_thetime);
    }

    public GameObject RequestTT(GameObject _theitem)
    {
        
        GameObject selected;
        GameObject thing = objective.Find(x => x == _theitem);
        if (thing != null)
        {
            selected = thing;
            objective.Remove(selected);
            selected.SetActive(true);
        }
        else
        {
            selected = Instantiate(_theitem, transform);
        }
        return selected;
    }
    
    
    
}
