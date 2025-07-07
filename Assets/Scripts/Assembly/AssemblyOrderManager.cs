using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyOrderManager : MonoBehaviour
{
    private HashSet<int> placedOrders = new HashSet<int>();

    public void MarkAsPlaced(int orderIndex)
    {
        if (!placedOrders.Contains(orderIndex))
            placedOrders.Add(orderIndex);
    }

    public bool IsOrderCompleted(int orderIndex)
    {
        return placedOrders.Contains(orderIndex);
    }

    public bool ArePreviousOrdersCompleted(int currentIndex)
    {
        for (int i = 0; i < currentIndex; i++)
        {
            if (!placedOrders.Contains(i))
                return false;
        }
        return true;
    }

    public void ResetAll()
    {
        placedOrders.Clear();
    }
}
