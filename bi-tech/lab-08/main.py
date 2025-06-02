import pandas as pd
import numpy as np
from sklearn.linear_model import LinearRegression
import matplotlib.pyplot as plt
import seaborn as sns
from io import StringIO
from pathlib import Path
from typing import List, Tuple

DATA_FILE = "data/BI_Alumnos08.xlsx"
OUTPUT_DIR = Path("outputs")
FEATURE_COLUMNS = ["Altura", "Edad"]
TARGET_COLUMN = "Peso"
PLOT_FILE = OUTPUT_DIR / "regression_plot_seaborn.png"
TABLE_FILE = OUTPUT_DIR / "prediction_table.csv"


# Clase para el modelo de regresión lineal múltiple
class MultipleLinearRegressionModel:
  # Inicializa el modelo con los parámetros necesarios
  def __init__(self, data_source: str, feature_cols: List[str], target_col: str):
    OUTPUT_DIR.mkdir(exist_ok=True)
    self.data_source = data_source
    self.feature_cols = feature_cols
    self.target_col = target_col
    self.data = None
    self.model = LinearRegression()
    self.coefficients = None
    self.intercept = None
    self.predictions = None
    
  # Función para cargar los datos
  def load_data(self) -> None:
    try:
      self.data = pd.read_excel(self.data_source)
    except Exception as e:
      raise ValueError(f"Error al cargar los datos: {e}")

  # Función para preprocesar los datos
  def preprocess_data(self) -> None:
    required_cols = self.feature_cols + [self.target_col]
    if not all(col in self.data.columns for col in required_cols):
      raise ValueError(f"Columnas requeridas no encontradas: {required_cols}")

    if self.data[required_cols].isnull().any().any():
      print("Advertencia: Valores nulos detectados. Eliminando filas afectadas.")
      self.data = self.data.dropna(subset=required_cols)

    for col in required_cols:
      self.data[col] = pd.to_numeric(self.data[col], errors='coerce')
    if self.data[required_cols].isnull().any().any():
      raise ValueError("Datos no numéricos detectados en las columnas seleccionadas.")

  # Función para entrenar el modelo
  def train_model(self) -> None:
    x = self.data[self.feature_cols].values
    y = self.data[self.target_col].values

    try:
      self.model.fit(x, y)
      self.intercept = self.model.intercept_
      self.coefficients = self.model.coef_
    except Exception as e:
      raise RuntimeError(f"Error al entrenar el modelo: {e}")

  # Función para generar predicciones
  def generate_predictions(self) -> pd.DataFrame:
    x = self.data[self.feature_cols].values
    self.predictions = self.model.predict(x)

    prediction_table = pd.DataFrame({
      'Nombre': self.data.get('Nombres', ['-'] * len(self.data)),
      'Altura': self.data['Altura'],
      'Edad': self.data['Edad'],
      'Peso Real': self.data[self.target_col],
      'Peso Predicho': np.round(self.predictions, 2),
      'Residual': np.round(self.data[self.target_col] - self.predictions, 2)
    })
    prediction_table.to_csv(TABLE_FILE, index=False)
    return prediction_table

  # Fnción para graficar
  def plot_results(self) -> None:
    sns.set_theme(style="ticks", palette="deep")
    sns.set_style("ticks")
    sns.set_context("talk")

    fig = plt.figure(figsize=(12, 8))
    ax = fig.add_subplot(111, projection='3d')

    scatter_color = sns.color_palette("deep", 1)[0]
    ax.scatter(self.data['Altura'], self.data['Edad'], self.data[self.target_col],
              color=scatter_color, s=100, label='Datos Reales', edgecolor='k')

    altura_range = np.linspace(self.data['Altura'].min(), self.data['Altura'].max(), 20)
    edad_range = np.linspace(self.data['Edad'].min(), self.data['Edad'].max(), 20)
    altura_mesh, edad_mesh = np.meshgrid(altura_range, edad_range)
    peso_mesh = (self.intercept + self.coefficients[0] * altura_mesh + self.coefficients[1] * edad_mesh)

    plane_color = sns.color_palette("muted", 1)[0]
    ax.plot_surface(altura_mesh, edad_mesh, peso_mesh, color=plane_color, alpha=0.4, label='Plano de Regresión')

    ax.set_xlabel('Altura (cm)', fontsize=12)
    ax.set_ylabel('Edad (años)', fontsize=12)
    ax.set_zlabel('Peso (kg)', fontsize=12)
    ax.set_title('Regresión Lineal Múltiple: Peso vs Altura y Edad', fontsize=14, pad=20)

    ax.legend()

    plt.tight_layout()

    plt.savefig(PLOT_FILE, dpi=300, bbox_inches='tight')
    plt.show()

  def run_analysis(self) -> Tuple[pd.DataFrame, dict]:
    self.load_data()
    self.preprocess_data()
    self.train_model()
    prediction_table = self.generate_predictions()
    self.plot_results()

    results = {
      'intercept': self.intercept,
      'coefficients': dict(zip(self.feature_cols, self.coefficients)),
      'prediction_table': TABLE_FILE,
      'plot_file': PLOT_FILE
    }
    return prediction_table, results

# Funcion de ejecución
def main():
  try:
    model = MultipleLinearRegressionModel(DATA_FILE, FEATURE_COLUMNS, TARGET_COLUMN)
    
    prediction_table, results = model.run_analysis()

    print("\n=== Resultados del Análisis ===")
    print(f"Intercepto: {results['intercept']:.2f}")
    print("Coeficientes:")
    for feature, coef in results['coefficients'].items():
        print(f"  {feature}: {coef:.4f}")
    print("\nTabla de Predicciones:")
    print(prediction_table.to_string(index=False))
    print(f"\nTabla guardada en: {TABLE_FILE}")
    print(f"Gráfica guardada en: {PLOT_FILE}")

  except Exception as e:
      print(f"Error durante el análisis: {e}")

if __name__ == "__main__":
  main()