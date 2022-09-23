namespace Tweens
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public delegate T1 Getter<T1>();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="newValue"></param>
    public delegate void Setter<T1>(T1 newValue);
}
