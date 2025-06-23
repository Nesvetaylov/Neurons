
data = [
    [60, 79, 60, 72, 63, 1, 0, 1.00, 1, 1],  # Варданян
    [60, 61, 30, 5, 17, 0, 0, 0.00, 1, 0],   # Горбунов
    [60, 61, 30, 66, 58, 0, 0, 0.00, 0, 0],  # Гуменюк
    [85, 78, 72, 70, 85, 1, 1, 1.25, 1, 1],  # Егоров
    [65, 78, 60, 67, 65, 1, 1, 1.00, 0, 1],  # Захарова
    [60, 78, 77, 81, 60, 1, 1, 1.25, 0, 1],  # Иванова
    [55, 79, 56, 69, 72, 0, 0, 0.00, 0, 1],  # Ишонина
    [55, 56, 50, 56, 60, 0, 0, 0.00, 1, 0],  # Климчук
    [55, 60, 21, 64, 50, 0, 0, 0.00, 1, 0],  # Лисовский
    [60, 56, 30, 16, 17, 0, 0, 0.00, 1, 0],  # Нетреба
    [85, 89, 85, 92, 85, 1, 1, 1.75, 0, 1],  # Остапова
    [60, 88, 76, 66, 60, 1, 1, 1.25, 0, 1],  # Пашкова
    [55, 64, 0, 9, 50, 0, 0, 0.00, 1, 0],    # Попов
    [80, 83, 62, 72, 72, 1, 1, 1.25, 0, 1],  # Сазон
    [55, 10, 3, 8, 50, 0, 0, 0.00, 1, 0],    # Степоненко
    [60, 67, 57, 64, 50, 0, 0, 0.00, 0, 1],  # Терентьева
    [75, 98, 86, 82, 85, 1, 1, 1.50, 1, 1],  # Титов
    [85, 85, 81, 85, 72, 1, 1, 1.25, 0, 1],  # Чернова
    [80, 56, 50, 69, 50, 0, 0, 0.00, 1, 1],  # Четкин
    [55, 60, 30, 8, 60, 0, 0, 0.00, 1, 0],   # Шевченко
]

def normalize(data):
    n_features = 7
    mins = [float('inf')] * n_features
    maxs = [float('-inf')] * n_features
    
    for row in data:
        for i in range(n_features):
            val = row[i]
            if val < mins[i]:
                mins[i] = val
            if val > maxs[i]:
                maxs[i] = val
    
    normalized_data = []
    for row in data:
        norm_row = []
        for i in range(n_features):
            if maxs[i] == mins[i]:
                norm_val = 0.0
            else:
                norm_val = (row[i] - mins[i]) / (maxs[i] - mins[i])
            norm_row.append(norm_val)
        norm_row.extend(row[n_features:])
        normalized_data.append(norm_row)
    return normalized_data

data_norm = normalize(data)

n_neurons = 6            
n_features = 7           
learning_rate_0 = 0.6    
n_epochs = 6             


import random
random.seed(42)  

weights = []
for _ in range(n_neurons):
    neuron_weights = []
    for f in range(n_features):
        neuron_weights.append(random.uniform(0, 1))
    weights.append(neuron_weights)

def euclidean_distance(vec1, vec2):
    s = 0.0
    for i in range(len(vec1)):
        s += (vec1[i] - vec2[i]) ** 2
    return s ** 0.5

for epoch in range(n_epochs):
    lr = learning_rate_0 * (1 - epoch / n_epochs) 
    for obj in data_norm:
        input_vec = obj[:n_features]
        dists = []
        for w in weights:
            dist = euclidean_distance(input_vec, w)
            dists.append(dist)
        bmu_index = dists.index(min(dists))
        for i in range(n_features):
            weights[bmu_index][i] += lr * (input_vec[i] - weights[bmu_index][i])
    
    print(f"Epoch {epoch+1}/{n_epochs}, learning rate = {lr:.3f}")

clusters = []
for obj in data_norm:
    input_vec = obj[:n_features]
    dists = []
    for w in weights:
        dist = euclidean_distance(input_vec, w)
        dists.append(dist)
    bmu_index = dists.index(min(dists))
    clusters.append(bmu_index)

print("\nРаспределение студентов по кластерам:")
for i, c in enumerate(clusters):
    print(f"Студент {i+1}: Кластер {c+1}")
    
print("\nВесовые коэффициенты нейронов (центры кластеров):")
for i, w in enumerate(weights):
    print(f"Нейрон {i+1}: {w}")
