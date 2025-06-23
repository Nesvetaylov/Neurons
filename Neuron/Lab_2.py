import numpy as np

X = np.array([
    [0, 0],
    [1, 0],
    [0, 1],
    [1, 1]
])

y = np.array([0, 1, 1, 0])

def relu(v):
    return np.maximum(0, v)

def hidden_features(x):
    h1 = relu(x[0] + x[1] - 1)
    h2 = relu(-x[0] - x[1] + 1)
    return np.array([h1, h2])

H = np.array([hidden_features(x) for x in X])

for i, x in enumerate(X):
    print(f"Точка {x}, скрытые признаки (h1,h2) = {H[i]}, класс = {y[i]}")




import random

def generate_data(num_points):
    data = []
    for _ in range(num_points):
        x1 = random.random()
        x2 = random.random()
        label = 1 if x1 > x2 else 0  
        data.append(([x1, x2], label))
    return data

# Смещение
def add_bias(inputs):
    return [1.0] + inputs 


def step_activation(value):
    return 1 if value >= 0 else 0

def adaline_activation(value):
    return 1 if value >= 0.5 else 0

def train_perceptron(data, learning_rate=0.1, epochs=100):
    weights = [random.uniform(-1, 1) for _ in range(3)]  
    for _ in range(epochs):
        for inputs, label in data:
            x = add_bias(inputs)
            weighted_sum = sum(w * xi for w, xi in zip(weights, x))
            prediction = step_activation(weighted_sum)
            error = label - prediction
            weights = [w + learning_rate * error * xi for w, xi in zip(weights, x)]
    return weights


def train_adaline(data, learning_rate=0.1, epochs=100):
    weights = [random.uniform(-1, 1) for _ in range(3)]
    for _ in range(epochs):
        for inputs, label in data:
            x = add_bias(inputs)
            weighted_sum = sum(w * xi for w, xi in zip(weights, x))
            output = weighted_sum  
            prediction = adaline_activation(output)
            error = label - prediction
            weights = [w + learning_rate * error * xi for w, xi in zip(weights, x)]
    return weights

def evaluate(weights, data, activation_fn):
    correct = 0
    for inputs, label in data:
        x = add_bias(inputs)
        weighted_sum = sum(w * xi for w, xi in zip(weights, x))
        prediction = activation_fn(weighted_sum)
        if prediction == label:
            correct += 1
    return correct / len(data)



train_data = generate_data(20)
test_data = generate_data(1000)


perceptron_weights = train_perceptron(train_data)
accuracy_perceptron = evaluate(perceptron_weights, test_data, step_activation)

print("Перцептрон:")
print("Финальные веса:", perceptron_weights)
print("Точность на тестовой выборке: {:.2f}%".format(accuracy_perceptron * 100))


adaline_weights = train_adaline(train_data)
accuracy_adaline = evaluate(adaline_weights, test_data, adaline_activation)

print("\nАдалайн (дискретный):")
print("Финальные веса:", adaline_weights)
print("Точность на тестовой выборке: {:.2f}%".format(accuracy_adaline * 100))




import numpy as np

def sigmoid(x):
    return 1 / (1 + np.exp(-x))

def sigmoid_derivative(x):
    return x * (1 - x)

# Обучающие данные — входы (4 образа) и их метки (one-hot)
X = np.array([
    [1, 0, 1,
     0, 1, 0,
     1, 0, 1],  # X
    [1, 0, 1,
     0, 1, 0,
     0, 1, 0],  # Y
    [0, 1, 0,
     0, 1, 0,
     0, 1, 0],  # I
    [1, 0, 0,
     1, 0, 0,
     1, 1, 1]   # L
])

# Метки (4 выхода, one-hot)
y = np.array([
    [0, 0, 0, 1],  # X
    [0, 0, 1, 0],  # Y
    [0, 1, 0, 0],  # I
    [1, 0, 0, 0],  # L
])

np.random.seed(1)

input_size = 9
hidden_size = 6  
output_size = 4


weights_input_hidden = 2 * np.random.random((input_size, hidden_size)) - 1
weights_hidden_output = 2 * np.random.random((hidden_size, output_size)) - 1


learning_rate = 0.5
epochs = 10000

for epoch in range(epochs):
    hidden_input = np.dot(X, weights_input_hidden)
    hidden_output = sigmoid(hidden_input)

    final_input = np.dot(hidden_output, weights_hidden_output)
    final_output = sigmoid(final_input)

    output_error = y - final_output
    output_delta = output_error * sigmoid_derivative(final_output)

    hidden_error = output_delta.dot(weights_hidden_output.T)
    hidden_delta = hidden_error * sigmoid_derivative(hidden_output)

    weights_hidden_output += hidden_output.T.dot(output_delta) * learning_rate
    weights_input_hidden += X.T.dot(hidden_delta) * learning_rate

    if epoch % 1000 == 0:
        loss = np.mean(np.abs(output_error))
        print(f"Epoch {epoch}, Loss: {loss}")

def predict(x):
    h = sigmoid(np.dot(x, weights_input_hidden))
    out = sigmoid(np.dot(h, weights_hidden_output))
    return out

print("\nРаспознавание обучающих образов:")
for i, sample in enumerate(X):
    res = predict(sample)
    print(f"Образ {i}: сеть выдала {res.round()} (ожидалось {y[i]})")

noisy_I = np.array([0,1,0,1,1,0,0,1,0])
res_noisy = predict(noisy_I)
print("\nРаспознавание шумного образа I:")
print("Выход сети:", res_noisy.round())
