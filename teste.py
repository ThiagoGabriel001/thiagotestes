def fibonacci_sequence(num):
    # Inicializa a sequência de Fibonacci
    a, b = 0, 1
    
    # Verifica se o número informado é 0 ou 1, que já estão na sequência
    if num == 0 or num == 1:
        return f"O número {num} pertence à sequência de Fibonacci."
    
    # Gera a sequência até o número informado ou até ultrapassá-lo
    while b < num:
        a, b = b, a + b

    # Verifica se o número informado está na sequência gerada
    if b == num:
        return f"O número {num} pertence à sequência de Fibonacci."
    else:
        return f"O número {num} não pertence à sequência de Fibonacci."

# Solicita o número ao usuário
num = int(input("Informe um número: "))

# Chama a função e exibe o resultado
print(fibonacci_sequence(num))