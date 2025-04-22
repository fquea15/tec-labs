import pandas as pd
import numpy as np
from sklearn.impute import KNNImputer, SimpleImputer
from sklearn.experimental import enable_iterative_imputer
from sklearn.impute import IterativeImputer
import matplotlib.pyplot as plt
import seaborn as sns
from matplotlib.lines import Line2D


def load_data(filepath):
    df = pd.read_csv(filepath, sep=';')
    print(f"Forma del dataset original: {df.shape}")
    print("\nValores faltantes por columna:")
    print(df.isnull().sum())
    return df

def impute_with_knn(df, n_neighbors=5):
    df_knn = df.copy()
    
    if '' in df_knn.columns:
        index_col = df_knn['']
        df_knn = df_knn.drop(columns=[''])
    else:
        index_col = None
    
    imputer = KNNImputer(n_neighbors=n_neighbors)
    
    imputed_array = imputer.fit_transform(df_knn)
    
    df_imputed = pd.DataFrame(imputed_array, columns=df_knn.columns)
    
    if index_col is not None:
        df_imputed.insert(0, '', index_col)
    
    print(f"\nForma del dataset después de imputación KNN: {df_imputed.shape}")
    print("Valores faltantes después de imputación KNN:")
    print(df_imputed.isnull().sum())
    
    return df_imputed

def impute_with_multivariate(df):
    df_multi = df.copy()
    
    if '' in df_multi.columns:
        index_col = df_multi['']
        df_multi = df_multi.drop(columns=[''])
    else:
        index_col = None
    
    imputer = IterativeImputer(max_iter=10, random_state=42)
    
    imputed_array = imputer.fit_transform(df_multi)
    
    df_imputed = pd.DataFrame(imputed_array, columns=df_multi.columns)
    
    if index_col is not None:
        df_imputed.insert(0, '', index_col)
    
    print(f"\nForma del dataset después de imputación Multivariada: {df_imputed.shape}")
    print("Valores faltantes después de imputación Multivariada:")
    print(df_imputed.isnull().sum())
    
    return df_imputed

def visualize_imputation_comparison(original_df, knn_df, multi_df, columns_to_plot=None):
    if columns_to_plot is None:
        columns_with_na = original_df.columns[original_df.isnull().any()].tolist()
        if '' in columns_with_na:
            columns_with_na.remove('')
        columns_to_plot = columns_with_na
    
    n_cols = len(columns_to_plot)
    fig, axes = plt.subplots(n_cols, 1, figsize=(12, 4*n_cols))
    
    if n_cols == 1:
        axes = [axes]
    
    for i, column in enumerate(columns_to_plot):
        ax = axes[i]
        
        original_values = original_df[column].dropna()
        
        mask_na = original_df[column].isna()
        knn_imputed_values = knn_df.loc[mask_na, column]
        multi_imputed_values = multi_df.loc[mask_na, column]
        
        ax.hist(original_values, bins=20, alpha=0.5, label='Valores Originales')
        
        for value in knn_imputed_values:
            ax.axvline(x=value, color='red', linestyle='--', alpha=0.7)
        
        for value in multi_imputed_values:
            ax.axvline(x=value, color='green', linestyle='-.', alpha=0.7)
        
        ax.set_title(f'Distribución de {column} con valores imputados marcados')
        ax.set_xlabel(column)
        ax.set_ylabel('Frecuencia')
        
        custom_lines = [
            Line2D([0], [0], color='blue', lw=4, alpha=0.5),
            Line2D([0], [0], color='red', linestyle='--', lw=2),
            Line2D([0], [0], color='green', linestyle='-.', lw=2)
        ]
        ax.legend(custom_lines, ['Valores Originales', 'Imputados KNN', 'Imputados Multivariados'])
    
    plt.tight_layout()
    plt.savefig('comparacion_imputaciones.png', dpi=300)
    plt.show()

def main():
    file_path = 'data/housing-with-missing.csv'
    df = load_data(file_path)
    
    print("\n=== IMPUTACIÓN CON KNN ===")
    df_knn = impute_with_knn(df)
    
    df_knn.to_csv('housing_imputed_knn.csv', index=False)
    print("Datos imputados con KNN guardados en 'housing_imputed_knn.csv'")
    
    print("\n=== IMPUTACIÓN MULTIVARIADA ===")
    df_multi = impute_with_multivariate(df)
    
    df_multi.to_csv('housing_imputed_multivariate.csv', index=False)
    print("Datos imputados con método multivariado guardados en 'housing_imputed_multivariate.csv'")
    
    visualize_imputation_comparison(df, df_knn, df_multi)

if __name__ == "__main__":
    main()


def show_group_statistics(file_path):
    df = pd.read_csv(file_path, sep=';')
    df['ABV'] = pd.to_numeric(df['ABV'], errors='coerce')
    df = df.dropna(subset=['ABV', 'BrewMethod'])
    df = df[df['ABV'] <= 20]
    
    stats = df.groupby('BrewMethod')['ABV'].agg(['mean', 'median', 'std', 'count']).round(2).reset_index()
    
    sns.set_theme(style="whitegrid", font_scale=1.2)
    
    plt.figure(figsize=(12, 8))
    
    sns.boxplot(x='BrewMethod', y='ABV', data=df, 
                    palette="Set2", width=0.6, linewidth=1.5,
                    fliersize=3, flierprops={'marker':'o', 'markerfacecolor':'gray', 'alpha':0.5})
    
    plt.title("Distribución del ABV (%) según el Método de Elaboración", fontsize=16, fontweight='bold')
    plt.suptitle("Cada caja representa el rango intercuartílico (IQR) con la mediana", fontsize=11, y=0.95)
    
    plt.xlabel("Método de Elaboración", fontsize=14, labelpad=10)
    plt.ylabel("Alcohol por Volumen (ABV %)", fontsize=14, labelpad=10)
    
    for i, method in enumerate(stats['BrewMethod']):
        row = stats[stats['BrewMethod'] == method].iloc[0]
        mean_val = row['mean']
        median_val = row['median']
        n_val = int(row['count'])
        
        plt.text(i, median_val - 1.2, 
                f"Media: {mean_val:.2f}%\nMediana: {median_val:.2f}%\nn={n_val}", 
                horizontalalignment='center', color='black', fontsize=10, fontweight='bold',
                bbox=dict(facecolor='white', alpha=0.7, edgecolor='none', boxstyle='round,pad=0.3'))
    
    plt.axhline(y=df['ABV'].mean(), color='red', linestyle='--', alpha=0.7, linewidth=1)
    plt.text(len(stats['BrewMethod'])-0.8, df['ABV'].mean()+0.2, 
             f"Media global: {df['ABV'].mean():.2f}%", 
             color='red', fontsize=10)
    
    plt.grid(axis='y', linestyle='--', alpha=0.7)
    plt.xticks(fontweight='bold')
    
    handles, labels = [], []
    handles.append(plt.Line2D([0], [0], color='red', linestyle='--', lw=1))
    labels.append("Media global")
    
    plt.legend(handles, labels, loc='upper right')
    
    plt.tight_layout()
    
    plt.savefig('abv_distribucion.png', dpi=150)
    
    plt.show()
    
def plot_area_chart_from_csv(file_path):
    sns.set(style='whitegrid')
    plt.figure(figsize=(14, 6))

    df = pd.read_csv(file_path, sep=';')
    df['BeerID'] = pd.to_numeric(df['BeerID'], errors='coerce')
    df['ABV'] = pd.to_numeric(df['ABV'], errors='coerce')
    df = df.dropna(subset=['BeerID', 'ABV', 'BrewMethod'])

    df = df[df['ABV'] <= 20]

    df['BeerID_bin'] = pd.cut(df['BeerID'], bins=50)
    grouped = df.groupby(['BeerID_bin', 'BrewMethod']).agg(mean_abv=('ABV', 'mean')).reset_index()

    grouped['BeerID_mid'] = grouped['BeerID_bin'].apply(lambda x: x.mid)

    methods = grouped['BrewMethod'].unique()
    palette = sns.color_palette("husl", len(methods))

    for i, method in enumerate(methods):
        subset = grouped[grouped['BrewMethod'] == method]
        sns.lineplot(x='BeerID_mid', y='mean_abv', data=subset, label=method, color=palette[i])
        plt.fill_between(subset['BeerID_mid'], subset['mean_abv'], alpha=0.2, color=palette[i])

    plt.title('ABV Promedio por Rango de BeerID y Método de Elaboración', fontsize=16, fontweight='bold')
    plt.xlabel('BeerID (Promedio por Rango)', fontsize=12)
    plt.ylabel('ABV Promedio (%)', fontsize=12)
    plt.legend(title='Método de Elaboración', title_fontsize='13', fontsize=11)
    plt.grid(True, linestyle='--', alpha=0.6)
    plt.tight_layout()
    plt.xticks(rotation=45)
    plt.yticks(fontsize=10)
    plt.show()
    
