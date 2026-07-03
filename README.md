# 📦 Projeto de Estudos - Apache Kafka

Este projeto tem como objetivo estudar e praticar os conceitos do **Apache Kafka**, incluindo produção e consumo de mensagens, além da execução do Kafka em ambiente containerizado.

Também foram realizados **testes de sobrecarga (load/stress testing)** para entender o comportamento do Kafka sob alta concorrência de requisições.
  Kafka.Producer.API
  └── LoadTests
  └── TesteSobrecarga.js

---

## 🚀 Objetivo

- Entender o funcionamento do Apache Kafka
- Aprender sobre produção e consumo de mensagens
- Estudar conceitos como topics, partitions e consumer groups
- Executar Kafka utilizando Docker

---

## 🧱 Tecnologias utilizadas

- Apache Kafka
- Docker / Docker Compose
- Zookeeper (quando aplicável)
- .NET / Java / Node.js (dependendo do estudo)

---

## 🐳 Subindo o Kafka com Docker

Este projeto utiliza Docker para facilitar a execução do Kafka.

### 📄  `docker-compose.yml`

```yaml
version: '3'
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:7.4.0
    networks: 
      - broker-kafka
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000

  kafka:
    image: confluentinc/cp-kafka:7.4.0
    networks: 
      - broker-kafka
    depends_on:
      - zookeeper
    ports:
      - 9092:9092
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://kafka:29092,PLAINTEXT_HOST://localhost:9092
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: PLAINTEXT:PLAINTEXT,PLAINTEXT_HOST:PLAINTEXT
      KAFKA_INTER_BROKER_LISTENER_NAME: PLAINTEXT
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1

  kafdrop:
    image: obsidiandynamics/kafdrop:latest
    networks: 
      - broker-kafka
    depends_on:
      - kafka
    ports:
      - 19000:9000
    environment:
      KAFKA_BROKERCONNECT: kafka:29092

networks: 
  broker-kafka:
    driver: bridge  
