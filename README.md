# Projeto de Época Especial da Disciplina de Linguagens de Programação 1

# Simulação Pedra, Papel e Tesoura

## Autoria

Vasco Duarte (21905658)  

## [Repositório GitHub](https://github.com/Vascord/LP1-Epoca_Especial)

## Contribuição

### Vasco Duarte

Não tendo grupo, nem nenhuma ajuda/commit do meu antigo grupo, eu posso
afirmar que fiz o projeto desde o começo até a sua conclusão.

## Arquitetura da Solução

### Iniciação da Simulação

Quando o programa é iniciado, a classe `Properties`, com o método `ReadFile()`, 
lendo o nome do ficheiro e pegando as propriedades dentro do ficheiro. O 
programa termina nesse ponto se faltar algum argumento obrigatório ou se uma das
propriedades. Depois disso, é criada uma instância de `Simulator`, e inicia-se o
método `SimulatorRunner()`.

### Durante a Simulação

A classe `Simulator` vai criar uma instância de `World`, uma lista de `Agent` e
uma instância de `UI`. A instância de `World` vai ser generada consoante as 
propriedades de `Properties`, e depois vai meter os agentes aléatoriamente na 
instância de `World` e metidos na lista de `Agent`.

Depois, o programa vai criar uma _Thread_ no `SimulatorRunner()`, onde o 
método `CoreLoop()` vai decorrer o simulador em sí e o `SimulatorRunner()` vai
estar à espera que o utilizador pressione o butão _Escape_, acabando o programa.

O `CoreLoop()` consiste de um _loop_ que é executado infinitamente se o 
utilizador não pressionar o butão _Escape_.  
  
Para cada turno executado, o método `CoreLoop()`, em primeiro lugar, 
vai criar o valor lambda para cada evento para o _Poisson algorithm_. Depois de 
ter o numero de cada evento, criamos uma lista de `Event` e com o método 
`PuttingEvents()` metemos os tipos de eventos adequados. Esse lista depois leva 
_shuffle_ com o método `ShuffleList()`, baseado no _Fisher-Yates algorithm_.
  
Em seguida, a lista é percurrida, pegando duas casas vizinhas e fazendo o 
evento adequado. A instância de `World` é actualizada e o método 
`VisualizacaoUI()` da instância de `UI` cria uma visualização para o utilizador
quando já todos os eventos estiverem feitos.

### Fim de simulação

Se o utilizador prime o botão _Escape_, então no método `SimulatorRunner()` o
_while_ é quebrado e fecha o programa.

## UML

![Diagrama de classes](UML.png)

## Observações e resultados

Consoante as observações, mais o _swap-rate-exp_ é pequeno, mais a formação de 
grandes comunidades de um dos tipos há (eles estão mais juntos), então a -1, 
conseguimos ver bem esse fenómeno. Se è 1, temos o efeito inverso, onde só se
cria pequenas comunidades.

Um conjunto que possa eliminar uma espécie vai deixar o _swap-rate-exp_ a 0,
metemos o _repr-rate-exp_ a -1, e o _selc-rate-ext_ a 1. Isto faz que as 
espécies lutem sempre, e fazendo que uma das espécie com poucas unidades possa
se extinguir, tendo pouca reprodutividade.

As duas outras ficam a lutar, fazendo que a mais forte (aquela que ganha pelas
as regras do pedra, papel o tesoura) seja a única viva.

## Referencias

- [Simulação de Pandemia](https://github.com/NelsonSalvador/Recurso_Lp1)

Pessoas que me ajudaram : 
- Miguel Romão Fernández para Threading e boas práticas em geral.
- David D. para Threading e Task (que modifiquei no final).
- Grupo 03 por me ter guiado na compreensão do enunciado.