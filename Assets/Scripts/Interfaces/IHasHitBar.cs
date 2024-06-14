using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHitBar
{

    public event EventHandler<OnHitChangedEventArgs> OnHitChanged;

    
    public event EventHandler<OnHitMissedEventArgs> OnHitMissed;

    public event EventHandler<OnHitRightEventArgs> OnHitRight;

    public event EventHandler OnHitInterrupted;

    public event EventHandler OnHitFinished;
    public class OnHitChangedEventArgs : EventArgs
    {
        public int hitNumber;
    }

    public class OnHitMissedEventArgs : EventArgs
    {
        public bool missed;
    }

    public class OnHitRightEventArgs : EventArgs
    {
        public bool hitRight;
    }
}
