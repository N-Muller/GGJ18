public class Wrapper<T> 
{
	public T value;

	public Wrapper (T value)
	{
		this.value = value;
	}

	public static implicit operator T(Wrapper<T> w)
	{
		return w.value;
	}
}