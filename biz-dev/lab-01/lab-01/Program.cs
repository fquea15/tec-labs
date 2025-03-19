using System;

namespace lab_01
{
  class Auto<T>
  {
    public string Placa { get; }
    public string Sede { get; }
    public T Propietario { get; }

    public Auto(string placa, string sede, T propietario)
    {
      Placa = placa;
      Sede = sede;
      Propietario = propietario;
    }

    public override string ToString() =>
      $"Auto: {Placa}, Sede: {Sede}, Propietario: {Propietario}";
  }

  class Persona
  {
    public int Id { get; }
    public string Nombre { get; }
    public int Dni { get; }

    public Persona(int id, string nombre, int dni)
    {
      Id = id;
      Nombre = nombre;
      Dni = dni;
    }

    public override string ToString() =>
      $"Persona - ID: {Id}, Nombre: {Nombre}, DNI: {Dni}";
  }

  class Empresa
  {
    public int Id { get; }
    public string RazonSocial { get; }
    public int Ruc { get; }

    public Empresa(int id, string razonSocial, int ruc)
    {
      Id = id;
      RazonSocial = razonSocial;
      Ruc = ruc;
    }

    public override string ToString() =>
      $"Empresa - ID: {Id}, Razón Social: {RazonSocial}, RUC: {Ruc}";
  }

  class Program
  {
    static void Main()
    {
      var persona = new Persona(1, "Freddy Quea", 12345678);
      var empresa = new Empresa(2, "Empresa 1", 1234567890);

      var autoDePersona = new Auto<Persona>("ABC-123", "Sede 1", persona);
      var autoDeEmpresa = new Auto<Empresa>("DEF-456", "Sede 2", empresa);

      Console.WriteLine(autoDePersona);
      Console.WriteLine(autoDeEmpresa);
    }
  }
}


// Ejercicio f
/*
 namespace lab_01
{
  class Program
  {

    static double FahrenheitToCelsius(double fahrenheit) => (5.0 / 9.0) * (fahrenheit - 32.0);
    static double CelsiusToFahrenheit(double celsius) => (9.0 / 5.0) * celsius + 32.0;

    static void Main(string[] args)
    {
      Console.WriteLine("Ingrese la temperatura: ");
      var temp = Convert.ToDouble(Console.ReadLine());

      Console.WriteLine("Convertir a (C/F):");
      var opcion = char.ToUpper(Console.ReadKey().KeyChar);
      Console.WriteLine();

      if (opcion == 'C')
        Console.WriteLine("La temperatura en Celsius es: " + FahrenheitToCelsius(temp));
      else if (opcion == 'F')
        Console.WriteLine("La temperatura en Fahrenheit es: " + CelsiusToFahrenheit(temp));
      else
        Console.WriteLine("Opcion no valida");
    }
  }
};
 */


// Ejercicios [a, b, c, d, e]
/*

namespace lab_01
{
 class Program
 {
   static void Saludar()
   {
     Console.WriteLine("¡Hola! Bienvenido a la sesion practica de c#");
   }

   static int Sumar(int a, int b)
   {
     return a + b;
   }

   static int Restar(int a, int b)
   {
     return a - b;
   }

   static int Multiplicar(int a, int b)
   {
     return a * b;
   }

   static double Dividir(int a, int b)
   {
     return a / b;
   }

   static bool EsPrimo(int numero)
   {
     if (numero < 2) return false;
     if (numero < 4) return true;
     if (numero % 2 == 0 || numero % 3 == 0) return false;
     for (int i = 5; i <= numero; i+=6)
       if (numero % i == 0 || numero % (i + 2) == 0)
         return false;
     return true;
   }

   static void ImprimirPrimos(int cantidad)
   {
     for (int i = 2, contador = 0; contador < cantidad; i++)
       if (EsPrimo(i))
       {
         Console.WriteLine(i);
         contador++;
       }
   }

   static void Main(string[] args)
   {
     // Console.WriteLine("¡Numeros Primos!:");
     // ImprimirPrimos(10);

     // Saludar();
     // const int num1 = 10;
     // const int num2 = 5;
     // Console.WriteLine("Operaciones con numeros:");
     // Console.WriteLine("La suma es: " + Sumar(num1, num2));
     // Console.WriteLine("La resta es: " + Restar(num1, num2));
     // Console.WriteLine("La multiplicacion es: " + Multiplicar(num1, num2));
     // Console.WriteLine("La division es: " + Dividir(num1, num2));
   }
 }
}
 */