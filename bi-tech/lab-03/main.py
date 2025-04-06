import pandas as pd
from sqlalchemy import create_engine
import warnings
from sqlalchemy.exc import SAWarning

warnings.filterwarnings("ignore", category=SAWarning)

engine = create_engine('mysql+pymysql://root:admin123@localhost:3306/sakila')

def load_table(table_name):
  df = pd.DataFrame(pd.read_sql(table_name, engine))
  print(f"\nTable '{table_name}' cargada correctamente:")
  print(df.head())
  return df

def customer_statistics():
  df_customer = load_table("customer")
  print("\nResumen de estadísticas del cliente:")
  print(df_customer.describe())

def payment_statistics():
  df_payment = load_table("payment")
  print("\nEstadísticas de la tabla Pagos (campo 'amount'):")
  print("Media:", df_payment['amount'].mean())
  print("Mediana:", df_payment['amount'].median())
  print("Moda:", df_payment['amount'].mode()[0])
  
def film_statistics():
  df_film = load_table("film")
  print("\nEstadísticas de la tabla Película (campo 'replacement_cost'):")
  print("Media:", df_film['replacement_cost'].mean())
  print("Mediana:", df_film['replacement_cost'].median())
  print("Moda:", df_film['replacement_cost'].mode()[0])
    
def payment_statistics_peru():
  query = """
      SELECT p.amount, ci.city, co.country
      FROM payment p
      JOIN customer c ON p.customer_id = c.customer_id
      JOIN address a ON c.address_id = a.address_id
      JOIN city ci ON a.city_id = ci.city_id
      JOIN country co ON ci.country_id = co.country_id
      WHERE co.country = 'Peru'
  """
  df_peru = pd.read_sql(query, engine)
  print(f"\nTable 'payment' cargada correctamente:")
  print(df_peru.head())
  print("\nEstadísticas de pagos (campo 'amount') de clientes de Perú:")
  print("Media:", df_peru['amount'].mean())
  print("Mediana:", df_peru['amount'].median())
  print("Moda:", df_peru['amount'].mode()[0])
  
if __name__ == "__main__":
  customer_statistics()
  payment_statistics()
  film_statistics()
  payment_statistics_peru()
  # Cerrar la conexión al motor de SQLAlchemy
  engine.dispose()