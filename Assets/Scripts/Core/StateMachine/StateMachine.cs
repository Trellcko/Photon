using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trellcko.MonstersVsMonsters.Core.SM
{
    public class StateMachine
    {
        private Dictionary<Type, BaseState> _states;

        private BaseState _currentState;

        public StateMachine(params BaseState[] states)
        {
            _states = new Dictionary<Type, BaseState>();
            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
                state.StateCompleted += SetState;
            }
            _states.Add(typeof(EmptyState), new EmptyState());
            _currentState = states[^1];
        }
        public void Update()
        {
            _currentState?.CheckTransition();
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
        public void SetState<T>() where T : BaseState
        {
            SetState(typeof(T));
        }

        private void SetState(Type type)
        {
            _currentState?.Exit();
            if (_states.ContainsKey(type))
            {
                BaseState state = _states[type];
                _currentState = state;
                _currentState.Enter();
                return;
            }

            Debug.LogError("State: " + type.Name + " is not inclued");
        }
    }
}