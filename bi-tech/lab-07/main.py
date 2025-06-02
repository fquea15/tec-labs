import os
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression
from matplotlib.backends.backend_pdf import PdfPages
import seaborn as sns

class RegresionAnalysis:
  def __init__(self, file_path="data/ingreso.csv"):
    self.file_path = file_path
    self.data = None
    self.model = None
    self.x = None # horas de trabajo semanal
    self.y = None # Ingreso mensual
    self.intercept = None # Interseccion
    self.slope = None # Pendiente
    self.load_data()
    
  # funcion para cargar la data
  def load_data(self):
    '''Load and validate the dataset.'''
    if not os.path.exists(self.file_path):
      raise FileNotFoundError(f'EL archivo {self.file_path} no se encuentra')
    self.data = pd.read_csv(self.file_path)
    required_columns = ['horas', 'ingreso']
    if not all(col in self.data.columns for col in required_columns):
      raise ValueError(f"El dataset debe contener las columnas: {required_columns}")
    if not np.issubdtype(self.data['horas'].dtype, np.number) or not np.issubdtype(self.data['ingreso'], np.number):
      raise ValueError("Las columnas 'horas' e 'ingreso' deben ser numéricas")
    self.x = self.data[['horas']].values
    self.y = self.data[['ingreso']].values
  
  # <==ingreso mensual depende de las horas trabajadas ==>
  # x -> valor independiente -> horas trabajadas
  # y -> valor dependiente -> ingreso mensual
  # formula y = mx + b
  def fit_linear_regresion(self):
    self.model = LinearRegression() #modelo de regresion
    self.model.fit(self.x, self.y)# x, y # ajustar
    self.intercept = self.model.intercept_ # intercepto -> b
    self.slope = self.model.coef_[0] # pendiente -> m
    
    
  def plot_regression(self,output_folder = 'output', output_file='regression_plot.pdf'):
    sns.set_theme(style="whitegrid")
    
    plt.figure(figsize=(10,6))
    plt.scatter(self.x, self.y, color='blue', alpha=0.5, label='Puntos de datos')
    plt.plot(self.x, self.model.predict(self.x), color='red', linewidth=2, label='Línea de regresión')
    plt.xlabel('Horas de trabajo semanal', fontsize=12)
    plt.ylabel('Ingreso mensual', fontsize=12)
    plt.title('Horas vs Ingreso con Regresion Lineal', fontsize=14)
    
    eq = f'y = {self.slope[0]:.2f}x + {self.intercept[0]:.2f}'
    plt.text(0.05, 0.95, eq, transform=plt.gca().transAxes, fontsize=10, bbox=dict(facecolor='white', alpha=0.8))
    
    plt.legend()
    plt.grid(True, linestyle='--', alpha=0.7)
    
    full_path = os.path.join(output_folder, output_file)
    with PdfPages(full_path) as pdf:
      if not os.path.exists(output_folder):
        os.mkdir(output_folder)
      pdf.savefig(bbox_inches='tight')
      
    plt.close()
    
  def run_analysis(self):
    try:
      self.fit_linear_regresion()
      self.plot_regression()
      print(f"Intercepto de la regresión lineal: {self.intercept[0]:.2f}")
      print(f"Pendiente de la regresión lineal: {self.slope[0]:.2f}")
            
    except Exception as e:
      print(f'Error: {e}')
    
if __name__ == "__main__":
  analysis = RegresionAnalysis()
  analysis.run_analysis()