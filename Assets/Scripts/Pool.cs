using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : PoolObject
{
    protected List<T> pool = new List<T>();
    protected int max_pool_size = 5;

    protected T Pull(T target) 
    {
        T instance = pool.FirstOrDefault(i => i.ID == target.ID);

        if (instance == null)
            return null;

        pool.Remove(instance);
        PullIternal();

        return instance;
    }
    protected void Push(T target)
    {
        pool.Add(target);

        if (pool.Count() > max_pool_size)
        {
            Destroy(pool[0].gameObject);
            pool.RemoveAt(0);
        }

        PushIternal();
    }

    protected virtual void PullIternal() { }
    protected virtual void PushIternal() { }

    public override string ToString()
    {
        string result = "";

        foreach (T item in pool)
        {
            result += item.name + ", ";
        }

        return result;
    }
}