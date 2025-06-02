import pandas as pd
import numpy as np
from sklearn.linear_model import LinearRegression
import matplotlib.pyplot as plt
from matplotlib.backends.backend_pdf import PdfPages
import seaborn as sns
import os
from typing import Tuple
import warnings

warnings.filterwarnings("ignore")


class LinearRegressionAnalyzer:
  def __init__(self, file_path: str = 'data/BI_Alumnos07.xlsx') -> None:
    self.file_path = file_path
    self.data = None
    self.model = None
    self.coefficient = 0.0
    self.intercept = 0.0
    self._load_data()

  def _load_data(self) -> None:
    if not os.path.exists(self.file_path):
      raise FileNotFoundError(f"El archivo {self.file_path} no se encuentra.")
    self.data = pd.read_excel(self.file_path)
    required_columns = ['Altura', 'Peso']
    if not all(col in self.data.columns for col in required_columns):
      raise ValueError(f"El dataset debe contener las columnas: {required_columns}")
    if not np.issubdtype(self.data['Altura'].dtype, np.number) or not np.issubdtype(self.data['Peso'].dtype, np.number):
      raise ValueError("Las columnas 'Altura' y 'Peso' deben ser numéricas.")

  def fit_model(self) -> None:
    x = self.data[['Altura']]
    y = self.data['Peso']
    self.model = LinearRegression()
    self.model.fit(x, y)
    self.coefficient = float(self.model.coef_[0])
    self.intercept = float(self.model.intercept_)

  def generate_predictions(self, min_height: float, max_height: float, steps: int = 10) -> pd.DataFrame:
    heights = np.linspace(min_height, max_height, steps).reshape(-1, 1)
    predictions = self.model.predict(heights)
    return pd.DataFrame({'Altura': heights.flatten(), 'Peso_Predicho': predictions})

  def plot_regression(self, output_file: str = 'regression_plot.pdf') -> None:
    sns.set_theme(style="whitegrid")
    plt.figure(figsize=(10, 6))
    plt.scatter(self.data['Altura'], self.data['Peso'], color='blue', alpha=0.5, label='Datos reales')
    plt.plot(self.data['Altura'], self.model.predict(self.data[['Altura']]), color='red', linewidth=2, label='Línea de regresión')
    plt.xlabel('Altura (cm)', fontsize=12)
    plt.ylabel('Peso (kg)', fontsize=12)
    plt.title('Regresión Lineal: Altura vs Peso', fontsize=14)
    equation = f'Peso = {self.coefficient:.2f} * Altura + {self.intercept:.2f}'
    plt.text(0.05, 0.95, equation, transform=plt.gca().transAxes, fontsize=10, bbox=dict(facecolor='white', alpha=0.8))
    plt.legend()
    plt.grid(True, linestyle='--', alpha=0.7)
    with PdfPages(output_file) as pdf:
        pdf.savefig(bbox_inches='tight')
    plt.close()

  def run_analysis(self) -> Tuple[float, float, pd.DataFrame]:
    try:
      self.fit_model()
      min_height = self.data['Altura'].min()
      max_height = self.data['Altura'].max()
      predictions = self.generate_predictions(min_height, max_height)
      self.plot_regression()
      print(f"Coeficiente (pendiente): {self.coefficient:.2f}")
      print(f"Intercepto: {self.intercept:.2f}")
      print("\nCuadro de Predicción:")
      print(predictions)
      print("\nDatos originales (primeras filas):")
      print(self.data.head())
      return self.coefficient, self.intercept, predictions
    except Exception as e:
      raise Exception(f"Error durante el análisis: {e}")

if __name__ == "__main__":
  try:
    analyzer = LinearRegressionAnalyzer()
    analyzer.run_analysis()
  except Exception as e:
    print(f"Error: {e}")