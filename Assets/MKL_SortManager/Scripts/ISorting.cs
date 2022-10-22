using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MKL.SortManager
{
    public interface ISorting<T>
    {
        void SortBy(T SortType);    
    }
}