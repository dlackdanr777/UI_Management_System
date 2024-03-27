using System;
using UnityEngine;

namespace Muks.PcUI
{
    [Serializable]
    public struct ViewDicStruct
    {
        [Tooltip("Key")]
        public string Name;

        [Tooltip("Value")]
        public PcUIView UIView;
    }
}
