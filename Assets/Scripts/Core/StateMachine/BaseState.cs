using System;
using System.Collections.Generic;

namespace Trellcko.MonstersVsMonsters.Core.SM
{
    public abstract class BaseState
    {
        public event Action<Type> StateCompleted;
        private Dictionary<Func<bool>, Action> _transitions = new();

        public void CheckTransition()
        {
            foreach (var transition in _transitions)
            {
                if (transition.Key())
                {
                    transition.Value();
                }
            }
        }
        public virtual void Enter() { }
        public virtual void Update() { }

        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
        protected void GoToState<T>(Func<bool> when) where T : BaseState
        {
            _transitions.Add(when, GoToState<T>);
        }
        protected void GoToState<T>() where T : BaseState
        {
            StateCompleted?.Invoke(typeof(T));
        }
    }
}
