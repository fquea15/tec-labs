import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
from sklearn import svm
from sklearn.preprocessing import StandardScaler, LabelEncoder
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score, confusion_matrix
import seaborn as sns

class SVMData10Classifier:
    def __init__(self, kernel='rbf', C=1.0):
        self.model = svm.SVC(kernel=kernel, C=C)
        self.scaler = StandardScaler()
        self.label_encoder = LabelEncoder()
        self.X = None
        self.y = None
        self.X_train = None
        self.y_train = None
        self.X_test = None
        self.y_test = None
        self.data = None

    def load_data(self, file_path):
        try:
            self.data = pd.read_excel(file_path)
            print("Datos cargados exitosamente.")
        except Exception as e:
            print(f"Error al cargar los datos: {e}")
            return False
        
        # Select features and target
        self.X = self.data[['Precio actual', 'Precio final']].values
        self.y = self.label_encoder.fit_transform(self.data['Estado'])
        
        # Normalize features
        self.X = self.scaler.fit_transform(self.X)
        return True

    def split_data(self, test_size=0.2, random_state=42):
        self.X_train, self.X_test, self.y_train, self.y_test = train_test_split(
            self.X, self.y, test_size=test_size, random_state=random_state
        )
    
    def train_model(self):
        self.model.fit(self.X_train, self.y_train)
    
    def evaluate_model(self):
        y_pred = self.model.predict(self.X_test)
        accuracy = accuracy_score(self.y_test, y_pred)
        cm = confusion_matrix(self.y_test, y_pred)
        print(f"Precisión: {accuracy:.2f}")
        print("Matriz de Confusión:")
        print(cm)
        
        # Plot confusion matrix
        plt.figure(figsize=(6, 4))
        sns.heatmap(cm, annot=True, fmt='d', cmap='Blues', 
                    xticklabels=self.label_encoder.classes_, 
                    yticklabels=self.label_encoder.classes_)
        plt.title('Matriz de Confusión para Clasificación de Data10')
        plt.xlabel('Predicción')
        plt.ylabel('Real')
        plt.show()
        
        return accuracy, cm
    
    def plot_decision_boundary(self):
        plt.figure(figsize=(10, 6))
        
        # Create a mesh grid
        h = .02  # Step size in the mesh
        x_min, x_max = self.X[:, 0].min() - 1, self.X[:, 0].max() + 1
        y_min, y_max = self.X[:, 1].min() - 1, self.X[:, 1].max() + 1
        xx, yy = np.meshgrid(np.arange(x_min, x_max, h), np.arange(y_min, y_max, h))
        
        # Predict on mesh points
        Z = self.model.predict(np.c_[xx.ravel(), yy.ravel()])
        Z = Z.reshape(xx.shape)
        
        # Plot decision boundary and points
        plt.contourf(xx, yy, Z, cmap=plt.cm.coolwarm, alpha=0.8)
        plt.scatter(self.X_train[:, 0], self.X_train[:, 1], c=self.y_train, cmap=plt.cm.coolwarm, edgecolors='k', label='Datos de Entrenamiento')
        plt.scatter(self.X_test[:, 0], self.X_test[:, 1], c=self.y_test, cmap=plt.cm.coolwarm, marker='x', label='Datos de Prueba')
        
        # Plot support vectors
        plt.scatter(self.model.support_vectors_[:, 0], self.model.support_vectors_[:, 1], 
                    s=100, facecolors='none', edgecolors='k', label='Vectores de Soporte')
        
        plt.xlabel('Precio actual (normalizado)')
        plt.ylabel('Precio final (normalizado)')
        plt.title('Límite de Decisión SVM con Data10.xlsx')
        plt.legend()
        plt.show()

def main():
    svm_classifier = SVMData10Classifier(kernel='rbf', C=1.0)
    if svm_classifier.load_data('data/Data10.xlsx'):
        svm_classifier.split_data(test_size=0.2)
        svm_classifier.train_model()
        accuracy, cm = svm_classifier.evaluate_model()
        svm_classifier.plot_decision_boundary()
        print("\nClasificación de Data10 Completada.")
        print(f"Precisión Final: {accuracy:.2f}")

if __name__ == "__main__":
    main()