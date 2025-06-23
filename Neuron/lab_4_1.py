import random

def sgn(x):
    return 1 if x >= 0 else -1

def train_hopfield(patterns):
    N = len(patterns[0])
    weights = [[0 for _ in range(N)] for _ in range(N)]
    
    for p in patterns:
        for i in range(N):
            for j in range(N):
                if i != j:
                    weights[i][j] += p[i] * p[j]
    return weights

def update_hopfield(state, weights):
    N = len(state)
    new_state = state[:]
    for i in range(N):
        s = 0
        for j in range(N):
            if i != j:
                s += weights[i][j] * state[j]
        new_state[i] = sgn(s)
    return new_state

def run_until_stable(state, weights, max_iter=100):
    for _ in range(max_iter):
        new_state = update_hopfield(state, weights)
        if new_state == state:
            break
        state = new_state
    return state

pattern1 = [1]*100  
pattern2 = [-1]*100 

weights = train_hopfield([pattern1, pattern2])

def add_noise(pattern, noise_percent):
    noisy = pattern[:]
    n_flip = int(len(pattern) * noise_percent / 100)
    flip_indices = random.sample(range(len(pattern)), n_flip)
    for idx in flip_indices:
        noisy[idx] *= -1
    return noisy


test_pattern = add_noise(pattern1, 20) 
result = run_until_stable(test_pattern, weights)

print("Test pattern:", test_pattern)
print("Result after stabilization:", result)






#(2)
import random

def hamming_distance(x, y):
    return sum(1 for xi, yi in zip(x, y) if xi != yi)

def train_hamming(patterns):
    return patterns[:]  

def hamming_network(test_pattern, patterns):
    distances = [hamming_distance(test_pattern, p) for p in patterns]
    best_match = distances.index(min(distances))
    return best_match, distances

def add_noise(pattern, noise_percent):
    noisy = pattern[:]
    n_flip = int(len(pattern) * noise_percent / 100)
    flip_indices = random.sample(range(len(pattern)), n_flip)
    for idx in flip_indices:
        noisy[idx] *= -1
    return noisy

patterns = []
for i in range(10):
    pattern = []
    for j in range(49):
        value = 1 if (j + i) % (i + 2) == 0 else -1
        pattern.append(value)
    patterns.append(pattern)

stored_patterns = train_hamming(patterns)

original_index = 0
test_pattern = add_noise(patterns[original_index], 20)

result_index, distances = hamming_network(test_pattern, stored_patterns)

print(f"Ожидался образ: {original_index}")
print(f"Распознанный образ: {result_index}")
print(f"Расстояния до эталонов: {distances}")

if result_index == original_index:
    print("Корректно распознан образ")
else:
    print("Ошибка распознавания")


