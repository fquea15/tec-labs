import pandas as pd

df = pd.read_csv('data/data_wine.csv', sep=';')

print("Columnas disponibles:")
print(df.columns.tolist())

columna = input("\nIngresa el nombre de la columna que deseas analizar: ")

if columna not in df.columns:
    print(" La columna ingresada no existe en el archivo.")
else:
    print(f"\n Análisis estadístico para la columna: {columna}\n") 

    # a.	Obtener la sumatoria de los valores de una determinada columna y/o campo
    suma = df[columna].sum()
    print(f"a. Suma: {suma}")

    # b.	Obtener el número de elementos
    num_elementos = df[columna].count()
    print(f"b. Número de elementos: {num_elementos}")
    
    # c.	Obtener la media
    media = df[columna].mean()
    print(f"c. Media: {media}")
    
    # d.	Obtener la media redondeada a 2 decimales
    media_redondeada = round(media, 2)
    print(f"d. Media redondeada: {media_redondeada}")
    
    # e.	Obtener la mediana
    mediana = df[columna].median()
    print(f"e. Mediana: {mediana}")
    
    # f.	Obtener la moda
    moda = df[columna].mode()
    if not moda.empty:
        moda = moda[0]
    print(f"f. Moda: {moda}")
    
    # g.	Obtener los percentiles
    percentiles = df[columna].quantile([0.25, 0.5, 0.75])
    print(f"g. Percentiles (25, 50, 75):")
    print(f"--> Percentil 25%: {percentiles[0.25]}")
    print(f"--> Percentil 50%: {percentiles[0.5]}")
    print(f"--> Percentil 75%: {percentiles[0.75]}")
    
    # h.	Obtener la varianza
    varianza = df[columna].var()
    print(f"h. Varianza: {varianza}")
