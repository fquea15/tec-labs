import pandas as pd
import numpy as np

# 1.	Empleando Series: Arreglos e índices (2 modalidades)
# a.	Genere un arreglo de 6 elementos de texto renombrando sus índices e imprima los datos
series_txt = pd.Series(['A', 'B', 'C', 'D', 'E', 'F'], index=['item1', 'item2', 'item3', 'item4', 'item5', 'item6'])
print("Arreglo de 6 elementos de texto con índices personalizados:")
print(series_txt)

# # b.	Genere un arreglo de 3 elementos de texto con sus 3 valores numéricos e imprima los datos
series_num_txt = pd.Series([1, 2, 3], index=["item1", "item2", "item3"])
print("\nArreglo de 3 elementos de texto con valores numéricos:")
print(series_num_txt)

# c.	Genere un arreglo de 10 números aleatorios e imprima todos los números, los primeros 5 números, 
# los últimos 5, los 2 primeros y los 2 últimos.
series_random = pd.Series((np.random.rand(10) * 100).round().astype(int))
print("\nArreglo de 10 números aleatorios:")
print(series_random)
print("\nPrimeros 5 números:")
print(series_random.head(5))
print("\nÚltimos 5 números:")
print(series_random.tail(5))
print("\nPrimeros 2 números:")
print(series_random.head(2))
print("\nÚltimos 2 números:")
print(series_random.tail(2))

# 2.	Empleando DataFrame: Arreglos
# a.	Genere un arreglo de 6 elementos de texto con sus respectivos valores numéricos e 
# imprima los datos
df = pd.DataFrame({
    'Texto': ['A', 'B', 'C', 'D', 'E', 'F'],
    'Numerico': [10, 20, 30, 40, 50, 60]
})
print("\nArreglo de 6 elementos de texto con sus respectivos valores numéricos:")
print(df)

# b.	Cambie el orden de las columnas
df_reordered = df[['Numerico', 'Texto']]
print("\nArreglo con el orden de las columnas cambiado:") 
print(df_reordered)

# 3.	Lea el archivo Ventas.csv e imprima los datos
ventas_csv = pd.read_csv('data/Ventas.csv')
print("\nDatos del archivo Ventas.csv:")
print(ventas_csv)

# 4.	Empleando DataFrame: Genere 2 arreglos de 6 elementos de texto con sus respectivos 
# valores numéricos e imprima los datos, agregue el arreglo 2 sobre el arreglo 1 e imprima los datos
df1 = pd.DataFrame({
    'Texto1': ['A', 'B', 'C', 'D', 'E', 'F'],
    'Numerico1': [10, 20, 30, 40, 50, 60]
})
df2 = pd.DataFrame({
    'Texto2': ['G', 'H', 'I', 'J', 'K', 'L'],
    'Numerico2': [70, 80, 90, 100, 110, 120]
})

# Agregando df2 sobre df1
df_combined = pd.concat([df1, df2], axis=1, ignore_index=True)
print("\nArreglo 1:")
print(df1)
print("\nArreglo 2:")
print(df2)
print("\nArreglo combinado:")
print(df_combined) 

# 5.	Empleando DataFrame: 
# a.	Genere un arreglo con las ventas del primer trimestre del año e imprima los datos
ventas_trimestre = pd.DataFrame({
    'Mes': ['Enero', 'Febrero', 'Marzo'],
    'Ventas': [100, 150, 200]
})
print("\nArreglo con las ventas del primer trimestre del año:")
print(ventas_trimestre)

# b.	Imprima el numero de registros y el número de campos
print("\nNúmero de registros:", len(ventas_trimestre))
print("Número de campos:", len(ventas_trimestre.columns))

# c.	Genere un arreglo numérico con las ventas del primer trimestre del año, imprima la media, 
# la correlación, el valor más bajo, el valor más alto, la mediana, la desviación estándar, 
# solo la primera columna del dataset, las 2 primeras columnas, primera fila y última columna 
# y los valores de la primera fila
ventas_numericos = ventas_trimestre['Ventas']
print("\nVentas del primer trimestre del año:")
print(ventas_numericos)
print("\nMedia:", ventas_numericos.mean())
print("Correlación:", ventas_numericos.corr(ventas_numericos))
print("Valor más bajo:", ventas_numericos.min())
print("Valor más alto:", ventas_numericos.max())
print("Mediana:", ventas_numericos.median())
print("Desviación estándar:", ventas_numericos.std())
print("\nPrimera columna del dataset:")
print(ventas_trimestre.iloc[:, 0])
print("\nDos primeras columnas:")
print(ventas_trimestre.iloc[:, :2])
print("\nPrimera fila:")
print(ventas_trimestre.iloc[0])
print("\nÚltima columna:")
print(ventas_trimestre.iloc[:, -1])
print("\nValores de la primera fila:")
print(ventas_trimestre.iloc[0, :])


# 6.	Lea el archivo de Excel Ventas02.xlsx ignorando la primera 
# fila (encabezado) e imprima los datos
df_excel = pd.read_excel('data/Ventas02.xlsx', header=1)
print("\nDatos del archivo Ventas02.xlsx:")
print(df_excel)
