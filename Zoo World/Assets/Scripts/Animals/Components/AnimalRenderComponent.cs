using UnityEngine;
using System;

namespace ZooWorld.Animals.Components
{
    public class AnimalRenderComponent : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        public void ApplyMaterial(Material material)
        {
            if (material == null)
                return;

            _renderer.sharedMaterial = material;
        }
    }
}
