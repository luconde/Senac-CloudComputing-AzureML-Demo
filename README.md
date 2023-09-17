# Visão Geral
Este projeto tem o objetivo de demonstrar o uso do Azure Machine Learning como ferramenta para fazer previsão com base em uma regressão Linear.

Projeto construído para a disciplina **Cloud Computing e Internet das Coisas** do curso **Tecnologia em Sistemas para Internet (TSI) do Senac-SP**.

# Autor
**Luciano Condé de Souza (luconde@gmail.com)**  
**Data da criação do projeto**: 2023-01-23  
**Data da última atualização**: 2023-09-17  
**Versão**: 1.0.20  

## Disclaimer
O seguinte material foi construído a partir de referências publicadas na Internet, livros e artigos acadêmicos. As referências foram utilizadas de sites e posts na Internet, não há qualquer propósito de plagiar os autores, em caso de pedidos de adição do autor, pode encontrar em contato pelo email luconde@gmail.com. A simplificação de certos conteúdos tem o único propósito didático para facilitar o entendimento dos mesmos para os alunos.

# Notas da versão 
## Versão 1.0.20
1. Adição de versionamento
2. Modificação do Sobre
3. Pequenos ajustes

# Detalhes técnicos

## Funcionalidades
1. Faz uma projeção com base em Regressão Linear

## Pré-requisitos
1. Subscription Ativa do Microsoft Azure
2. Visual Studio 2022 Community para executar o código-fonte
3. Modelo implementado no Azure Machine Learning Studio exportado como EndPoint

# Informações adicionais
A Regressão Linear utilizada é

$$ 
\hat{Preco} = \alpha + \beta_1 Quartos + \beta_2 Banheiros + \beta_3 AreaUtil + \beta_4 AreaTotal + \beta_5 Anos + \epsilon 
$$