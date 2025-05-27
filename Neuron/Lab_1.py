
# Задание (1)

# import numpy as np

# inputs = np.array([
#     [0.97, 0.20],
#     [1.00, 0.00],
#     [0.72, -0.70],
#     [0.67, -0.74],
#     [0.80, -0.60],
#     [0.00, -1.00],
#     [0.20, -0.97],
#     [0.30, -0.95]
# ])

# n_neurons = 4
# learning_rate = 0.5
# n_epochs = 10

# weights = np.random.randn(n_neurons, 2)
# weights = np.array([w / np.linalg.norm(w) for w in weights])

# # Правило Гроссберга
# for epoch in range(n_epochs):
#     for x in inputs:
#         x = x / np.linalg.norm(x)
#         activations = weights @ x  
#         winner_idx = np.argmax(activations)
#         weights[winner_idx] += learning_rate * (x - weights[winner_idx])
#         weights[winner_idx] /= np.linalg.norm(weights[winner_idx])

# print("Финальные веса нейронов:")
# for i, w in enumerate(weights):
#     print(f"Нейрон {i+1}: {w}")

# Задание (2)
# import numpy as np

# inputs = np.array([
#     [0.97, 0.20],
#     [1.00, 0.00],
#     [0.72, -0.70],
#     [0.67, -0.74],
#     [0.80, -0.60],
#     [0.00, -1.00],
#     [0.20, -0.97],
#     [0.30, -0.95]
# ])

# n_neurons = 4
# learning_rate = 0.5
# n_epochs = 10
# max_wins = 20

# weights = np.random.randn(n_neurons, 2)
# weights = np.array([w / np.linalg.norm(w) for w in weights])
# win_counts = np.zeros(n_neurons)

# # Обучение
# for epoch in range(n_epochs):
#     for x in inputs:
#         x = x / np.linalg.norm(x)
#         activations = weights @ x
#         penalized_activations = activations / (1 + 2 * win_counts)

#         penalized_activations = [
#             a if win_counts[i] < max_wins else -np.inf
#             for i, a in enumerate(penalized_activations)
#         ]

#         winner_idx = np.argmax(penalized_activations)
#         win_counts[winner_idx] += 1

#         weights[winner_idx] += learning_rate * (x - weights[winner_idx])
#         weights[winner_idx] /= np.linalg.norm(weights[winner_idx])

# print("Финальные веса нейронов:")
# for i, w in enumerate(weights):
#     print(f"Нейрон {i+1}: {w}")

# print("\nЧисло побед каждого нейрона:")
# for i, c in enumerate(win_counts):
#     print(f"Нейрон {i+1}: {int(c)} побед")


# Задание (3)
# import numpy as np

# n_inputs = 2
# n_neurons = 2
# learning_rate = 0.1
# n_epochs = 10

# inputs = np.array([
#     [1, 0],
#     [0, 1],
#     [1, 1],
#     [0, 0],
# ])

# weights = np.random.randn(n_neurons, n_inputs)

# print("Начальные веса:")
# print(weights)

# # Функция вычисления выхода нейронов (линейная)
# def neuron_output(weights, x):
#     return weights @ x

# # Обучение по правилу Хебба
# for epoch in range(n_epochs):
#     for x in inputs:
#         y = neuron_output(weights, x)
#         # Обновляем веса для каждого нейрона i и каждого входа j:
#         for i in range(n_neurons):
#             for j in range(n_inputs):
#                 # Δw_ij = η * y_j * y_i
#                 # Здесь y_j — входной сигнал j, y_i — выход нейрона i
#                 delta_w = learning_rate * x[j] * y[i]
#                 weights[i, j] += delta_w

# print("\nВеса после обучения по правилу Хебба:")
# print(weights)

