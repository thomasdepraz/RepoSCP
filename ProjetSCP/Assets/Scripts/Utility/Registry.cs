using System;
using System.Collections;
using System.Collections.Generic;

public class Registry
{
    static readonly Dictionary<Type, object> register = new Dictionary<Type, object>();

    public void Register<T>(T typeToRegister)
    {
        register[typeof(T)] = typeToRegister;
    }

    public static T Get<T>() where T : class
    {
        if (register.ContainsKey(typeof(T)))
            return register[typeof(T)] as T;

        if(typeof(T).IsInterface)
        {
            foreach(Type t in register.Keys)
            {
                if (typeof(T).IsAssignableFrom(typeof(T)))
                    return register[t] as T;
            }
        }
        if(typeof(T).IsAbstract)
        {
            foreach(Type t in register.Keys)
            {
                if(t.IsSubclassOf(typeof(T)))
                    return register[t] as T;
            }   
        }

        return default(T);
    }
}
