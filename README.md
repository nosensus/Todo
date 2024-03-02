# Todo
Todo list

## Update
Azure not available right now.
Rised PostgresSQL database in Docker container
- docker pull postgres
- docker run --name Postgres-database -e POSTGRES_PASSWORD=postgres -d -p 5432:5432 postgres

additional instruction you can [find here](https://www.docker.com/blog/how-to-use-the-postgres-docker-official-image/)

Create migration script
- dotnet ef migrations add NameOfMigration

Restore database
- dotnet ef database update

Run web api server (provide to Todo.WebApi assembly)
- dotnet run

Swagger url
- https://localhost:8000/swagger/index.html


## Technical Task
Share link in - [Google docs](https://docs.google.com/document/d/1kwtM7JFHlMZwD_h-gkE1NMf3HpMf7Q2wSBvxMCIbwGg/edit?usp=sharing)

## Design
Share link in - [Figma](https://www.figma.com/file/114AkxgaDYtkK9bxTiskgM/Untitled?type=design&node-id=0%3A1&mode=design&t=GQKrtz4KfeGLhXRP-1)

## Run WebAPI project in VS Code

- Provide in terminal into folder Todo/Todo_webapi
- dotnet restore
- dotnet run
- open in browser <https://localhost:8000/swagger/index.html>

## Publish Status ([tutorial](<https://learn.microsoft.com/en-us/dotnet/devops/dotnet-publish-github-action>))
[![publish](https://github.com/nosensus/Todo/actions/workflows/publish-app.yml/badge.svg)](https://github.com/nosensus/Todo/actions/workflows/publish-app.yml)
