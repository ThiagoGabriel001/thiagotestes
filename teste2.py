def verificar_letra_a(texto):
    # Converte a string para minúsculas e conta quantas vezes 'a' aparece
    quantidade_a = texto.lower().count('a')

    # Verifica se a letra 'a' aparece e retorna a quantidade
    if quantidade_a > 0:
        return f"A letra 'a' aparece {quantidade_a} vezes na string."
    else:
        return "A letra 'a' não foi encontrada na string."

# Solicita a string ao usuário
texto = input("Digite uma string: ")

# Chama a função e exibe o resultado
print(verificar_letra_a(texto))