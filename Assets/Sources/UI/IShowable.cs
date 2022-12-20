using UnityEngine;

namespace Balthazariy.UI
{
    public interface IShowable
    {
        bool IsActive { get; }
        GameObject ActivePage { get; }
    }
}