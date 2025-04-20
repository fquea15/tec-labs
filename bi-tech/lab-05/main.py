# __main__.py
# Script completo para:
# 1) Gráficos de área
# 2) Estadísticas por grupo
# 3) Imputación con KNN
# 4) Imputación multivariada (IterativeImputer)

import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.impute import KNNImputer
from sklearn.experimental import enable_iterative_imputer
from sklearn.impute import IterativeImputer

# Estilo visual
sns.set(style="whitegrid")
plt.rcParams.update({
    'figure.figsize': (14, 7),
    'axes.titlesize': 18,
    'axes.labelsize': 14,
    'xtick.labelsize': 12,
    'ytick.labelsize': 12
})

# -------------------- PUNTO 1: Gráfico de Área Mejorado --------------------

def plot_area_from_csv(filepath):
    df = pd.read_csv(filepath, sep=';')
    df['ABV'] = pd.to_numeric(df['ABV'], errors='coerce')
    df['IBU'] = pd.to_numeric(df['IBU'], errors='coerce')

    df = df.dropna(subset=['ABV', 'IBU']).sort_values(by='BeerID')

    plt.figure()
    plt.fill_between(df['BeerID'], df['ABV'], color='royalblue', alpha=0.5, label='ABV (Alcohol %)')
    plt.fill_between(df['BeerID'], df['IBU'], color='orange', alpha=0.4, label='IBU (Amargor)')

    plt.title('ABV e IBU por BeerID - Gráfico de Área')
    plt.xlabel('BeerID')
    plt.ylabel('Valor')
    plt.legend(loc='upper right')
    plt.grid(True, linestyle='--', alpha=0.6)
    plt.tight_layout()
    plt.show()

# -------------------- PUNTO 2: Estadísticas y Gráficos por Grupo --------------------
def group_stats_and_plot(filepath):
    df = pd.read_csv(filepath, sep=';')
    df['ABV'] = pd.to_numeric(df['ABV'], errors='coerce')
    df['IBU'] = pd.to_numeric(df['IBU'], errors='coerce')
    df = df.dropna(subset=['StyleID', 'ABV', 'IBU'])

    # Agrupamos por StyleID y calculamos estadísticas
    stats = df.groupby('StyleID')[['ABV', 'IBU']].agg(['mean', 'std', 'min', 'max']).reset_index()
    print("Resumen estadístico por StyleID (primeros 10):\n")
    print(stats.head(10))

    # Preparamos datos para gráfico de barras agrupadas
    mean_abv = df.groupby('StyleID')['ABV'].mean()
    mean_ibu = df.groupby('StyleID')['IBU'].mean()
    grouped_df = pd.DataFrame({
        'ABV (Promedio)': mean_abv,
        'IBU (Promedio)': mean_ibu
    }).reset_index()

    # Gráfico combinado
    plt.figure(figsize=(16, 7))
    sns.barplot(x='StyleID', y='ABV (Promedio)', data=grouped_df, color='royalblue', label='ABV (Promedio)')
    sns.barplot(x='StyleID', y='IBU (Promedio)', data=grouped_df, color='orange', alpha=0.7, label='IBU (Promedio)')

    plt.title('Promedios de ABV e IBU por Estilo de Cerveza (StyleID)')
    plt.xlabel('StyleID')
    plt.ylabel('Valor Promedio')
    plt.legend()
    plt.xticks(rotation=45)
    plt.grid(True, linestyle='--', alpha=0.5)
    plt.tight_layout()
    plt.show()

# -------------------- PUNTO 3: Imputación con KNN --------------------

def knn_imputation(filepath):
    df = pd.read_csv(filepath, sep=';')
    df_clean = df.drop(columns=[df.columns[0]])  # Eliminar columna vacía/índice si la hay

    knn_imputer = KNNImputer(n_neighbors=5)
    df_imputed = pd.DataFrame(knn_imputer.fit_transform(df_clean), columns=df_clean.columns)

    print("\n📌 Primeras filas luego de imputación con KNN:")
    print(df_imputed.head())
    return df_imputed

# -------------------- PUNTO 4: Imputación Multivariada (IterativeImputer) --------------------

def multivariate_imputation(filepath):
    df = pd.read_csv(filepath, sep=';')
    df_clean = df.drop(columns=[df.columns[0]])  # Eliminar columna vacía/índice si la hay

    iter_imputer = IterativeImputer(random_state=0)
    df_imputed = pd.DataFrame(iter_imputer.fit_transform(df_clean), columns=df_clean.columns)

    print("\n📌 Primeras filas luego de imputación multivariada:")
    print(df_imputed.head())
    return df_imputed

# -------------------- MAIN --------------------

if __name__ == "__main__":
    print("🔍 Iniciando análisis...")

    # Punto 1
    plot_area_from_csv('data/recipeData2.csv')

    # Punto 2
    group_stats_and_plot('data/recipeData2.csv')

    # Punto 3
    knn_imputation('data/housing-with-missing-1.csv')

    # Punto 4
    multivariate_imputation('data/housing-with-missing-1.csv')

    print("\n✅ Análisis completo.")
