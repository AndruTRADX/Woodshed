<h1 align="center">Woodshed</h1>

<p align="center">
  <em>Share your music, get feedback, and connect with other musicians, all in one place.</em>
</p>

<p align="center">
  <img alt="Status" src="https://img.shields.io/badge/status-in%20development-orange" />
  <img alt=".NET" src="https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white" />
  <img alt="SQL Server" src="https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white" />
  <img alt="React" src="https://img.shields.io/badge/React-20232A?logo=react&logoColor=61DAFB" />
  <img alt="TypeScript" src="https://img.shields.io/badge/TypeScript-3178C6?logo=typescript&logoColor=white" />
</p>

---

# What is Woodshed?

Woodshed is a platform for all musicians, where they can share their content, get feedback, interact, and connect, all in one place.

In musician slang, *to woodshed* means to lock yourself in the practice room and work on your craft. This project wants to be that place for musicians all over the world, made with love for the people who inspire us with their passion for music.

## Features

- **Musician profiles**: unique nicknames and the instruments you play.
- **Posts**: share your content with the community.
- **Comments and likes**: get feedback on what you share.
- **Messages**: talk directly with other musicians.
- **Email-based login**: with cookie or bearer token support.
- **Piano-inspired interface with sound effects**: an app that feels like a place you can live in.

## In development

Woodshed is in active development, and in my spare time I will keep updating the repository with more functionalities.

Right now I'm just building the walking skeleton: the base on which the whole project will be built, including the core functionality, the architecture, and all that.

## Tech stack

| Area | Technologies |
| --- | --- |
| **Backend** | C#, .NET, ASP.NET Identity, Entity Framework Core, SQL Server |
| **Architecture** | Clean Architecture, MediatR, AutoMapper, Repository + Unit of Work |
| **Frontend** | React, TypeScript, React Router, shadcn/ui on Radix UI |

## Design philosophy

Woodshed tries to resemble an actual piano, because I want it to be a personalized experience for musicians. The idea is to have an interface that feels responsive and alive when performing actions.

![Woodshed relational model](resources/images/woodshed_1.png)
![Woodshed relational model](resources/images/woodshed_2.png)

You can read more about the why and how in the [DEV Notes](DEV_NOTES.md).

## Relational model v0.0.1

![Woodshed relational model](resources/images/WoodshedRelationalModel.png)

## More content

- [FAQ](FAQ.md)
- [DEV Notes](DEV_NOTES.md): insights on how and why things were built the way they were.

---

<p align="center">
  I hope you folks enjoy it as much as I'm enjoying making it &lt;3
</p>
