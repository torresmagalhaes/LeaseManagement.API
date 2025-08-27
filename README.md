# lease-management-backend

API para gestão de aluguel de motos, com cadastro de motos, entregadores, locações e integração com mensageria.

## Tecnologias Utilizadas

- **.NET 8.0** (ASP.NET Core Minimal APIs)
- **Entity Framework Core 9**
- **RabbitMQ** (mensageria)
- **MongoDB** (banco de dados relacional)
- **Docker** e **Docker Compose**
- **NUnit** (testes automatizados)
- **OpenAPI/Scalar** (documentação automática)

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Executando com Docker Compose

Obs.: por padrão o appsettings.development está para localhost, antes de utilizar o dockercompose, atualizar o localhost do RabbitMq e PostgreSQL para rabbit e postgres respectivamente.

1. No terminal, navegue até a pasta `docker` do projeto:

    ```sh
    cd docker
    ```

2. Suba os containers (API, RabbitMQ, MongoDB):

    ```sh
    docker compose up --build
    ```

3. Acesse a API em [https://localhost:8081](https://localhost:8081).

## Principais Funcionalidades

- Cadastro, consulta, alteração e remoção de motos.
- Cadastro de entregadores, envio e atualização de imagem da CNH.
- Criação e consulta de locações, devolução de motos e cálculo de valores/multas.
- Integração com RabbitMQ para eventos de domínio.
- Documentação automática via Swagger/OpenAPI.

## Observações

- As imagens da CNH não são armazenadas no banco, mas sim em storage (disco local ou serviço externo).
- O projeto segue arquitetura em camadas e DDD simplificado.

---

Para mais detalhes sobre os casos de uso, consulte o início deste arquivo ou a documentação OpenAPI gerada pela aplicação.

### Casos de uso
- Eu como usuário admin quero cadastrar uma nova moto.
  - Os dados obrigatórios da moto são Identificador, Ano, Modelo e Placa
  - A placa é um dado único e não pode se repetir.
  - Quando a moto for cadastrada a aplicação deverá gerar um evento de moto cadastrada
    - A notificação deverá ser publicada por mensageria.
    - Criar um consumidor para notificar quando o ano da moto for "2024"
    - Assim que a mensagem for recebida, deverá ser armazenada no banco de dados para consulta futura.
- Eu como usuário admin quero consultar as motos existentes na plataforma e conseguir filtrar pela placa.
- Eu como usuário admin quero modificar uma moto alterando apenas sua placa que foi cadastrado indevidamente
- Eu como usuário admin quero remover uma moto que foi cadastrado incorretamente, desde que não tenha registro de locações.
- Eu como usuário entregador quero me cadastrar na plataforma para alugar motos.
    - Os dados do entregador são( identificador, nome, cnpj, data de nascimento, número da CNHh, tipo da CNH, imagemCNH)
    - Os tipos de cnh válidos são A, B ou ambas A+B.
    - O cnpj é único e não pode se repetir.
    - O número da CNH é único e não pode se repetir.
- Eu como entregador quero enviar a foto de minha cnh para atualizar meu cadastro.
    - O formato do arquivo deve ser png ou bmp.
    - A foto não poderá ser armazenada no banco de dados, você pode utilizar um serviço de storage( disco local, amazon s3, minIO ou outros).
- Eu como entregador quero alugar uma moto por um período.
    - Os planos disponíveis para locação são:
        - 7 dias com um custo de R$30,00 por dia
        - 15 dias com um custo de R$28,00 por dia
        - 30 dias com um custo de R$22,00 por dia
        - 45 dias com um custo de R$20,00 por dia
        - 50 dias com um custo de R$18,00 por dia
    - A locação obrigatóriamente tem que ter uma data de inicio e uma data de término e outra data de previsão de término.
    - O inicio da locação obrigatóriamente é o primeiro dia após a data de criação.
    - Somente entregadores habilitados na categoria A podem efetuar uma locação
- Eu como entregador quero informar a data que irei devolver a moto e consultar o valor total da locação.
    - Quando a data informada for inferior a data prevista do término, será cobrado o valor das diárias e uma multa adicional
        - Para plano de 7 dias o valor da multa é de 20% sobre o valor das diárias não efetivadas.
        - Para plano de 15 dias o valor da multa é de 40% sobre o valor das diárias não efetivadas.
    - Quando a data informada for superior a data prevista do término, será cobrado um valor adicional de R$50,00 por diária adicional.
