namespace aPC.Client.Disco.Generators
{
  public interface IGenerator<out T>
  {
    T Generate();
  }
}