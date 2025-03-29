namespace Lab2_FreddyQuea_01.Models;

public class Auto<T> where T : class
{
  public int Id { get; set; }
  public string Marca { get; set; } = string.Empty;
  public string Modelo { get; set; } = string.Empty;
  public string Placa { get; set; } = string.Empty;
  public string SedeTransito { get; set; } = string.Empty;
  public T? Propietario { get; set; }
}