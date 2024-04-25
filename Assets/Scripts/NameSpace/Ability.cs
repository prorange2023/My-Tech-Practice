using System.Collections;
using UnityEngine;

namespace CreatingCharacter.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] private string abilityName = "New Ability Name";
        [SerializeField] private string abilityDescription = "New Ability Descriotion";
        [SerializeField] private float abilityCooldown = 1f;

        public abstract IEnumerable Cast();
    }
}
