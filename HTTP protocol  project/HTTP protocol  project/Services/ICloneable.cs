namespace HTTP_protocol__project.Services;

public interface ICloneable<T>
    where T : class
{
    T Clone();
}