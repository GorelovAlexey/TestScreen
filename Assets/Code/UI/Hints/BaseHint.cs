using UnityEngine;

namespace Assets.Code.UI
{
    public abstract class BaseHint<T> : MonoBehaviour
    {
        protected T data;
        public virtual void SetData(T hintData)
        {
            data = hintData;
        }

        //public virtual void DisplayHint();
       // public abstract void HideHint();
    }

    public class RewardHint : BaseHint<string>
    {
        public override void SetData(string hintData)
        {
            throw new System.NotImplementedException();
        }
    }
}