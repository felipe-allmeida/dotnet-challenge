# Desafio Backend

## Vis�o Geral
Este projeto � um sistema de gest�o de aluguel de motocicletas e entregas desenvolvido em C# .NET 8. 
O sistema permite que administradores cadastrem pedidos na plataforma, notifiquem entregadores aptos a efetuar as entregas, e gerenciem todo o ciclo de vida das entregas. 
A comunica��o ass�ncrona � realizada usando RabbitMQ para notifica��o de novos pedidos. 
Al�m disso, o sistema permite que entregadores aceitem pedidos e registrem a finaliza��o das entregas.

## Estrutura do Projeto

### Entidades Pricipais
- **DeliveryRider**: Representa um entregador cadastrado no sistema.
- **Bike**: Representa uma moto cadastrada no sistema.
- **Rental**: Representa um aluguel de motocicleta cadastrado no sistema.
- **DeliveryRequest**: Representa um pedido cadastrado no sistema.
- **DeliveryRequestNotification**: Representa uma notifica��o de pedido criado cadastrada no sistema.

### Servi�os
- **BikeRental.API**: API RESTful que permite a gest�o de entregadores, motos, alugueis e pedidos.

### Casos de Uso Solicitados vs Implementados
- [x] Eu como usu�rio admin quero cadastrar uma nova moto.
  - [x] Os dados obrigat�rios da moto s�o Identificador, Ano, Modelo e Placa
  - [x] A placa � um dado �nico e n�o pode se repetir.
    
- [x] Eu como usu�rio admin quero consultar as motos existentes na plataforma e conseguir filtrar pela placa.
- [x] Eu como usu�rio admin quero modificar uma moto alterando apenas sua placa que foi cadastrado indevidamente.
- [x] Eu como usu�rio admin quero remover uma moto que foi cadastrado incorretamente, desde que n�o tenha registro de loca��es.
- [x] Eu como usu�rio entregador quero me cadastrar na plataforma para alugar motos.
  - [x] Os dados do entregador s�o( identificador, nome, cnpj, data de nascimento, numero da cnh, tipo da cnh, imagemCnh)
  - [x] Os tipos de cnh v�lidos s�o A, B ou ambas A+B.
  - [x] O cnpj � �nico e n�o pode se repetir.
  - [x] O n�mero da CNH � �nico e n�o pode se repetir.
- [x] Eu como entregador quero enviar a foto de minha cnh para atualizar meu cadastro.
  - [x] O formato do arquivo deve ser png ou bmp.
  - [x] A foto n�o poder� ser armazenada no banco de dados, voc� pode utilizar um storage( disco local, amazon s3, minIO ou outros).
- [x] Eu como entregador quero alugar uma moto por um per�odo.
  - [x] Os planos dispon�veis para loca��o s�o:
    - [x] 7 dias com um custo de R$30,00 por dia
    - [x] 15 dias com um custo de R$28,00 por dia
    - [x] 30 dias com um custo de R$22,00 por dia
  - [x] A loca��o obrigat�riamente tem que ter uma data de inicio e uma data de t�rmino e outra data de previs�o de t�rmino.
  - [x] O inicio da loca��o obrigat�riamente � o primeiro dia ap�s a data de cria��o.
  - [x] O entregador s� conseguir� concluir na loca��o caso exista motos dispon�veis.
  - [x] Somente entregadores habilitados na categoria A podem efetuar uma loca��o
- [x] Eu como entregador quero informar a data que irei devolver a moto e consultar o valor total da loca��o.
  - [x] Quando a data informada for inferior a data prevista do t�rmino, ser� cobrado o valor das di�rias e uma multa adicional
    - [x] Para plano de 7 dias o valor da multa � de 20% sobre o valor das di�rias n�o efetivadas.
    - [x] Para plano de 15 dias o valor da multa � de 40% sobre o valor das di�rias n�o efetivadas.
    - [x] Para plano de 30 dias o valor da multa � de 60% sobre o valor das di�rias n�o efetivadas.
  - [x] Quando a data informada for superior a data prevista do t�rmino, ser� cobrado um valor adicional de R$50,00 por di�ria adicional.
- [x] Eu como admin quero cadastrar um pedido na plataforma e disponibilizar para os entregadores aptos efetuarem a entrega.
  - [x] Os dados obrigat�rios do pedido s�o: identificador, data de criacao, valor da corrida, situacao.
  - [x] As situa��es v�lidas s�o disponivel, aceito e entregue.
  - [x] Quando o pedido entrar na plataforma a aplica��o dever� notificar os entregadores sobre a existencia desse pedido.
    - [x] A notifica��o dever� ser publicada por mensageria.
    - [x] Somente entregadores com loca��o ativa e que n�o estejam com um pedido j� aceito dever�o ser notificados.
  - [x] Criar um consumidor para notifica��o de pedido dispon�vel.
    - [x] Assim que a mensagem for recebida, dever� ser armazenada no banco de dados para consulta futura.
- [x] Eu como admin quero consultar todos entregadores que foram notificados de um pedido.
- [x] Eu como entregador quero aceitar um pedido.
  - [x] Somente entregadores que tenham sido notificados podem aceitar o pedido.
- [x] Eu como entregador quero efetuar a entrega do pedido.

## Padr�o de Design Utilizado: CQRS
O projeto utiliza o padr�o de design Command Query Responsibility Segregation (CQRS), que separa as opera��es de leitura (queries) das opera��es de escrita (commands). Isso permite uma melhor organiza��o do c�digo, al�m de otimizar e escalar separadamente as partes de leitura e escrita da aplica��o.

- **Commands**: Utilizados para a��es que alteram o estado do sistema, como cadastrar um pedido, aceitar um pedido e finalizar uma entrega.
- **Queries**: Utilizados para buscar dados do sistema, como consultar pedidos e entregadores notificados.


## Pr�-requisitos

Antes de come�ar, verifique se voc� possui os seguintes pr�-requisitos instalados:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

### Passo 1: Verificar os pr� requisitos
Para verificar se o .NET 8 est� corretamente instalado, abra o terminal (ou prompt de comando) e digite:
```bash
dotnet --version
```

Para verificar se o Docker est� corretamente instalado, abra o terminal (ou prompt de comando) e digite:
```bash
docker --version
```

### Passo 2: Clonar o reposit�rio
Para clonar o reposit�rio, abra o terminal (ou prompt de comando) e digite:
```bash
git clone https://github.com/felipe-allmeida/dotnet-challenge.git
```

### Passo 3: Rodar o docker-compose
Para rodar o docker-compose, abra o terminal (ou prompt de comando) e digite:
```bash
docker-compose up -d
```

### Comandos �teis durante o desenvolvimento

## Adicionar migra��o

dotnet ef migrations add Initial_Create --context BikeRentalContext --project ./src/Services/BikeRental/BikeRental.Data/BikeRental.Data.csproj --startup-project ./src/Services/BikeRental/BikeRental.API/BikeRental.API.csproj -o Migrations
dotnet ef migrations add Initial_Create --context IntegrationEventLogContext --project ./src/CrossCutting/EventBus/BikeRental.CrossCutting.IntegrationEventLog/BikeRental.CrossCutting.IntegrationEventLog.csproj --startup-project ./src/Services/BikeRental/BikeRental.API/BikeRental.API.csproj -o Migrations
