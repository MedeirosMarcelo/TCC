using System;
using System.Linq;
using System.Reflection;

public class StateLoader<FsmType>
    where FsmType : BaseFsm
{
    public void LoadStates(FsmType fsm, string @namespace)
    {
        if (!string.IsNullOrEmpty(@namespace))
        {
            Type[] types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Namespace == @namespace &&
                               type.GetInterfaces().Contains(typeof(IState)) &&
                               !type.IsAbstract)
                .ToArray();
            foreach (Type t in types)
            {
                var state = (IState)Activator.CreateInstance(t, fsm);
                fsm.AddStates(state);
            }
        }
    }
}
