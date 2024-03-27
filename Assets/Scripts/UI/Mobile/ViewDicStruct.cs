using System;
using UnityEngine;

namespace Muks.MobileUI
{
    [Serializable]
    public struct ViewDicStruct
    {
        [Tooltip("Key")]
        public string Name;

        [Tooltip("Value")]
        public MobileUIView UIView;
    }
}