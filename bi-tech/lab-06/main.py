import pandas as pd
from pathlib import Path

class DataProcessor:
  def __init__(self, input_file: str, output_dir: str = "output"):
    self.input_file = input_file
    self.output_dir = Path(output_dir)
    self.data = None
    self._setup_output_directory()

  def _setup_output_directory(self) -> None:
    self.output_dir.mkdir(parents=True, exist_ok=True)

  def load_data(self) -> None:
    print("="*50)
    print(f"Iniciando carga de datos desde {self.input_file}...")
    try:
      self.data = pd.read_excel(self.input_file)
      print("Archivo cargado exitosamente.")
      print(f"Dimensiones del archivo: {self.data.shape[0]} filas, {self.data.shape[1]} columnas")
      print(f"Columnas disponibles: {list(self.data.columns)}")
    except FileNotFoundError:
      raise FileNotFoundError(f"El archivo {self.input_file} no se encontró.")
    except Exception as e:
      raise Exception(f"Error al leer el archivo: {str(e)}")
    print("="*50)

  def activity_1(self) -> None:
    print("\nIniciando Actividad 1: Selección y limpieza de columnas específicas")
    selected_columns = ['CustomerKey', 'FirstName', 'TotalChildren']
    df_subset = self.data[selected_columns].copy()
    print(f"Columnas seleccionadas: {selected_columns}")
    df_clean = df_subset.dropna()
    output_path = self.output_dir / 'Activity_1_Cleaned.xlsx'
    df_clean.to_excel(output_path, index=False)
    print(f"Actividad 1 completada. Archivo exportado a: {output_path}")
    print(f"Filas originales: {len(df_subset)}, Filas después de limpiar: {len(df_clean)}")

  def activity_2(self) -> None:
    print("\nIniciando Actividad 2: Eliminación de filas totalmente vacías")
    selected_columns = ['CustomerKey', 'FirstName', 'TotalChildren']
    df_subset = self.data[selected_columns].copy()
    print(f"Columnas seleccionadas: {selected_columns}")
    df_clean = df_subset.dropna(how='all')
    output_path = self.output_dir / 'Activity_2_Cleaned.xlsx'
    df_clean.to_excel(output_path, index=False)
    print(f"Actividad 2 completada. Archivo exportado a: {output_path}")
    print(f"Filas originales: {len(df_subset)}, Filas después de limpiar: {len(df_clean)}")

  def activity_3(self) -> None:
    print("\nIniciando Actividad 3: Imputación de valores faltantes con la media")
    df_copy = self.data.copy()
    total_children_mean = df_copy['TotalChildren'].mean()
    print(f"Media calculada de 'TotalChildren': {total_children_mean:.2f}")
    df_copy['TotalChildren'] = df_copy['TotalChildren'].fillna(total_children_mean)
    output_path = self.output_dir / 'Activity_3_Cleaned.xlsx'
    df_copy.to_excel(output_path, index=False)
    print(f"Actividad 3 completada. Archivo exportado a: {output_path}")
    missing_after = df_copy['TotalChildren'].isnull().sum()
    print(f"Valores faltantes en 'TotalChildren' después de imputar: {missing_after}")

  def activity_4(self) -> None:
    print("\nIniciando Actividad 4: Análisis y eliminación de duplicados")
    print("\nResumen de valores faltantes:")
    missing_values = self.data.isnull().sum()
    print(missing_values[missing_values > 0])

    duplicated_count = self.data.duplicated().sum()
    print(f"\nCantidad de filas duplicadas encontradas: {duplicated_count}")

    print("\nTop 5 clientes más frecuentes:")
    customer_counts = self.data['CustomerKey'].value_counts()
    print(customer_counts.head())

    df_clean = self.data.drop_duplicates()
    output_path = self.output_dir / 'Activity_4_Cleaned.xlsx'
    df_clean.to_excel(output_path, index=False)
    print(f"\nActividad 4 completada. Archivo exportado a: {output_path}")
    print(f"Filas originales: {len(self.data)}, Filas después de eliminar duplicados: {len(df_clean)}")

  def run_all_activities(self) -> None:
    print("\nIniciando procesamiento completo de actividades...")
    self.load_data()
    try:
      #self.activity_1()
      #self.activity_2()
      #self.activity_3()
      self.activity_4()
      print("\nProcesamiento de datos completado exitosamente.")
    except Exception as e:
      print(f"Error durante la ejecución de las actividades: {str(e)}")


def main():
  processor = DataProcessor(input_file='data/BI_Clientes06.xlsx', output_dir='output')
  processor.run_all_activities()

if __name__ == "__main__":
  main()
