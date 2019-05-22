# AquaScript

![license](https://img.shields.io/github/license/davidsonbsilva/aquascript.svg) ![GitHub issues](https://img.shields.io/github/issues/davidsonbrsilva/aquascript.svg)

AquaScript é uma **linguagem de programação procedural**, desenvolvida para fins de aprendizado sobre teoria de compiladores. É bastante simples e não objetiva trazer novidades em relação às diversas linguagens já conhecidas, porém espera-se que atenda ao seu propósito. 

Apesar do termo Script em seu nome, é uma linguagem de programação compilada. O uso deste termo se deve ao fato de que as declarações em AquaScript se assemelham com as declarações simplistas da linguagem JavaScript. 

## Definição

AquaScript é uma linguagem bem simples que possui uma definição típica das linguagens C-Like. Veja a seguir a notação permitida:

- Controladores de fluxo `if`, `else` e `for`;
- Operadores aritméticos `+`, `-`, `*`, `/` e `%`;
- Operadores condicionais `=`, `<`, `>`, `<=`, `>=` e `!=`;
- Operadores lógicos `and`, `or` e `!`
- Operadores de incremento `++` e decremento `--`;
- Palavras reservadas `return`, `read`, `write`, `break` e `null`;

### Tipagem

AquaScript é uma linguagem **fracamente tipada** e lida apenas com os tipos `number`, `text`, e `bool`. Os valores atribuídos a uma variável não mudam durante a execução do programa, ou seja, é também **estaticamente tipada**. Também não há conversões implícitas ou explícitas. Com o avanço do projeto, é possível que futuramente isso seja implementado.

### Sintaxe

A sintaxe de AquaScript foi construída para ser bem simples e familiar aos programadores acostumados com outras linguagens. Ela é tão simples que dispensa o uso de um ponto de início do programa, o que permite ao usuário construir suas aplicações com pouca verbosidade. Mas atenção, há um custo para isso: o programa é lido pelo compilador e executado na ordem em que as declarações são feitas.

Veja a seguir a definição da sintaxe em Backus-Naur Form:

```

program  ::= statement*

statement
         ::= if
           | for
           | attribuition
           | singlinecomment
           | multilinecomment
           | ( increment | decrement | return | read | write | 'break' ) ';'

if       ::= 'if' '(' expression ')' body

for      ::= 'for' '(' attribuition? ';' expression? ';' attribuition? ')' body

attribuition
         ::= 'id' ':' ( function | expression ';' )

singlinecomment
         ::= '//' [^\n]*

multilinecomment
         ::= '/*' ( [^*] | '*'+ [^*/] )* '*'* '*/'

increment
         ::= 'id' '++'

decrement
         ::= 'id' '--'

return   ::= 'return' expression?

read     ::= 'read' 'id'

write    ::= 'write' expression

function ::= '(' param? ')' body

expression
         ::= side ( ( '<' | '>' | '<=' | '>=' | '!=' | '=' ) side )? ( ( 'and' | 'or' ) side ( ( '<' | '>' | '<=' | '>=' | '!=' | '=' ) side )? )*

param    ::= 'id' ( ',' 'id' )*

body     ::= '{' statement* '}'

side     ::= term ( ( '+' | '-' ) term )*

term     ::= unaryexpr ( ( '*' | '/' | '%' ) unaryexpr )*

unaryexpr
         ::= ( '+' | '-' )? factor

factor   ::= bool
           | 'number'
           | 'text'
           | 'id'
           | 'null'
           | '(' expression ')'

bool     ::= 'true'
           | 'false'

```

Consulte o [Diagrama de Sintaxe](https://davidsonbrsilva.github.io/aquascript)  de AquaScript gerado a partir da definição descrita acima.

## Exemplos

### Declaração de variáveis

Variáveis em AquaScript são sempre inicializadas com algum valor. O operador de atribuição é o `:` (dois-pontos), como se estivéssemos vendo o atributo de um objeto em JavaScript.

```

a: 3; // a recebe o valor 3 e é do tipo number
b: 4.5; // b recebe o valor 4.5 e é do tipo number
c: a + b; // c agora vale 7.5

d: "Foo";
e: "bar";
f: d + e; // f é do tipo text e possui o valor "Foobar";

g: true; // g é do tipo bool e vale true
h: !g; // h é do tipo bool e vale a negação de g - false
i: g or h; // i é do tipo bool e vale true

```

### Declaração de funções

Declaração de funções em AquaScript são feitas de maneira diferente de outras linguagens como C ou Java. Nomear funções é como dar apelidos a elas através de variáveis, tal como é mostrado a seguir:

```

<function_name>: (<parameters>)
{
	// body
}

```

Confira a seguir alguns exemplos.

#### Olá mundo

```

write "Hello, World!";

```

#### Diga olá

```

say_hello: (name)
{
	write "Hello, " + name + "!";
}

say_hello("Petter"); // Saída: "Hello, Petter!"

```

```

say_hello: (name)
{
	return "Hello, " + name + "!";
}

write "Informe um nome: ";
read name; // Lê uma entrada do usuário

write say_hello(name);

```

#### Fatorial

``` 

factorial: (x)
{
	if (x < 2)
	{
		return 1;
	}

	return x * factorial(x - 1);
}

write factorial(5); // Saída: 120

```

#### O maior

```

the_bigger: (x, y)
{
	if (x > y)
	{
		return x;
	}
	else
	{
		return y;
	}
}

write the_bigger(5, 6.5); // Saída: 6.5

``` 

#### É primo

```

is_prime: (x)
{
	count: 0;

	for (i: 0; i < x; i++)
	{
		if (x % i = 0)
		{
			count++;

			if (count > 2)
			{
				return false;
			}
		}
	}

	return true;
}

write is_prime(4); // Saída: false
write is_prime(7); // Saída: true

```

## Contribuição

Para contribuir com o projeto, leia o [Guia de Contribuição](CONTRIBUTING.md) antes de fazer um Pull Request.

## Contato

Para dúvidas ou esclarecimentos, envie um e-mail para <davidsobruno@outlook.com>.

## Licença
[MIT](LICENSE.md) Copyright (c) 2018 Davidson Bruno da Silva
