import pandas as pd
import numpy as np

# Ruta del archivo Excel - Ajusta esta ruta según la ubicación de tu archivo
excel_file = "data/BI_Clientes06.xlsx"

def load_data(file_path):
    """
    Carga datos desde un archivo Excel
    """
    try:
        df = pd.read_excel(file_path)
        print("Datos originales cargados correctamente")
        print("Forma de los datos:", df.shape)
        print("Muestra de los datos originales:")
        print(df.head())
        return df
    except Exception as e:
        print(f"Error al leer el archivo Excel: {e}")
        return None

def analyze_null_values(df):
    """
    Analiza los valores nulos en el dataframe
    """
    print("\nValores nulos en cada columna:")
    print(df.isnull().sum())
    return df.isnull().sum()

# 1. Lea las columnas CustomerKey, FirstName y TotalChildren del
#    archivo BI_Clientes06.xlsx, elimine los registros perdidos (nulos) y exporte la
#    data limpia a otro archivo de Excel
def task1(df):
    print("\n--- Tarea 1: Eliminar registros perdidos en columnas específicas ---")
    # Seleccionar las columnas requeridas
    task1_df = df[['CustomerKey', 'FirstName', 'TotalChildren']].copy()

    # Eliminar las filas que tienen algún valor nulo en las columnas seleccionadas
    task1_df_clean = task1_df.dropna()

    print(f"Forma original: {task1_df.shape}, Forma después de limpieza: {task1_df_clean.shape}")
    print("Filas eliminadas:", task1_df.shape[0] - task1_df_clean.shape[0])

    # Exportar a Excel
    task1_df_clean.to_excel("Task1_Clean_Data.xlsx", index=False)
    print("Datos limpios exportados a 'Task1_Clean_Data.xlsx'")

    print("Muestra de datos limpios:")
    print(task1_df_clean.head())

    return task1_df_clean

# 2. Lea las columnas CustomerKey, FirstName y TotalChildren del
#    archivo BI_Clientes06.xlsx, elimine las filas de todos los datos perdidos
#    ubicados en una sola columna y exporte la data limpia a otro archivo de Excel
def task2(df):
    print("\n--- Tarea 2: Eliminar filas con valores perdidos en una columna ---")
    # Seleccionar las columnas requeridas
    task2_df = df[['CustomerKey', 'FirstName', 'TotalChildren']].copy()

    # Mostrar valores nulos antes de la limpieza
    print("Valores nulos antes de la limpieza:")
    print(task2_df.isnull().sum())

    # Eliminar las filas donde la columna 'TotalChildren' tiene valores nulos
    task2_df_clean = task2_df.dropna(subset=['TotalChildren'])

    print(f"Forma original: {task2_df.shape}, Forma después de limpieza: {task2_df_clean.shape}")
    print("Filas eliminadas:", task2_df.shape[0] - task2_df_clean.shape[0])

    # Exportar a Excel
    task2_df_clean.to_excel("Task2_Clean_Data.xlsx", index=False)
    print("Datos limpios exportados a 'Task2_Clean_Data.xlsx'")

    print("Muestra de datos limpios:")
    print(task2_df_clean.head())

    return task2_df_clean

# 3. Lea el archivo BI_Clientes06.xlsx. Calcule la media del campo
#    TotalChildren y reemplace este dato a los registros nulos. Exporte la data
#    limpia a otro Excel
def task3(df):
    print("\n--- Tarea 3: Reemplazar valores nulos con la media ---")
    # Crear una copia del dataframe
    task3_df = df.copy()

    # Calcular la media de la columna 'TotalChildren'
    mean_total_children = task3_df['TotalChildren'].mean()
    print(f"Media de TotalChildren: {mean_total_children}")

    # Reemplazar los valores nulos en 'TotalChildren' con la media
    task3_df['TotalChildren'].fillna(mean_total_children, inplace=True)

    # Verificar que no queden valores nulos en 'TotalChildren'
    print("Valores nulos en TotalChildren después del reemplazo:",
          task3_df['TotalChildren'].isnull().sum())

    # Exportar a Excel
    task3_df.to_excel("Task3_Clean_Data.xlsx", index=False)
    print("Datos limpios exportados a 'Task3_Clean_Data.xlsx'")

    print("Muestra de datos limpios:")
    print(task3_df[['CustomerKey', 'FirstName', 'TotalChildren']].head())

    return task3_df

# 4. Lea el archivo BI_Clientes06.xlsx. Resumir los valores perdidos
#    totales, obtener los valores duplicados, imprimir la cantidad de veces que un
#    cliente aparece en la data y elimine los valores duplicados. Exporte la data
#    limpia a otro Excel
def task4(df):
    print("\n--- Tarea 4: Análisis de valores perdidos y duplicados ---")

    # Resumir los valores perdidos totales por columna
    print("\nTotal de valores perdidos por columna:")
    print(df.isnull().sum())
    print(f"Total de valores perdidos en el dataset: {df.isnull().sum().sum()}")

    # Identificar filas duplicadas
    duplicate_rows = df.duplicated()
    print(f"\nNúmero de filas duplicadas: {duplicate_rows.sum()}")

    # Contar las ocurrencias de cada 'CustomerKey'
    customer_counts = df['CustomerKey'].value_counts()
    print("\nNúmero de apariciones para cada cliente:")
    for customer, count in customer_counts.items():
        if count > 1:  # Mostrar solo los que aparecen más de una vez
            print(f"Cliente {customer}: {count} veces")

    # Eliminar filas duplicadas
    task4_df_clean = df.drop_duplicates()
    print(f"\nForma original: {df.shape}, Después de eliminar duplicados: {task4_df_clean.shape}")

    # Exportar a Excel
    task4_df_clean.to_excel("Task4_Clean_Data.xlsx", index=False)
    print("Datos limpios exportados a 'Task4_Clean_Data.xlsx'")

    print("Muestra de datos limpios (después de eliminar duplicados):")
    print(task4_df_clean.head())

    return task4_df_clean

def main():
    """
    Función principal para ejecutar todas las tareas
    """
    # Load the data
    df = load_data(excel_file)
    if df is None:
        return

    # Analyze null values in the original dataframe
    analyze_null_values(df)

    # Execute all tasks
    print("\nEjecutando todas las tareas...")
    task1_result = task1(df.copy()) # Pass a copy to avoid modifying the original DataFrame
    task2_result = task2(df.copy()) # Pass a copy to avoid modifying the original DataFrame
    task3_result = task3(df.copy()) # Pass a copy to avoid modifying the original DataFrame
    task4_result = task4(df.copy()) # Pass a copy to avoid modifying the original DataFrame
    print("\n¡Todas las tareas completadas con éxito!")

if __name__ == "__main__":
    main()