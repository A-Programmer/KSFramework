namespace KSFramework.Exceptions;

public class KSArgumentNullException : KSException
{
	public KSArgumentNullException(int code)
		: base(code)
	{

	}

	public KSArgumentNullException(string message) : base(message)
	{

	}
}