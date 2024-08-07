namespace EncryptDecrypt.Services
{
	public interface ICloneable<T>
		where T : class
	{
		T Clone();
	}
}
