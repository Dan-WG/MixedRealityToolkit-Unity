﻿using UnityEngine;

namespace MRTK.Grabbables
{
    /// <summary>
    /// Extends its behaviour from BaseThrowable. This is a non-abstract script that's actually attached to throwable object
    /// This script will not work without a grab script attached to the same gameObject
    /// </summary>

    public class ThrowableObject : BaseThrowable
    {
        public override void Throw(BaseGrabbable grabbable)
        {
            base.Throw(grabbable);
            GetComponent<Rigidbody>().velocity = MotionControllerInfoTemp.GetVelocity(grabbable.GrabberPrimary) * grabbable.GrabberPrimary.Strength * ThrowMultiplier;

            if (ZeroGravityThrow)
            {
                GetComponent<Rigidbody>().useGravity = false;
            }
        }

        private void Update()
        {
            if (Thrown)
            {
                f += 0.01f;
                if (f < 1)
                {
                    //GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + new Vector3((1.1f * LeftRightCurveOverTime.Evaluate(f)), (1.1f * UpDownCurveOverTime.Evaluate(f)), (1.1f * LeftRightCurveOverTime.Evaluate(f)));
                }
            }
        }

        private float f = 0;
    }
}