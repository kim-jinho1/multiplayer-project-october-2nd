using UnityEngine;

namespace Code.CoreGameLogic
{
    public interface IMouseOver
    {
        Material[] OriginalMaterials { get; }
        Material ChangeMaterial { get; }
    }
}