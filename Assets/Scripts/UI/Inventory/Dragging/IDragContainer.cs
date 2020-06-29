using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shakespeare.UI.Inventories.Dragging
{
    public interface IDragContainer<T> : IDragDestination<T>, IDragSource<T> where T : class
    {

    }
}