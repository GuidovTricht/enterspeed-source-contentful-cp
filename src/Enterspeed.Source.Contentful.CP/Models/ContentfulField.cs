using System;

namespace Enterspeed.Source.Contentful.CP.Models;

public abstract class ContentfulField
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public object Value { get; set; }

    public T GetValue<T>()
    {
        return (T)Value;
    }
}