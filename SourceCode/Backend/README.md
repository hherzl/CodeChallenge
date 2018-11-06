# Backend

This directory contains source code for **Snacks API**.

## API.Core

**API.Core** contains code related to business and database access.

Levels for **API.Core** project:

* BusinessLayer
* DataLayer
* EntityLayer

### Business Layer

Contains objects to represent business logic.

### Data Layer

Provides access to database through *DbContext* class.

### Entity Layer

Contains objects to represent database tables.

## API

**API** project it's the Web API for Snacks Store.

This *API* has the following route table:

|Verb|Route|Description|
|----|-----|-----------|
|GET|api/v1/Warehouse/Product|Gets the products|
|POST|api/v1/Warehouse/Product|Creates a new product|
|PUT|api/v1/Warehouse/Product/{id}|Updates the price for an existing product|
|PUT|api/v1/Warehouse/LikeProduct/{id}|Likes an existing product|
|DELETE|api/v1/Warehouse/Product/{id}|Deletes an existing product|
|POST|api/v1/Sales/PostOrder|Places a new order|

## API.UnitTests

**API.UnitTests** project contains unit tests for Web API Project.

## Auth.API

**Auth.API** it's the API that provides security (Authentication and Authorization) for Web API project.
