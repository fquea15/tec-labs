import numpy as np
import matplotlib.pyplot as plt
from sklearn import svm
from sklearn.datasets import make_blobs
from sklearn.metrics import accuracy_score, confusion_matrix
import seaborn as sns

class SVMSyntheticClassifier:
    def __init__(self, kernel='rbf', C=1.0):
        self.model = svm.SVC(kernel=kernel, C=C)
        self.X = None
        self.y = None
        self.X_train = None
        self.y_train = None
        self.X_test = None
        self.y_test = None

    def generate_data(self, n_samples=100, centers=2, random_state=42):
        self.X, self.y = make_blobs(n_samples=n_samples, centers=centers, cluster_std=1.5, random_state=random_state)
        
    def split_data(self, test_size=0.2, random_state=42):
        from sklearn.model_selection import train_test_split
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
                    xticklabels=['Clase 0', 'Clase 1'], 
                    yticklabels=['Clase 0', 'Clase 1'])
        plt.title('Matriz de Confusión para Datos Sintéticos')
        plt.xlabel('Predicción')
        plt.ylabel('Real')
        plt.show()
        
        return accuracy, cm
    
    def plot_decision_boundary(self):
        """Plot the decision boundary and support vectors."""
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
        
        plt.xlabel('Característica 1')
        plt.ylabel('Característica 2')
        plt.title('Límite de Decisión SVM con Datos Sintéticos')
        plt.legend()
        plt.show()

def main():
    svm_classifier = SVMSyntheticClassifier(kernel='rbf', C=1.0)
    svm_classifier.generate_data(n_samples=100, centers=2)
    svm_classifier.split_data(test_size=0.2)
    svm_classifier.train_model()
    accuracy, cm = svm_classifier.evaluate_model()
    svm_classifier.plot_decision_boundary()
    print("\nClasificación de Datos Sintéticos Completada.")
    print(f"Precisión Final: {accuracy:.2f}")

if __name__ == "__main__":
    main()