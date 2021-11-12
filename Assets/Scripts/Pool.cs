using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : PoolObject
{
    protected List<T> pool = new List<T>();
    [SerializeField] protected int max_pool_size = 5;

    protected T Pull(T target) 
    {
        T instance = pool.FirstOrDefault(i => i.ID == target.ID);

        if (instance == null)
            return null;

        pool.Remove(instance);
        PullIternal(target, instance);

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

        PushIternal(target);
    }

    protected virtual void PullIternal(T target, T instance) { }
    protected virtual void PushIternal(T target) { }

    public override string ToString()
    {
        string result = "";

        foreach (T item in pool)
        {
            result += item.transform.GetInstanceID() + ", ";
        }

        return result;
    }
}