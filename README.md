
# WebAPI .NET 8 com Redis e SQL Server 2022

![WebAPI with Redis](https://github.com/user-attachments/assets/fe4a74cf-47f1-4299-a56e-6646d1aaa6db)

## Contextualizando a ideia de Cache para APIs

Em aplicações modernas, o desempenho e a escalabilidade são fundamentais para oferecer uma experiência de usuário fluida e responsiva. O uso de cache em APIs é uma prática recomendada para melhorar o tempo de resposta e reduzir a carga no banco de dados. O cache armazena temporariamente os dados frequentemente acessados, permitindo que a aplicação responda rapidamente a solicitações sem a necessidade de acessar a base de dados toda vez.

## Cache Centralizado e Distribuído

Existem duas abordagens principais para implementar caching: cache centralizado e cache distribuído.

- **Cache Centralizado**: Neste modelo, todos os dados em cache são armazenados em um único servidor. Embora seja mais simples de implementar, esse tipo de cache pode se tornar um gargalo em situações de alta carga, pois todas as requisições dependem de um único ponto de falha.

- **Cache Distribuído**: O cache distribuído, por outro lado, permite que os dados em cache sejam armazenados em múltiplas instâncias de servidores, como o Redis. Isso melhora a resiliência e a escalabilidade da aplicação, pois a carga é distribuída entre vários nós. Além disso, o Redis fornece recursos adicionais, como persistência de dados e suporte a estruturas de dados complexas.

## 🚀 Exemplo de Implementação

Para exemplificar esses conceitos, criei este repositório que demonstra boas práticas de desenvolvimento ao utlizar essa abordagem.

## 📋 Pré-requisitos

- Git
- Visual Studio 2022
- Docker

## 🛠️ Instalação

1. Clone este repositório:

   git clone [https://github.com/matteusmachhado/webapi-with-redis.git](https://github.com/matteusmachhado/webapi-with-redis.git)

2. Navegue até o diretório do projeto:

```
   cd [diretorio]\webapi-with-redis\docker
```

3. Execute os serviços com o Docker Compose:

```
   docker-compose up -d --build
```

 ✔ Network docker_local   Created                                                                                 
 ✔ Container mssql-db     Started                                                                                  
 ✔ Container redis-cache  Started                                                                                  
 ✔ Container webapi       Started                                                                                  

4. Acesse a API através do Nginx:

   Abra seu navegador e vá para http://localhost:5001/swagger/index.html

## Colaboradores

- Mateus Machado - Criador e Mantenedor

