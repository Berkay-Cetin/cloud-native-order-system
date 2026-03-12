# Cloud Native Order System

![.NET](https://img.shields.io/badge/.NET-8-512BD4?style=flat&logo=dotnet)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-3-FF6600?style=flat&logo=rabbitmq)
![MassTransit](https://img.shields.io/badge/MassTransit-Saga-512BD4?style=flat&logo=dotnet)
![gRPC](https://img.shields.io/badge/gRPC-Protobuf-244c5a?style=flat&logo=grpc)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat&logo=docker)

A distributed microservices sample built with .NET demonstrating event-driven architecture, gRPC communication and the Saga pattern for distributed transactions.

## Architecture

This project simulates a simple e-commerce order workflow implemented using microservices.

Services:

- OrderService
- PaymentService
- InventoryService
- NotificationService

Communication patterns used:

- gRPC (synchronous service-to-service communication)
- RabbitMQ (asynchronous event-driven messaging)

Message handling is implemented with MassTransit.

## Tech Stack

- .NET 8
- MassTransit
- RabbitMQ
- gRPC
- Docker
- Minimal APIs

## System Flow

1. Client calls `/create-order` endpoint on OrderService
2. OrderService validates payment via gRPC with PaymentService
3. If payment is successful, OrderService publishes `OrderCreatedEvent`
4. InventoryService consumes the event and tries to reserve inventory
5. NotificationService listens to order events
6. Saga orchestrates the workflow and handles failures

## Saga Pattern

The project implements a Saga State Machine that coordinates distributed transactions between services.

Flow:

OrderCreated → PaymentProcessed → InventoryReserved → OrderCompleted

If inventory reservation fails:

InventoryFailed → RefundPayment → OrderFailed

This ensures consistency across services without using a distributed database transaction.

## Features Demonstrated

- Event-Driven Microservices
- Saga Pattern for Distributed Transactions
- Message-based communication
- gRPC service interaction
- Fault handling and compensation logic

## Learning Goals

This project was built to explore modern backend architecture patterns used in scalable distributed systems.