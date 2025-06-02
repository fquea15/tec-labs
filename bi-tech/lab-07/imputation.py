import pandas as pd
import numpy as np
from sklearn.impute import SimpleImputer

class DataImputation:
  def __init__(self):
    self.data = pd.DataFrame({
      'x1': [1, 2, 3, 4, 5, 6],
      'x2': [1, 2, 3, 1, 2, 7],
      'x3': [32, 30, np.nan, np.nan, 27, 44],
      'x4': [102, 121, 343, np.nan, 121, 125],
    })
    self.data_imputed = None
    
  def impute_missing_data(self, columns=['x3', 'x4']):
    print('DataFrame original:')
    print(self.data)
    print('\nValores faltantes antes de imputación:')
    print(self.data[columns].isnull().sum())
    
    self.data_imputed = self.data.copy()
    imputer = SimpleImputer(strategy='mean')
    self.data_imputed[columns] = imputer.fit_transform(self.data_imputed[columns])
    
    print('\nDataFrame después de imputar valores faltantes:')
    print(self.data_imputed)
    print(self.data_imputed[columns].isnull().sum())
    
  def run_imputation(self):
    try:
      self.impute_missing_data()
    except Exception as e:
      print(f'Error: {e}')
      
if __name__ == "__main__":
  imputer = DataImputation()
  imputer.run_imputation()