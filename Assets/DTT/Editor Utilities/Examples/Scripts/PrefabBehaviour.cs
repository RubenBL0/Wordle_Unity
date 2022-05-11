using UnityEngine;

public class PrefabBehaviour : MonoBehaviour
{
   [SerializeField]
   private int _value;

   public void UpdateValue(int value) => _value = value;
}
