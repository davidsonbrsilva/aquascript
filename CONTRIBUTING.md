# Guia de Contribuição para AquaScript

Olá! Estou feliz que você tem interesse em colaborar no projeto. Antes de submeter suas contribuições, tenha certeza de reservar um momento para ler os seguintes tópicos:

- [Código de Conduta](CODE_OF_CONDUCT.md)
- [Diretrizes de _Issues_](#diretrizes-de-issues)
- [Diretrizes de _Pull Request_](#diretrizes-de-pull-request)
- [Configuração de Desenvolvimento](#configuracao-de-desenvolvimento)
- [Estrutura do Projeto](#estrutura-do-projeto)

## Diretrizes de _Issues_

_Issues_ restringem-se somente ao desenvolvimento do projeto. Para dúvidas ou discussões, [entre em contato com o autor](davidsonbruno@outlook.com). Uma _Issue_ deve ser aberta sempre que um _bug_ for detectado ou uma nova funcionalidade for implementada. Para identificá-las, fazemos uso de _tags_ específicas para manter o projeto organizado.

## Diretrizes de _Pull Request_

O _branch_ `master` é basicamente um _snapshot_ da última versão estável. Recomendamos que todo o desenvolvimento seja feito em **_branches_ separados**. Pedimos também que _commits_ sejam feitos em **inglês** sempre que possível.

## Configuração de Desenvolvimento

### Pré-requisitos

Você precisará de uma IDE que apoie o seu desenvolvimento, como o [Visual Studio](https://www.visualstudio.com/pt-br/downloads/) ou o [MonoDevelop](http://www.monodevelop.com/download/).

### Instalação

Crie uma cópia do projeto para o seu repositório local com o comando:

```
git clone https://github.com/davidsonbsilva/aquascript.git
```

## Estrutura do Projeto

Todo o conteúdo relevante do compilador para AquaScript está na pasta `compiler`. Dentro dela você encontrará a solução criada pelo Visual Studio. Inicie o arquivo `AquaScript.sln` para carregá-la.

Neste mesmo diretório, você também verá a pasta `Compiler`. Dentro dela, há a seguinte árvore de diretórios:

- **`Attributes`**: contém as classes que estendem `System.Attributes`, responsáveis por criar estruturas de armazenamento para metadados;
- **`Enum`**: contém os enumeradores do projeto;
- **`Exception`**: contém as classes que estendem `System.Exception`, responsáveis por tratamentos de erros do programa;
- **`Structure`**: contém as classes de estrutura do projeto;

Os diretórios **`bin`**, **`obj`** e **`Properties`** são gerados automaticamente pelo Visual Studio. De maneira resumida, **`bin`** contém os arquivos de saída da compilação do projeto, **`obj`** contém os arquivos intermediários necessários para a produção do arquivo executável final e **`Properties`** contém os arquivos de informação do Assembly.

## Créditos

Obrigado a todos que já contribuíram ou apoiam este projeto!

_Este documento foi baseado no [Guia de Contribuição do Vue.js](https://github.com/vuejs/vue/blob/dev/.github/CONTRIBUTING.md)_.
