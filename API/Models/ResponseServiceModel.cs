namespace API.Models
{
  public class ResponseServiceModel<T>
  {
    public T Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = null;
  }
}