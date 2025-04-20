import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns

sns.set_style("whitegrid")
plt.rcParams.update({
    'axes.facecolor': '#21252B',
    'figure.facecolor': '#21252B',
    'axes.edgecolor': '#ABB2BF',
    'axes.labelcolor': '#ABB2BF',
    'xtick.color': '#ABB2BF',
    'ytick.color': '#ABB2BF',
    'text.color': '#ABB2BF',
    'axes.titleweight': 'bold',
    'axes.titlesize': 14,
    'axes.titlepad': 20,
})

def plot_gdp_country_over_time(file_path, country_name="Albania"):
  try:
    df = pd.read_csv(file_path)

    df_country = df[
        (df["Country"] == country_name) &
        (df["Subject Descriptor"] == "Gross domestic product, current prices")
    ]

    year_cols = [col for col in df.columns if col.isdigit()]
    df_years = df_country[year_cols].T.reset_index()
    df_years.columns = ['Año', 'PIB (billones USD)']
    df_years['Año'] = df_years['Año'].astype(int)
    df_years['PIB (billones USD)'] = pd.to_numeric(df_years['PIB (billones USD)'], errors='coerce')

    plt.figure(figsize=(12, 6))
    sns.lineplot(data=df_years, x='Año', y='PIB (billones USD)', marker='o', color="#61AFEF")
    plt.title(f'Evolución del PIB en {country_name} (2000–2024)')
    plt.ylabel('PIB (billones de USD)')
    plt.xlabel('Año')
    plt.grid(True, linestyle='--', alpha=0.5)
    plt.tight_layout()
    plt.show()

  except Exception as e:
    print(f"Error: {e}")

if __name__ == "__main__":
  plot_gdp_country_over_time("data/WEO_Data.csv", country_name="Albania")
